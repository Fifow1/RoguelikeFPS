using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestFactory : MonoBehaviour
{
    float x;
    float y;
    float z;
    public InteractImage interImage;
    public ItemFactory itemFactory;
    //public GameObject priceText;
    public GameObject chest;
    public GameObject player;
    Ray ray;
    RaycastHit hit;
    public LayerMask layer;


    private void Start()
    {
        int num = 0;
        while (num < 15)
        {
            x = Random.Range(-25.0f, 25.0f);
            z = Random.Range(-30.0f, 30.0f);
            y = 0;
            Physics.Raycast(new Vector3(x, 10, z), Vector3.down,out hit, 10,layer);
            var temp = Instantiate(chest, new Vector3(hit.point.x, hit.point.y + 0.3f, hit.point.z), transform.rotation);
            temp.GetComponent<ChestController>().items = (Items)Random.Range(0,2);
            temp.GetComponent<ChestController>().player = player;
            temp.GetComponent<ChestController>().interImage = interImage;
            //temp.GetComponent<ChestController>().itemFac = itemFactory;
            //temp.GetComponent<ChestController>().priceText = priceText;
            num++;
        }
    }
}
