using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class MonsterHpUi : MonoBehaviour
{
    public Transform target;
    public Monster monster;
    public Image image;
    private void Awake()
    {
        image = GetComponent<Image>();
    }
    private void OnDisable()
    {
        if (monster.eventHp != null)
        {
            monster.eventHp -= MonsterHpUpdate;
        }
    }
    public void SetEvent()
    {
        if (monster.eventHp == null)
        {
            monster.eventHp += MonsterHpUpdate;
        }
    }
    public void MonsterHpUpdate(float currentHp , float maxHp)
    {
        image.fillAmount = currentHp / maxHp;
    }
    private void LateUpdate()
    {
        if (target == null)
        {
            return;
        }
        else
        {
            transform.parent.transform.position = Camera.main.WorldToScreenPoint(target.transform.position);
        }
    }
    public IEnumerator MonsterHpTime(Monster monsterScript)
    {
        yield return new WaitForSeconds(2);
        ObjPool.instance.DeActive("monsterHpUi", gameObject.transform.parent.gameObject);
        monsterScript.hpUiActive = false;
    }
}
