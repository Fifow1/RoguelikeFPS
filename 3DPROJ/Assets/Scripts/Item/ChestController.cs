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
    bool isOpen;
    int price;
    public Items items;
    //public Game itemFac;
    public InteractImage interImage;
    public GameObject priceText;
    public GameObject item;
    public GameObject player { get; set; }
    public PlayerController playerController { get; set; }
    private void Start()
    {
        isOpen = false;
        playerController = player.GetComponent<PlayerController>();
        price = 12;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && isOpen ==false)
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

        if (other.gameObject.tag == "Player"&& isOpen == false)
        {
            ObjPool.instance.DeActive("chestPrice", priceText);
            interImage.DisableImage();
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (isOpen == true)
        {
            return;
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.E)) 
            {
                Debug.Log("eeeeeeeee");
                if (playerController.gold < price) 
                {
                    isOpen = false;
                    return;
                }
                else
                {
                    isOpen = true;
                    ObjPool.instance.DeActive("chestPrice", priceText);
                    interImage.DisableImage();
                    playerController.DecreaseGold(price);
                    StartCoroutine(transform.GetComponentInChildren<ChestOpen>().OpenCover());
                    //Instantiate(item,transform.position,transform.rotation);
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
}
