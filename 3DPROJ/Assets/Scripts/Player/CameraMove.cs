using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class CameraMove : MonoBehaviour
{
    public Transform target;
    Vector3 offset;
    float mX;
    float mY;
    float minAngleY;
    float maxYAngleY;

    void Start()
    {
        offset = transform.position - target.transform.position;
    }

    void Update()
    {
        mX += Input.GetAxis("Mouse X");
        mY -= Input.GetAxis("Mouse Y");
        mY = Mathf.Clamp(mY, -50, 50);
        transform.localRotation = Quaternion.Euler(mY, mX, 0);
        transform.position = target.position + offset;
    }
}
