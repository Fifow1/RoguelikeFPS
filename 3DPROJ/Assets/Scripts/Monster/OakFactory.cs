using System.Collections;
using System.Collections.Generic;
using UnityEditor.Search;
using UnityEngine;

public class OakFactory : BaseFactory
{
    public GameObject oak;
    public Transform target;
    public override void CreateMonster()
    {
        var temp = ObjPool.instance.OnActive("oak", oak);
        temp.GetComponent<OakController>().agent.Warp(new Vector3(0, 1.4f, 0));
        temp.GetComponent<OakController>().player = target;
        Queue<Coroutine> queue = new Queue<Coroutine>();
        queue.Enqueue(StartCoroutine(temp.GetComponent<OakController>().PlayerFind()));
        queue.Enqueue(StartCoroutine(temp.GetComponent<OakController>().StateAnimator()));
        temp.GetComponent<OakController>().queue = queue;
    }
}
