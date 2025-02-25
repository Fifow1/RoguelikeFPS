using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// !!
using UnityEngine.Pool;


public class ArrowObjPool : MonoBehaviour
{
    public GameObject arrowPrefab;
    public Queue<GameObject> pool;
    int maxSize;
    private void Start()
    {
        maxSize = 100;
        CreatPool();
    }

    public void CreatPool()
    {
        Debug.Log("Pool »ý¼º");
        pool = new Queue<GameObject>();
        for (int i = 0; i < maxSize; i++)
        {
            var temp = Instantiate(arrowPrefab);
            temp.SetActive(false);
            pool.Enqueue(temp);
        }
    }
                                       
    public GameObject OnActive()
    {
        var temp = pool.Dequeue();
        temp.SetActive(true);
        return temp;
    }
    public void DeActive(GameObject temp)
    {
        temp.SetActive(false);
        pool.Enqueue(temp);
    }
}
