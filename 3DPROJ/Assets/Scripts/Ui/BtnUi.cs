using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class BtnUi : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    Button firstButton;
    Image btnImage;
    Outline btnOutline;
    private Text buttonText;  // 버튼 이미지
    Color textBlack; // 기본 색상 (블랙)
    Color textWhite; // 마우스 올렸을 때 색상 (흰색)
    Color imageGrean ;
    void Start()
    {
        btnOutline = GetComponent<Outline>();
        btnImage = GetComponent<Image>();
        buttonText = transform.GetChild(0).GetComponent<Text>();
        imageGrean = new Color(0.95597f, 0.9206086f, 0.5681736f);
        textBlack = Color.black;
        textWhite = Color.white;
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (buttonText != null)
        {
            btnOutline.enabled = true;
            btnImage.color = imageGrean;
            buttonText.color = textWhite; 
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (buttonText != null)
        {
            btnOutline.enabled = false;
            btnImage.color = textWhite;
            buttonText.color = textBlack;
        }
    }
    private void OnDisable()
    {
        btnOutline.enabled = false;
        btnImage.color = textWhite;
        buttonText.color = textBlack;
    }


}
