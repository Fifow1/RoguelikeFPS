using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyesFactory : BaseFactory
{
    public GameObject eyes;
    public Transform target;
    public override void CreateMonster()
    {
        var temp = ObjPool.instance.OnActive("eyes",eyes);
        temp.GetComponent<EyesController>().player = target;
        StartCoroutine(temp.GetComponent<EyesController>().PlayerFind());
        StartCoroutine(temp.GetComponent<EyesController>().StateAnimator());
    }
}
