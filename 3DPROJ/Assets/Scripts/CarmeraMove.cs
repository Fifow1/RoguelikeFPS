using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarmeraMove : MonoBehaviour
{
    public Transform target;
    public float distance;
    public float angle;
    public float height;
    Vector3 tmepVec;
    float test;
    public GameObject ttt;


    float mY;
    //private void Start()
    //{
    //    //tmepVec = transform.position - target.transform.position;
    //}
    //void Update()
    //{
    //    //transform.position = target.position + tmepVec;
    //}
    private void Start()
    {
        distance = 9;
        angle = 7;
        height = 3;
        test = 3;
        mY = 0;
    }
    void Update()
    {
        mY -= Input.GetAxis("Mouse Y");
        mY = Mathf.Clamp(mY, -50, 50);

        tmepVec = target.transform.position + (-target.forward * distance) + (Vector3.up * mY);
        transform.position = tmepVec;
        transform.LookAt(target.transform);
    }

    private void OnValidate()
    {
        tmepVec = target.transform.position + (-target.forward * distance) + (Vector3.up * height);
        transform.rotation = Quaternion.Euler(angle, 0, 0);
        transform.position = tmepVec;
    }

}
