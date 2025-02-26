using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEditor.Search;
using UnityEngine;
using UnityEngine.AI;
enum State
{
    idle,move,attack,die
}
public class EyesController : Monster
{
    public Transform player; // ÇÃ·¹ÀÌ¾î À§Ä¡
    private NavMeshAgent agent;
    Animator animator;
    public float attackDist;
    State currentState;
    private void Awake()
    {
        SetValue(50,50,false,0);
        attackDist = 5;
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        
    }

    public IEnumerator PlayerFind()
    {
        while (isDie == false)
        {
            distance = Vector3.Distance(player.position,transform.position);
            if (currentHp <= 0)
            {
                isDie = true;
            }
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
            yield return new WaitForSeconds(0.1f);
        }
        OnDie();
    }

    public IEnumerator StateAnimator()
    {
        while (isDie == false)
        {
            switch (currentState)
            {
                case State.move:
                    animator.SetBool("IsAttack", false);
                    break;
                case State.attack:
                    animator.SetBool("IsAttack", true);
                    break;
            }
            yield return new WaitForSeconds(0.1f);
        }
    }
    public override void OnDie()
    {
        Debug.Log("Á×À½Á×À½");
        animator.SetTrigger("IsDie");
        StopAllCoroutines();
    }
    public override void OnAttack()
    {
        var temp = ObjPool.instance.OnActive("eyesBullet",gameObject);
        temp.transform.position = transform.position;
        Vector3 tempVec = (player.position - transform.position).normalized;
        temp.GetComponent<Rigidbody>().AddForce(tempVec * 10,ForceMode.Impulse);

    }
}

