using System;
using Unity.VisualScripting;
using UnityEngine;

interface IPrint
{
    public void Print(GameObject pos);
}
interface IRotation
{
    public void Rotaion(Transform player);
}
public class ChestController : MonoBehaviour
{
    int price;
    
    public InteractImage interImage;
    public GameObject priceText;
    public GameObject player { get; set; }
    private void Awake()
    {
        player.GetComponent<PlayerController>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            price = 12;
            interImage.ChangePos(transform);
            interImage.EnableImage();
            priceText = UiManager.instance.OnActiveChestPrice();
            priceText.GetComponent<PriceText>().Print(gameObject);
            priceText.GetComponent<PriceText>().PrintPrice(price);
        }
    }
    private void OnTriggerExit(Collider other)
    {

        if (other.gameObject.tag == "Player")
        {
            ObjPool.instance.DeActive("chestPrice", priceText);
            interImage.DisableImage();
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (Input.GetKeyDown(KeyCode.E)) 
        {
            if(){

            }
            StartCoroutine(transform.GetComponentInChildren<ChestOpen>().OpenCover());
        }
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.W)||
            Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
        {
            interImage.Rotaion(player.transform);
            priceText.GetComponent<PriceText>().Rotaion(player.transform);
        }
    }
}
