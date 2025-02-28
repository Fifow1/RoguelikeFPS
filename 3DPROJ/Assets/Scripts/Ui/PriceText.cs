using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PriceText : MonoBehaviour, IPrint, IRotation
{
    public GameObject plater;
    Text textCom;
    public int price;
    private void Awake()
    {
        textCom = GetComponent<Text>();
    }
    public void PrintPrice(int price)
    {
        textCom.text = "$" + price;
    }
    public void Print(GameObject chestPos)
    {
        transform.position = new Vector3(chestPos.transform.position.x, chestPos.transform.position.y + 0.7f, chestPos.transform.position.z);
    }
    public void Rotaion(Transform player)
    {
        transform.rotation = Quaternion.LookRotation(-(player.transform.position - transform.position));
    }

}
