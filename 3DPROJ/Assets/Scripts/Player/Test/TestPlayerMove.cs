using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlayerMove : MonoBehaviour
{
    float h;
    float v;
    float moveSpeed;
    Rigidbody myRigid;
    Vector3 myVec;
    void Start()
    {
        moveSpeed = 8f;
        myRigid = GetComponent<Rigidbody>();
    }

    //void Update()
    //{
    //    h = Input.GetAxis("Horizontal");
    //    v = Input.GetAxis("Vertical");
    //    myVec = (new Vector3(h, 0, v)).normalized;

    //    //var aa = camera.GetComponent<TestCameraMove>().cameraForward * myVec;

    //    Vector3 forward = Camera.main.transform.forward;  // 카메라의 앞방향
    //    Vector3 right = Camera.main.transform.right;
    //    forward.y = 0; // 수직 방향 제거 (지면과 평행한 방향 유지)
    //    right.y = 0;
    //    // 001
    //    // 100
    //    Vector3 moveDirection = (forward * myVec.z + right * myVec.x).normalized;

    //    if (myVec != Vector3.zero)
    //    {
    //        transform.localRotation = Quaternion.LookRotation(new Vector3(moveDirection.x, 0, moveDirection.z), new Vector3(0, 1, 0));
    //        myRigid.velocity = transform.TransformDirection(Vector3.forward * moveSpeed);
    //    }
    //    else
    //    {
    //        myRigid.velocity = Vector3.zero;
    //    }

    //}

    #region
    void Update()
    {
        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");

        Vector3 forward = Camera.main.transform.forward; // 카메라의 앞방향
        Vector3 right = Camera.main.transform.right;     // 카메라의 오른쪽 방향

        forward.y = 0; // 수직 방향 제거 (지면과 평행한 방향 유지)
        right.y = 0;

        Vector3 moveDirection = (forward * v + right * h).normalized;

        // 2️⃣ 이동 방향이 있을 경우 플레이어 회전
        if (moveDirection.sqrMagnitude > 0.01f)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(moveDirection), Time.deltaTime * 10f);
            myRigid.velocity = moveDirection * moveSpeed; // 3️⃣ 카메라 방향을 반영한 속도 적용
        }
        else
        {
            myRigid.velocity = Vector3.zero;
        }
    }
    #endregion
}
