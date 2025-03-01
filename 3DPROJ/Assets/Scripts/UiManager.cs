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
    public GameObject GameOverUi;

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
    public void OnGameOverUi()
    {
        GameOverUi.SetActive(true);
    }
    public void OffGameOverUi()
    {
        GameOverUi.SetActive(false);
    }
    public void PauseMenu()
    {
        if (isPauseMenu == true)
        { // 두번째 esc 눌렀을때
            Cursor.visible = false;
            pauseMenu.SetActive(false);
            Time.timeScale = 1f;
            isPauseMenu = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
        else // 처음 esc 눌렀을때
        {
            Cursor.visible = true;
            pauseMenu.SetActive(true);
            Time.timeScale = 0f;
            isPauseMenu = true;
            Cursor.lockState = CursorLockMode.None;
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
