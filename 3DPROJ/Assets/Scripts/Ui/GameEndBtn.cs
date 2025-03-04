using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameEndBtn : MonoBehaviour
{
    Button gameEndBtn;
    void Start()
    {
        gameEndBtn=GetComponent<Button>();
        gameEndBtn.onClick.AddListener(() => Application.Quit());
    }
}
