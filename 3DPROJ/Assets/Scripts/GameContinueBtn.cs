using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
    Button btn;
    private void Start()
    {
        btn = GetComponent<Button>();
        btn.onClick.AddListener(()=> UiManager.instance.PauseMenu());
    }
}
