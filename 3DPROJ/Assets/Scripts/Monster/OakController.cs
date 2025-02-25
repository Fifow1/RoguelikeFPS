using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public class OakController : MonoBehaviour
{
    public Transform player; // 플레이어 위치
    Animator animator;
    private NavMeshAgent agent;
    bool isDie;
    bool isAttacking;
    float attackColldown;
    int hp;
    int str;
    float distance;
    public float attackDist;
    State currentState;
    AnimatorStateInfo stateInfo;

    void Start()
    {

        attackColldown = 2f;
        isAttacking = false;
        str = 20;
        attackDist = 5;
        hp = 10;
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        agent.updatePosition = false;
        agent.updateRotation = false;
        StartCoroutine(PlayerFind());
        StartCoroutine(StateAnimator());
    }

    IEnumerator PlayerFind()
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
                agent.SetDestination(player.position);
                currentState = State.move;
            }
            if (hp <= 0)
            {
                currentState = State.die;
                isDie = true;
            }
            yield return new WaitForSeconds(0.1f);
        }
    }

    IEnumerator StateAnimator()
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
                case State.die:
                    animator.SetBool("IsDie",true);
                    break;
            }
            yield return new WaitForSeconds(0.5f);    
        }
    }
    
    void OnAttack()
    {
        Collider[] temp = Physics.OverlapSphere(transform.position, 5);
        for (int i = 0; i < temp.Length; i++)
        {
            if (temp[i].gameObject.tag == "Player")
            {
                player.gameObject.GetComponent<PlayerController>().TakeDamage(str);
            }

        }

    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 5);
    }
}
