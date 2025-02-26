using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class UiManager : MonoBehaviour
{
    public static UiManager instance;
    public GameObject monsterHp;

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
    public void MonsterHpOnActive(Monster monsterScript)
    {
        if (monsterScript.hpUiActive == true)
        { return; }
        else
        {
            var temp = ObjPool.instance.OnActive("monsterHpUi", monsterHp);
            Debug.Log("넘겨주기 전 : " + monsterScript.name);
            temp.GetComponentInChildren<MonsterHpUi>().target = monsterScript.gameObject.transform;
            temp.GetComponentInChildren<MonsterHpUi>().monster = monsterScript;
            temp.GetComponentInChildren<MonsterHpUi>().SetEvent();
            StartCoroutine(temp.GetComponentInChildren<MonsterHpUi>().MonsterHpTime(monsterScript));
            monsterScript.hpUiActive = true;
        }
    }
    
}
