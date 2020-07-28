using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeControls : MonoBehaviour
{
    private float minSwipeRecognition;
    private Rigidbody rb;
    private Vector2 Initialposition, Finalposition;

    public bool selected=false;
    public SelectedLetterManager MainSelectedLetterManger;
    public bool travelling;
    public float speed;
    public Collider Foward, Back, Right, Left, Main;
    private Vector3 nextCollisionPosition;

    private void Start()
    {
        MainSelectedLetterManger = GameObject.Find("SelectedLetterManager").GetComponent<SelectedLetterManager>();
        travelling = false;
        Foward.enabled = false;
        Back.enabled = false;
        Right.enabled = false;
        Left.enabled = false;
        minSwipeRecognition = Screen.height * 15 / 100;
        rb = GetComponent<Rigidbody>();
        rb.velocity = Vector3.zero;
    }
    private void OnMouseDown()
    {
        if (!travelling)
        {
            MainSelectedLetterManger.ChangeCurrentSelected(gameObject);
            ResetPosition.reset = false;
        }

    }

    private void Update()
    {
        if (nextCollisionPosition != Vector3.zero)
        {
            if (Vector3.Distance(transform.position, nextCollisionPosition) < 0.5f)
            {
                travelling = false;
                nextCollisionPosition = Vector3.zero;
                rb.velocity = Vector3.zero;
                Foward.enabled = false;
                Back.enabled = false;
                Right.enabled = false;
                Left.enabled = false;
            }
        }
        if (travelling) return;

        if (Input.GetMouseButtonDown(0))
        {
            Initialposition = Finalposition = Input.mousePosition;
        }

        if (Input.GetMouseButtonUp(0))
        {
            Finalposition = Input.mousePosition;
        }

        if (!travelling)
        {
            if (Math.Abs(Finalposition.x - Initialposition.x) > minSwipeRecognition || Math.Abs(Finalposition.y - Initialposition.y) > minSwipeRecognition)
            {
                if (Math.Abs(Finalposition.x - Initialposition.x) > Math.Abs(Finalposition.y - Initialposition.y))
                {
                    if (Finalposition.x > Initialposition.x)
                    {
                        rb.velocity = Vector3.right * speed;
                        travelling = true;
                        RaycastHit hit;
                        if (Physics.Raycast(transform.position, transform.right, out hit))
                        {
                            nextCollisionPosition = hit.point;
                        }
                    }
                    else
                    {
                        rb.velocity = Vector3.left * speed;
                        travelling = true;
                        RaycastHit hit;
                        if (Physics.Raycast(transform.position, transform.right*-1, out hit))
                        {
                            nextCollisionPosition = hit.point;
                        }
                    }
                }
                else
                {
                    if (Finalposition.y > Initialposition.y)
                    {
                        rb.velocity = Vector3.forward * speed;
                        travelling = true;
                        RaycastHit hit;
                        if (Physics.Raycast(transform.position, Vector3.forward, out hit))
                        {
                            nextCollisionPosition = hit.point;
                        }
                    }
                    else
                    {
                        rb.velocity = Vector3.back * speed;
                        travelling = true;
                        RaycastHit hit;
                        if (Physics.Raycast(transform.position,Vector3.back, out hit))
                        {
                            nextCollisionPosition = hit.point;
                            Debug.Log(nextCollisionPosition);
                        }
                    }
                }
                Initialposition = Finalposition = Vector2.zero;
            }
        }
    }
}
