using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Factory : MonoBehaviour
{
    public BaseFactory oak;
    public BaseFactory eyes;
    private void Start()
    {
        StartCoroutine(CreatMonster());
    }
    IEnumerator CreatMonster()
    {
        while (true)
        {
            var temp = Random.Range(1, 2);
            if (temp == 1)
            {
                oak.CreateMonster();
            }
            else if (temp == 2)
            {
                eyes.CreateMonster();
            }
            yield return new WaitForSeconds(2);
        }
    }
}
