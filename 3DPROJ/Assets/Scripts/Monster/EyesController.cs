using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
enum State
{
    idle,move,attack,die
}
public class EyesController : MonoBehaviour
{
    public Transform player; // 플레이어 위치
    private NavMeshAgent agent;
    Animator animator;
    bool isDie;
    float distance;
    public float attackDist;
    State currentState;

    void Start()
    {
        attackDist = 10;
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        StartCoroutine(PlayerFind());
        StartCoroutine(StateAnimator());
    }

    IEnumerator PlayerFind()
    {
        while (isDie == false)
        {
            distance = Vector3.Distance(player.position,transform.position);
            if (player != null)
            {
                agent.SetDestination(player.position);
            }
            if (distance < attackDist)
            {
                currentState = State.attack;
            }
            else
            {
                currentState = State.move;
            }
            yield return new WaitForSeconds(1f);
        }
    }

    IEnumerator StateAnimator()
    {
        while (isDie == false)
        {
            switch (currentState)
            {
                case State.move:
                    animator.SetBool("IsMove", true);
                    break;
                case State.attack:
                    animator.SetBool("IsAttack", true);
                    break;
            }
            yield return new WaitForSeconds(0.1f);
        }
    }
}

