using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedLetterManager : MonoBehaviour
{
    public static GameObject CurrentSelected;
    public static bool resetSelectedLetterManager=false;
    public Material Unselected, Selected;

    private void Update()
    {
        if (resetSelectedLetterManager)
        {
            CurrentSelected.GetComponent<Renderer>().material = Unselected;
            CurrentSelected.GetComponent<Rigidbody>().isKinematic = true;
            CurrentSelected.GetComponent<BoxCollider>().enabled = true;
            CurrentSelected.GetComponent<Swipe>().initializeToZero();
            CurrentSelected.GetComponent<Swipe>().enabled = false;

            resetSelectedLetterManager = false;
        }
    }
    public void ChangeCurrentSelected(GameObject NewObj)
    {
        if (CurrentSelected != null)
        {
            CurrentSelected.GetComponent<Renderer>().material = Unselected;
            CurrentSelected.GetComponent<Rigidbody>().isKinematic = true;
            CurrentSelected.GetComponent<BoxCollider>().enabled = true;
            CurrentSelected.GetComponent<Swipe>().initializeToZero();
            CurrentSelected.GetComponent<Swipe>().enabled = false;
        }
        CurrentSelected = NewObj;
        CurrentSelected.GetComponent<Swipe>().enabled = true;
        CurrentSelected.GetComponent<Renderer>().material = Selected;
        CurrentSelected.GetComponent<Rigidbody>().isKinematic = false;
    }
}