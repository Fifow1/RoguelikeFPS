using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowPluseItem : Item
{
    PlayerController playerController;
    Rigidbody rb;
    protected override void Effect()
    {
        // 화살 개수 추가
    }
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.AddForce(Vector3.up * 4,ForceMode.Impulse);
    }
}
