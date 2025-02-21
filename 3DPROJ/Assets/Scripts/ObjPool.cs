using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// !!
using UnityEngine.Pool;
using UnityEngine.Rendering;

public class ObjPool : MonoBehaviour
{
    //ObjectPool<GameObject> myObjPool;
    List<Action> actions = new List<Action>();  
    void Start()
    {
        for (int i=0; i<3; i++)
        {
            actions.Add(() => Debug.Log(i));
        }
        foreach (var action in actions) 
        {
            action();
        }
    }
}
