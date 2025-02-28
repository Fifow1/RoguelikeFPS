using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextController : MonoBehaviour
{
    Text text;
    private void Awake()
    {
        text = GetComponent<Text>();
    }
    public void OnText()
    {
        text.enabled = true;
    }
    public void OffText()
    {
        text.enabled = false;
    }
}
