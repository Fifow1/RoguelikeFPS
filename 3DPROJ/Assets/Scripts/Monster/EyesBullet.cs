using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyesBullet : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag =="Player")
        {
            ObjPool.instance.DeActive("eyesBullet",gameObject);
            other.GetComponent<PlayerController>().DecreaseHp(5);
        }
        if (other.gameObject.tag == "Ground")
        {

        }
    }
}
