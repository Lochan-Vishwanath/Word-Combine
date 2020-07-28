using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LetterSelector : MonoBehaviour
{
    SelectedLetterManager MainSelectedLetterManger;

    private void Start()
    {
        MainSelectedLetterManger = GameObject.Find("SelectedLetterManager").GetComponent<SelectedLetterManager>();
    }

    private void OnMouseDown()
    {
        GetComponent<BoxCollider>().enabled = false;
        MainSelectedLetterManger.ChangeCurrentSelected(gameObject);
    }
}