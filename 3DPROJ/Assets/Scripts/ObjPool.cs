using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// !!
using UnityEngine.Pool;


public class ObjPool : MonoBehaviour
{
    public static ObjPool instance;
    GameObject temp;
    public Transform canvas;
    public GameObject[] prefab;
    Dictionary<string, List<GameObject>> pool;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        pool = new Dictionary<string, List<GameObject>>();
        CreatPool("arrow", prefab[0],20);
        CreatPool("oak", prefab[2],20);
        CreatPool("eyes", prefab[3],20);
        CreatPool("eyesBullet", prefab[4],30);
        CreatPool("monsterHpUi", prefab[1],5);
    }

    public void CreatPool(string key , GameObject prefabs , int count)
    {
        pool[key] = new List<GameObject>();
        for (int i = 0; i < count; i++)
        {
            if (key == "monsterHpUi")
            {
                temp = Instantiate(prefabs, canvas);
            }
            else
            {
                temp = Instantiate(prefabs);
            }
            temp.SetActive(false);
            pool[key].Add(temp);
        }
    }

    public GameObject OnActive(string key, GameObject prefabs)
    {
        GameObject temp = pool[key].Find(obj => obj.activeInHierarchy == false );
        if (temp != null)
        {
            temp.SetActive(true);
        }
        else
        {
            var temp2 = Instantiate(prefabs);
            temp2.SetActive(true);
            pool[key].Add(temp);
        }
        return temp;
    }
    public void DeActive(string key, GameObject prefabs)
    {
        if (pool[key].Contains(prefabs) == true)
        {
            prefabs.SetActive(false);
        }
        else
        {
            Debug.Log("sss");
        }
    }
}
