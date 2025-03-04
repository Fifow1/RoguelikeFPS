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
    public int gold { get; set; }
    public Action hpEvent;
    public Action<int> goldEvent;
    public ObjPool arrowPool;
    //public GameObject aimInfo;
    public LinkedList<GameObject> attackRangeMonsterList = new LinkedList<GameObject>();
    public Coroutine attackCoroution;
    public GameObject arrowPrefab;
    public Rigidbody rb;
    public CapsuleCollider cl;
    public Animator animator { get; set; }
    public Vector3 moveDir;
    public float turnSpeed { get; private set; }
    public int arrowCount;

    public float speedParameter;
    public bool isMouseLeftPress;
    public bool isAttacking;
    public bool isDie;

    public LayerMask groundLayer; 
    GameObject arrowTarget;
    public Vector3 camForward;
    public Vector3 camRight;
    public Vector3 moveDirection;
    public Vector2 currentInput;
    public AttackRange attackRange;
    public PlayerState playerState { get; private set; }
    public PlayerDie playerDie { get; private set; }
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
        Cursor.visible = false;
        Time.timeScale = 1f;
        UiManager.instance.isPauseMenu = false;
        Cursor.lockState = CursorLockMode.Locked;
        maxHp = 100;
        currentHp = maxHp;
        gold = 0;
        isAttacking = false;
        playerIdle = new PlayerIdle();
        playerDie = new PlayerDie();    
        playerAttackMove = new PlayerAttackMove();
        playerAttackIdle = new AttackIdle();
        playerMove = new PlayerMove();
        playerJump = new PlayerJump();
        isMouseLeftPress = false;
        turnSpeed = 10f;
        cl = GetComponent<CapsuleCollider>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        arrowCount = 2;
    }

    void Start()
    {
        ChangeState(playerIdle);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            UiManager.instance.PauseMenu();
        }
        playerState.Update(this);
    }

    private void FixedUpdate()
    {
        CarmeraStandard();
        playerState.FixedUpdate(this);
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
        int walkSpeed = 0;

        rb.velocity = transform.TransformDirection(Vector3.forward * walkSpeed);
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
        arrowTarget = attackRange.proximateMonster.gameObject;
        for (int i = 0; i < arrowCount; i++)
        {
            var temp = arrowPool.OnActive("arrow", arrowPrefab);
            temp.transform.position = transform.position;
            temp.GetComponent<ArrowController>().target = arrowTarget;
            yield return new WaitForSeconds(0.2f);
        }
    }
    public void DecreaseHp(int damage)
    {
        currentHp -= damage;
        hpEvent?.Invoke();
    }
    public void DieAnimationEvent()
    {
        cl.height = 0;
        cl.center = new Vector3(0, 1, 0);
        isDie = true;
    }
    public void ChangeGold()
    {
        goldEvent?.Invoke(gold);
    }
    public void IncreaseGold(int gold)
    {
        this.gold += gold;
        ChangeGold();
    }
    public void DecreaseGold(int price)
    {
        gold -= price;
        ChangeGold();
    }
}
