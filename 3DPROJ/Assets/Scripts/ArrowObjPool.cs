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
        maxSize = 20;
        CreatPool();
    }

    public void CreatPool()
    {
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
        if (pool.Count == 0)
        {
            Instantiate(arrowPrefab);
        }
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
