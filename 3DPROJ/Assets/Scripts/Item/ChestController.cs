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
    public Items items;
    //public Game itemFac;
    public InteractImage interImage;
    public GameObject priceText;
    public GameObject player { get; set; }
    public PlayerController playerController { get; set; }
    private void Start()
    {
        playerController = player.GetComponent<PlayerController>();
        price = 12;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("æ∆¿Ã≈€ : " + items);
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
            if (playerController.gold < price) 
            {
                return;
            }
            else
            {
                playerController.DecreaseGold(price);
                StartCoroutine(transform.GetComponentInChildren<ChestOpen>().OpenCover());
            }
        }
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.W)||
            Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
        {
            interImage.Rotaion(player.transform);
            if (priceText != null)
            {
                priceText.GetComponent<PriceText>().Rotaion(player.transform);
            }
        }
    }
}
