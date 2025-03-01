using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using UnityEngine.SceneManagement;

public class BtnUi : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    Image btnImage;
    Outline btnOutline;
    private Text buttonText;  // ��ư �̹���
    Color textBlack; // �⺻ ���� (��)
    Color textWhite; // ���콺 �÷��� �� ���� (���)
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
            //StartCoroutine(FadeImage(1));
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

}
