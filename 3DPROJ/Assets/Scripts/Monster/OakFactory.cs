using System.Collections.Generic;
using UnityEngine;

public class OakFactory : BaseFactory
{
    int oakCount;
    public GameObject oak;
    public Transform target;
    public override void CreateMonster()
    {
        var temp = ObjPool.instance.OnActive("oak", oak);
        temp.GetComponent<OakController>().agent.Warp(new Vector3(0, 1.2f, 0));
        temp.GetComponent<OakController>().player = target;
        StartCoroutine(temp.GetComponent<OakController>().PlayerFind());
        StartCoroutine(temp.GetComponent<OakController>().StateAnimator());
    }
}
