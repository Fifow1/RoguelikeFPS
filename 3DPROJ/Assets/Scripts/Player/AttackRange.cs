using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst.Intrinsics;
using Unity.Mathematics;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.UI;

public class AttackRange : MonoBehaviour
{
    public Image aim;
    public Coroutine AttackRangeCheckCoroutine { get; set; }
    public Coroutine AroundMonsterCheckCoroutine { get; set; }
    public Collider[] inRangeMonsterList { get; set; }
    public Collider proximateMonster { get; set; }
    float distance;
    float maxRange;
    //LinkedList<GameObject> inRangeMonsterList;
    public LayerMask monsterLayer;
    private void Start()
    {
        maxRange = 1000000;
        StartCoroutine(AttackRangeCheck());
    }
    IEnumerator AttackRangeCheck()
    {
        while (true)
        {
            // 영역내 적 배열에 담기
            inRangeMonsterList = Physics.OverlapBox(transform.position,new Vector3(3, 3,7) ,transform.rotation, monsterLayer);
            if (inRangeMonsterList.Length == 0 )
            {
                aim.enabled = false;

            }
            yield return StartCoroutine(AroundMonsterCheck());
        }
    }

    IEnumerator AroundMonsterCheck()
    {
        float tempDistance = maxRange;
        proximateMonster = null;
        var tempPlayerPos = Camera.main.WorldToScreenPoint(transform.position);
        for (int i = 0; i < inRangeMonsterList.Length; i++)
        {
            var tempMonPos = Camera.main.WorldToScreenPoint(inRangeMonsterList[i].gameObject.transform.position);
            distance = Vector3.Distance(tempPlayerPos, tempMonPos);
            if (distance < tempDistance)
            {//
                tempDistance = distance;
                proximateMonster = inRangeMonsterList[i];
            }
        }
        if (proximateMonster != null)
        {
            AimmingUi(proximateMonster.transform.position);
        }
        yield return new WaitForSeconds(0.1f);
    }

    void AimmingUi(Vector3 target)
    {
        aim.enabled = true;
        var temp = Camera.main.WorldToScreenPoint(target);
        aim.transform.position = new Vector3(temp.x, temp.y+120, temp.z);
    }
    #region 첫번째 시도
    //IEnumerator AroundMonsterCheck()
    //{
    //    while (true)
    //    {
    //        yield return new WaitUntil(()=> inRangeMonsterList.Length>0);
    //        Debug.Log(inRangeMonsterList.Length);
    //        float tempDistance = maxRange;
    //        proximateMonster = null;
    //        for (int i = 0; i < inRangeMonsterList.Length; i++)
    //        {
    //            distance = Vector3.Distance(transform.position , inRangeMonsterList[i].gameObject.transform.position);
    //            if (distance < tempDistance) 
    //            {
    //                tempDistance = distance;
    //                proximateMonster = inRangeMonsterList[i];
    //            }
    //        }
    //        if (proximateMonster != null)
    //        {
    //            proximateMonster.transform.Rotate(0,10,0);
    //        }
    //        yield return new WaitForSeconds(0.1f);
    //    }
    //}
    #endregion

}

