using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerGoldText : MonoBehaviour
{
    public GameObject player;
    public PlayerController PlayerController;
    Text goldText;
    private void Awake()
    {
        goldText = GetComponent<Text>();
    }
    void Start()
    {
        PlayerController = player.GetComponent<PlayerController>();
        PlayerController.goldEvent += PrintGoldText;
    }

    public void PrintGoldText(int gold)
    {
        goldText.text = "" + gold;
    }
}
