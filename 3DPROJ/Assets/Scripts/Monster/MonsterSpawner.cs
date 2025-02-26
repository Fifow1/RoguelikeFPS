using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    public GameObject oak;
    public GameObject eyes;
    public Transform player;
    private void Start()
    {
        StartCoroutine(CreatMonster());
    }
    public void MonsterSpawn(int num)
    {
        GameObject temp = new GameObject();
        switch (num)
        {
            case 1:
                temp = ObjPool.instance.OnActive("oak", oak);
                temp.transform.position = transform.position;
                temp.GetComponent<OakController>().player = player;
                Debug.Log("오크 생성");
                temp.GetComponent<OakController>().StartCoroutine(temp.GetComponent<OakController>().PlayerFind());
                temp.GetComponent<OakController>().StartCoroutine(temp.GetComponent<OakController>().StateAnimator());
                break;
            case 2:
                temp = ObjPool.instance.OnActive("Eyes", eyes);
                temp.transform.position = transform.position;
                temp.GetComponent<OakController>().player = player;
                break;
        }
    }

    IEnumerator CreatMonster()
    {
        while (true)
        {
            var temp = Random.Range(1, 2);
            MonsterSpawn(temp);
            yield return new WaitForSeconds(5f);
        }
    }

}
