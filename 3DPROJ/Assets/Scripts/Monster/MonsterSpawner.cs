using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    public GameObject oak;
    public GameObject eyes;
    public void MonsterSpawn(string name)
    {
        GameObject temp = new GameObject();
        switch (name)
        {
            case "oak":
                temp = Instantiate(oak);
               break;
            case "Eyes":
                temp = Instantiate(eyes);
                break;
        }
    }

   // IEnumerator CreatMonster()
   // {
   //
   // }

   
}
