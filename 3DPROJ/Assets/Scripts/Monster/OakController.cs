using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.Search;
using UnityEngine;
using UnityEngine.AI;

public class OakController : Monster
{
    public Queue<Coroutine> queue;
    public GameObject hpUi;
    public Transform player; // 플레이어 위치
    Animator animator;
    public NavMeshAgent agent;
    public bool isAttacking;
    public bool isDieAnimationResult;
    int str;
    public float attackDist;
    State currentState;

    private void Awake()
    {
        compensation = 5;
        SetValue(5,5,false,0);
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        hpUiActive = false;
        str = 20;
        attackDist = 5;
        agent.updatePosition = false;
        agent.updateRotation = false;
    }
    private void OnEnable()
    {
        isDie = false;
        //queue = new Queue<Coroutine>();
        isDieAnimationResult = false;
        maxHp = 100;
        currentHp = maxHp;
    }
    public IEnumerator PlayerFind()
    {
        while (isDie == false)
        {
            distance = Vector3.Distance(player.position, transform.position);
            if (distance < attackDist)
            {
                currentState = State.attack;
            }
            else
            {
                if (isAttacking ==false)
                {
                    agent.SetDestination(player.position);
                    currentState = State.move;
                }
            }
            if (currentHp <= 0)
            {
                currentState = State.die;
                isDie = true;
            }
            yield return new WaitForSeconds(0.3f);
        }
        animator.SetTrigger("IsDiee");
        yield return new WaitUntil(() => isDieAnimationResult == true);
        OnDie();
    }
    public override void OnDie()
    {
        StopAllCoroutines();
        player.GetComponent<PlayerController>().IncreaseGold(compensation);
        ObjPool.instance.DeActive("oak",gameObject);
    }
    public void DieAnimation()
    {
        isDieAnimationResult = true;
    }

    public IEnumerator StateAnimator()
    {
        while (isDie == false)
        {
            switch (currentState)
            {
                case State.attack:
                    animator.SetBool("IsAttack", true);
                    break;
                case State.move:
                    transform.rotation = Quaternion.LookRotation(player.transform.position - transform.position);
                    animator.SetBool("IsAttack",false);
                    break;
            }
            yield return new WaitForSeconds(0.5f);    
        }
    }

    public override void OnAttack()
    {
        Collider[] temp = Physics.OverlapSphere(transform.position, 5);
        for (int i = 0; i < temp.Length; i++)
        {
            if (temp[i].gameObject.tag == "Player")
            {
                player.gameObject.GetComponent<PlayerController>().DecreaseHp(str);
            }

        }

    }
    public void OnIsAttacking()
    {
        isAttacking = true;
    }
    public void OffIsAttacking()
    {
        isAttacking = false;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 5);
    }

}
