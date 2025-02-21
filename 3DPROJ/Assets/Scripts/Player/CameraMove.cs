using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class CameraMove : MonoBehaviour
{
    public Transform target;
    float distance;
    float maxDistance;
    float minDistance;
    Vector3 offset;
    float mX;
    float mY;
    Ray ray;

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
