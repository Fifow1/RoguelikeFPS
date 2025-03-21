using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EyesFactory : BaseFactory
{
    int eyesCount;
    public GameObject eyes;
    public Transform target;
    public override void CreateMonster()
    {
        var temp = ObjPool.instance.OnActive("eyes",eyes);
        temp.GetComponent<EyesController>().agent.Warp(new Vector3(0, 0, 0));
        temp.GetComponent<EyesController>().player = target;
        StartCoroutine(temp.GetComponent<EyesController>().PlayerFind());
        StartCoroutine(temp.GetComponent<EyesController>().StateAnimator());
    }
}
