using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractImage : MonoBehaviour,IRotation
{
    public Transform target;
    Image image;
    private void Awake()
    {
        image = GetComponent<Image>();
    }
    public void ChangePos(Transform target)
    {
        this.target = target;
        transform.position = new Vector3(target.position.x, target.position.y+1.5f, target.position.z);
    }


    public void Rotaion(Transform player)
    {
        transform.rotation = Quaternion.LookRotation(-(player.transform.position - transform.position));
    }

    public void DisableImage()
    {
        image.enabled = false;
        transform.GetChild(0).GetComponent<TextController>().OffText();
        transform.GetChild(1).GetComponent<TextController>().OffText();

    }
    public void EnableImage()
    {
        image.enabled = true;
        transform.GetChild(0).GetComponent<TextController>().OnText();
        transform.GetChild(1).GetComponent<TextController>().OnText();
    }
}
