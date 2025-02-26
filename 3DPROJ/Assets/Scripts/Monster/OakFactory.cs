using System.Collections;
using System.Collections.Generic;
using UnityEditor.Search;
using UnityEngine;

public class OakFactory : BaseFactory
{
    public GameObject oak;
    public Transform target;
    //Queue<Coroutine> queue = new Queue<Coroutine>();
    public override void CreateMonster()
    {
        var temp = ObjPool.instance.OnActive("oak", oak);
        temp.transform.position = new Vector3(3,24,12);
        temp.GetComponent<OakController>().player = target;
        Queue<Coroutine> queue = new Queue<Coroutine>();
        queue.Enqueue(StartCoroutine(temp.GetComponent<OakController>().PlayerFind()));
        queue.Enqueue(StartCoroutine(temp.GetComponent<OakController>().StateAnimator()));
        temp.GetComponent<OakController>().queue = queue;
        //temp.GetComponent<OakController>().hpUi = temp;
    }
}
