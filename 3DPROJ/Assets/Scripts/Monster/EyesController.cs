using System.Collections;
using UnityEngine;
using UnityEngine.AI;
enum State
{
    idle,move,attack,die
}
public class EyesController : Monster
{
    public Transform player; // 플레이어 위치
    public NavMeshAgent agent;
    Animator animator;
    public float attackDist;
    bool isDieAnimationResult;
    State currentState;
    private void Awake()
    {
        isDie = false;
        SetValue(50,50,false,0);
        attackDist = 5;
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        hpUiActive = false;

    }
    private void OnEnable()
    {
        isDie = false;
        isDieAnimationResult = false;
        compensation = 2;
        maxHp = 100;
        currentHp = maxHp;
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
        animator.SetTrigger("IsDie");
        yield return new WaitUntil(() => isDieAnimationResult == true );
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
    public void DieAnimation()
    {
        isDieAnimationResult = true;
    }
    public override void OnDie()
    {
        StopAllCoroutines();
        transform.GetComponentInChildren<MonsterHitShader>().ResetColor();
        player.GetComponent<PlayerController>().IncreaseGold(compensation);
        ObjPool.instance.DeActive("eyes",gameObject);
    }
    public override void OnAttack()
    {
        var temp = ObjPool.instance.OnActive("eyesBullet",gameObject);
        temp.transform.position = transform.position;
        Vector3 tempVec = (player.position - temp.transform.position).normalized;
        temp.GetComponent<Rigidbody>().AddForce(tempVec * 10,ForceMode.Impulse);

    }
}

