using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ArrowController : MonoBehaviour
{
    public ArrowObjPool arrowPool;
    public Vector3 startPoint;
    public Vector3 target;
    Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    

    public void ShotArrow()
    {
        transform.position = new Vector3(startPoint.x, startPoint.y + 0.5f, startPoint.z);
        transform.rotation = Quaternion.LookRotation(target);
        rb.AddForce(target * 100, ForceMode.Impulse);
    }
    private void OnDisable()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag =="Monster")
        {
            arrowPool.DeActive(gameObject);
        }
    }
}
