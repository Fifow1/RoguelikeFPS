using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class test : MonoBehaviour
{
    Coroutine coroutine1;

    private void Start()
    {
    }
    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
        }
        else
        {

        }

    }

    IEnumerator a()
    {
        while (true)
        {
            Debug.Log("a - 1");
            coroutine1 = StartCoroutine(b());
            yield return new WaitForSeconds(5f);
            Debug.Log("a - 2");
        }
    }
    IEnumerator b()
    {
        Debug.Log("b - 1");
        yield return new WaitForSeconds(0.5f);
        Debug.Log("b - 2");
        yield return new WaitForSeconds(0.5f);
        Debug.Log("b - 3");
        yield return new WaitForSeconds(0.5f);
        StopCoroutine(coroutine1);

    }


    //IEnumerator a()
    //{
    //    while (true)
    //    {
    //        Debug.Log("a - 1");
    //        StartCoroutine(b());
    //        yield return new WaitForSeconds(0.1f);
    //    }
    //}
    //IEnumerator b()
    //{
    //    while (true)
    //    {
    //        Debug.Log("b - 1");
    //        yield return new WaitForSeconds(10f);
    //    }
    //}
}
