using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetPosition : MonoBehaviour
{
    public static bool reset=false;

    bool restPos = false;
    Vector3 startingPos;
    GameObject[] listOfPlayers;

    private void Start()
    {
        listOfPlayers = GameObject.FindGameObjectsWithTag("Player");
        startingPos = transform.position;
    }

    private void Update()
    {
        if (reset)
        {
            restPos = true;
            SelectedLetterManager.resetSelectedLetterManager = true;
            GetComponent<BoxCollider>().enabled = true;
            GetComponent<Rigidbody>().isKinematic = true;
            Swipe swipeObj = gameObject.GetComponent<Swipe>();
            swipeObj.initializeToZero();
            swipeObj.enabled = false;
            StartCoroutine("ChangeReset");
        }

        if(restPos)
        {
            transform.position = Vector3.Lerp(transform.position, startingPos, 0.1f);
            if (Vector3.Distance(transform.position, startingPos) < 0.05f)
            {
                restPos = false;
            }
        }
    }

    IEnumerator ChangeReset()
    {
        yield return new WaitForSeconds(0.5f);
        reset = false;
    }
}