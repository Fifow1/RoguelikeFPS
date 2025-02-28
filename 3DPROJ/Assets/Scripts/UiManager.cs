using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEditor.Rendering;
using UnityEngine;

public class UiManager : MonoBehaviour
{
    public static UiManager instance;
    public GameObject monsterHp;
    public GameObject pauseMenu;
    public bool isPauseMenu;
    public GameObject inventory;
    public GameObject chestPrice;
    public GameObject interactionImage;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    //public GameObject PrintInteractionTextOn()
    //{
    //    Debug.Log("22");
    //    interactionImage.SetActive(true);
    //    return interactionImage;
    //}
    //public GameObject PrintInteractionTextOff()
    //{
    //    interactionImage.SetActive(false);
    //    return interactionImage;
    //}
    public void PauseMenu()
    {
        if (isPauseMenu == true)
        {
            pauseMenu.SetActive(false);
            Time.timeScale = 1f;
            isPauseMenu = false;
        }
        else
        {
            pauseMenu.SetActive(true);
            Time.timeScale = 0f;
            isPauseMenu = true;
        }
    }
    public GameObject OnActiveChestPrice()
    {
        var temp = ObjPool.instance.OnActive("chestPrice", chestPrice);
        return temp;
    }
    public void MonsterHpOnActive(Monster monsterScript)
    {
        if (monsterScript.hpUiActive == true)
        { return; }
        else
        {
            var temp = ObjPool.instance.OnActive("monsterHpUi", monsterHp);
            temp.GetComponentInChildren<MonsterHpUi>().target = monsterScript.gameObject.transform;
            temp.GetComponentInChildren<MonsterHpUi>().monster = monsterScript;
            temp.GetComponentInChildren<MonsterHpUi>().SetEvent();
            StartCoroutine(temp.GetComponentInChildren<MonsterHpUi>().MonsterHpTime(monsterScript));
            monsterScript.hpUiActive = true;
        }
    }
    
}
