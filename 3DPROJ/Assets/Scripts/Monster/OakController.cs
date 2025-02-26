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
    private NavMeshAgent agent;
    int str;
    public float attackDist;
    State currentState;

    private void Awake()
    {
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
        //queue = new Queue<Coroutine>();
        maxHp = 100;
        currentHp = maxHp;
    }
    public IEnumerator PlayerFind()
    {
        while (isDie == false)
        {
        Debug.Log("!!!!!!!!!!");
            distance = Vector3.Distance(player.position, transform.position);
            if (distance < attackDist)
            {
                currentState = State.attack;
            }
            else
            {
                agent.SetDestination(player.position);
                currentState = State.move;
            }
            if (currentHp <= 0)
            {
                currentState = State.die;
                isDie = true;
            }
            yield return new WaitForSeconds(0.1f);
        }
        OnDie();
    }
    public override void OnDie()
    {
        animator.SetTrigger("IsDiee");
        foreach (Coroutine i in queue)
        {
            StopCoroutine(i);
        }
    }

    public IEnumerator StateAnimator()
    {
        while (isDie == false)
        {
        Debug.Log("!!!!!!!!!!");
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
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 5);
    }

}
