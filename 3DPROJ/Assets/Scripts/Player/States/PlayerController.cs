using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Playables;

public class PlayerController : MonoBehaviour
{
    public float maxHp { get; private set; }
    public float currentHp { get; private set; }
    public int gold { get; private set; }
    public Action hpEvent;
    public ObjPool arrowPool;
    //public GameObject aimInfo;
    public LinkedList<GameObject> attackRangeMonsterList = new LinkedList<GameObject>();
    public Coroutine attackCoroution;
    public GameObject arrowPrefab;
    public Rigidbody rb;
    public Animator animator { get; set; }
    public Vector3 moveDir;
    public float turnSpeed { get; private set; }

    // public float walkSpeed { get; private set; }
    // public bool isWalking { get; set; }
    // public bool isRunning { get; set; }
    // public float runSpeed { get; set; }

    public float speedParameter;
    public bool isMouseLeftPress;
    public bool isAttacking;

    public LayerMask groundLayer; 
    Vector3 arrowDir;
    public Vector3 camForward;
    public Vector3 camRight;
    public Vector3 moveDirection;
    public Vector2 currentInput;
    //걷기
    //점프
    //달리기
    //PlayerState playerState;
    public AttackRange attackRange;
    public PlayerState playerState { get; private set; }
    public PlayerIdle playerIdle { get; private set; }
    public PlayerMove playerMove { get; private set; }
    public PlayerJump playerJump { get; private set; }
    public PlayerAttackMove playerAttackMove { get; private set; }
    public AttackIdle playerAttackIdle { get; private set; }

    public void ChangeState(PlayerState playerState)
    {
        this.playerState = playerState;
        playerState.Enter(this);
    }
    private void Awake()
    {

        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;
        maxHp = 100;
        currentHp = maxHp;
        isAttacking = false;
        playerIdle = new PlayerIdle();
        playerAttackMove = new PlayerAttackMove();
        playerAttackIdle = new AttackIdle();
        playerMove = new PlayerMove();
        playerJump = new PlayerJump();
        isMouseLeftPress = false;
        turnSpeed = 10f;
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    void Start()
    {
        ChangeState(playerIdle);
    }

    void Update()
    {
        playerState.Update(this);
    }

    private void FixedUpdate()
    {
        CarmeraStandard();
        playerState.FixedUpdate(this);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Monster")
        {
            attackRangeMonsterList.AddLast(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Monster")
        {
            attackRangeMonsterList.Remove(other.gameObject);
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Monster")
        {
            attackRangeMonsterList.Remove(other.gameObject);
        }
    }

    public void MonsterDetection()
    {

    }



    public void LookRotation()
    {
        moveDirection  = (camForward * moveDir.z + camRight * moveDir.x).normalized;
        Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * turnSpeed);
    }
    
    public void CarmeraStandard()
    {
        camForward = Camera.main.transform.forward;
        camRight = Camera.main.transform.right;
        camForward.y = 0;
        camRight.y = 0;
        camForward.Normalize();
        camRight.Normalize();
    }

    public void OnMove(InputValue val)
    {
        Vector2 temp = val.Get<Vector2>();
        moveDir = new Vector3(temp.x, 0, temp.y);
    }
    public void OnAttack(InputValue val)
    {
        if (val.isPressed == true)
        {
            isAttacking = true;
        }
        else
        {
            isAttacking = false;
        }
    }
    public void AttackAnimationOn()
    {
        animator.SetBool("IsAttack", true);
    }
    public void AttackAnimationFalse()
    {
        animator.SetBool("IsAttack", false);

    }
    public IEnumerator Shot()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            yield return new WaitUntil(() => attackRange.proximateMonster != null);
            StartCoroutine(ArrowCreate());
        }
    }
    public IEnumerator ArrowCreate()
    {
        arrowDir = (attackRange.proximateMonster.gameObject.transform.position - transform.position).normalized;
        for (int i = 0; i < 3; i++)
        {
            var temp = arrowPool.OnActive("arrow", arrowPrefab);
            //Debug.Log(temp.name);
            temp.GetComponent<ArrowController>().target = arrowDir;
            temp.GetComponent<ArrowController>().startPoint = transform.position;
            temp.GetComponent<ArrowController>().arrowPool = this.arrowPool;
            temp.GetComponent<ArrowController>().ShotArrow();
            yield return new WaitForSeconds(0.2f);
        }
        
    }
    public void DecreaseHp(int damage)
    {
        Debug.Log("데미지 받음 , " + currentHp + "-" + damage + "=" + (currentHp - damage));
        currentHp -= damage;
        hpEvent?.Invoke();

    }
    public void AddGold(int gold)
    {
        this.gold += gold;
    }
}
