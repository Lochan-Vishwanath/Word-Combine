using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;

public class Swipe : MonoBehaviour
{
    public float speed = 15;
    public float accleration = 2;

    private Rigidbody rb;
    private int minSwipeRecognition;
    private bool isTraveling;

    private Vector3 travelDirection;
    private Vector3 nextCollisionPosition;

    private Vector2 swipePosLastFrame;
    private Vector2 swipePosCurrentFrame;
    private Vector2 currentSwipe;

    public void initializeToZero()
    {
        travelDirection = Vector3.zero;
        nextCollisionPosition = Vector3.zero;

        swipePosLastFrame = Vector2.zero;
        swipePosCurrentFrame = Vector2.zero;
        currentSwipe = Vector2.zero;

        isTraveling = false;
    }
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        minSwipeRecognition = Screen.height * 15 / 100;
        minSwipeRecognition = (minSwipeRecognition * minSwipeRecognition)-500;
    }

    private void FixedUpdate()
    {
        rb.centerOfMass = Vector3.zero;
        // Set the balls speed when it should travel
        if (isTraveling)
        {
            //rb.velocity = travelDirection * (speed +  accleration * Time.deltaTime);
            rb.AddForce(travelDirection * speed, ForceMode.Impulse);
        }

        // Check if we have reached our destination
        if (nextCollisionPosition != Vector3.zero)
        {
            if (Vector3.Distance(transform.position, nextCollisionPosition) <= 1)
            {
                isTraveling = false;
                travelDirection = Vector3.zero;
                nextCollisionPosition = Vector3.zero;
                //rb.velocity = Vector3.zero;
                //rb.AddForce(travelDirection * 0, ForceMode.Impulse);
                CameraShaker.Instance.ShakeOnce(1f, 2.5f, .1f, .1f);
            }
        }

        if (!isTraveling)
        {
            // Swipe mechanism
            if (Input.GetMouseButton(0))
            {
                // Where is the mouse now?
                swipePosCurrentFrame = new Vector2(Input.mousePosition.x, Input.mousePosition.y);

                if (swipePosLastFrame != Vector2.zero)
                {

                    // Calculate the swipe direction
                    currentSwipe = swipePosCurrentFrame - swipePosLastFrame;

                    if (currentSwipe.sqrMagnitude < minSwipeRecognition) // Minium amount of swipe recognition
                        return;

                    currentSwipe.Normalize(); // Normalize it to only get the direction not the distance (would fake the balls speed)

                    // Up/Down swipe
                    if (currentSwipe.x > -0.5f && currentSwipe.x < 0.5f)
                    {
                        rb.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotation;
                        SetDestination(currentSwipe.y > 0 ? Vector3.forward : Vector3.back);
                    }

                    // Left/Right swipe
                    if (currentSwipe.y > -0.5f && currentSwipe.y < 0.5f)
                    {             
                        rb.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotation;
                        SetDestination(currentSwipe.x > 0 ? Vector3.right : Vector3.left);
                    }
                }
                swipePosLastFrame = swipePosCurrentFrame;
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            swipePosLastFrame = Vector2.zero;
            currentSwipe = Vector2.zero;
        }
    }

    private void SetDestination(Vector3 direction)
    {
        travelDirection = direction;

        // Check with which object we will collide
        RaycastHit hit;
        if (Physics.Raycast(transform.position, direction, out hit))
        {
            nextCollisionPosition = hit.point;
            isTraveling = true;
        }
    }
}