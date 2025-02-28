using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.ShaderGraph.Internal.KeywordDependentCollection;

public class ArrowController : MonoBehaviour
{
    public Coroutine coroutine;
    public Vector3 startPoint;
    public GameObject target;
    Rigidbody rb;
    float distance;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    public void ShotArrow()
    {
        transform.position = new Vector3(startPoint.x, startPoint.y + 2f, startPoint.z);
    }
    private void Update()
    {
        Vector3 ttemp = new Vector3(target.transform.position.x, target.transform.position.y + 1, target.transform.position.z);
        Vector3 temp = (ttemp - transform.position).normalized;
        distance = Vector3.Distance(target.transform.position, transform.position);
        transform.rotation = Quaternion.LookRotation(temp);
        rb.velocity = temp * 5;
        if (distance < 2f)
        {

            rb.velocity = Vector3.zero;
            ObjPool.instance.DeActive("arrow", gameObject);
            Debug.Log(target.name);
            //SM_Wep_Troll_01
            target.GetComponent<Monster>().DecreaseHp(10);
            return;
        }
    }

    public IEnumerator DeActiveCor()
    {
        yield return new WaitForSeconds(2f);
        if (gameObject.activeInHierarchy == true)
        {
            if (coroutine != null)
            {
                StopCoroutine(coroutine);
            }
            ObjPool.instance.DeActive("arrow", gameObject);
        }
    }
}
