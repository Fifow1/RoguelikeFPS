using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameStartBtn : MonoBehaviour
{
    Button btn;
    private void OnEnable()
    {
        btn = GetComponent<Button>();
        btn.onClick.AddListener (() => SceneChange.Instance.LoadScene("STAGE"));
    }
}
