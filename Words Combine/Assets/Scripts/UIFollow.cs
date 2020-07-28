using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIFollow : MonoBehaviour
{
    public RectTransform nameLabel;
    public float Zoffset;
    private void Update()
    {
        //Vector3 namePos = Camera.main.WorldToScreenPoint(transform.position);
        //nameLabel.transform.position = namePos;
        nameLabel.transform.position = new Vector3(transform.position.x,transform.position.y+Zoffset,transform.position.z);
    }
}
