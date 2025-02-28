using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Factory : MonoBehaviour
{
    public BaseFactory oak;
    public BaseFactory eyes;
    int minNum;
    int maxNum;
    int eyesNum;
    int oakNum;
    private void Start()
    {
        minNum = 1;
        maxNum = 3;
        eyesNum = 2;
        oakNum = 1;
        StartCoroutine(CreatMonster());
    }
    IEnumerator CreatMonster()
    {
        while (true)
        {
            var temp = Random.Range(minNum, maxNum);
            if (temp == oakNum)
            {
                oak.CreateMonster();
            }
            else if (temp == eyesNum)
            {
                eyes.CreateMonster();
            }
            yield return new WaitForSeconds(1);
        }
    }
}
