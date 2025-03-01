using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Items
{
    speed,arrowCountPlus
}

public class ItemFactory : MonoBehaviour
{
    public GameObject player;
    public GameObject[] itemArr;
    Item items;

    private void Awake()
    {
        
    }
    public void CreatItem(Item item)
    {
        Instantiate(item);
    }
}
