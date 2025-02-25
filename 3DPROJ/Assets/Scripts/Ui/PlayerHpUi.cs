using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class PlayerHpUi : MonoBehaviour
{
    PlayerController playerCNTL;
    Image image;
    private void Awake()
    {
        image = GetComponent<Image>();
    }
    void Start()
    {
        GameObject TEST = GameObject.FindWithTag("Player");
        playerCNTL = TEST.GetComponent<PlayerController>();
        playerCNTL.hpEvent += UpdateHp;
    }
    public void UpdateHp()
    {
        Debug.Log(" rr : " + image.fillAmount);
        image.fillAmount = playerCNTL.currentHp / playerCNTL.maxHp;
    }
}
