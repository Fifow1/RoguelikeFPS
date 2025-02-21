using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Playables;

public class PlayerController : MonoBehaviour
{
    public ArrowObjPool arrowPool;
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

    public LayerMask groundLayer;
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

    public void ChangeState(PlayerState playerState)
    {
        this.playerState = playerState;
        playerState.Enter(this);
    }
    private void Awake()
    {
        playerIdle = new PlayerIdle();
        playerMove = new PlayerMove();
        playerJump = new PlayerJump();

    }

    void Start()
    {
        isMouseLeftPress = false;
        turnSpeed = 10f;
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();

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
        moveDirection = (camForward * moveDir.z + camRight * moveDir.x).normalized;
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
            yield return new WaitForSeconds(2f);
            StartCoroutine(ArrowCreate());
        }
    }
    public IEnumerator ArrowCreate()
    {
        Vector3 arrowDir = (attackRange.proximateMonster.gameObject.transform.position - transform.position).normalized;
        for (int i = 0; i < 3; i++)
        {
            var temp = arrowPool.OnActive();
            temp.transform.position = new Vector3(transform.position.x, transform.position.y + 1.5f, transform.position.z);
            temp.transform.rotation = Quaternion.LookRotation(arrowDir);   
            //Instantiate(arrowPrefab,
            //new Vector3(transform.position.x, transform.position.y + 1.5f, transform.position.z),
            //Quaternion.LookRotation(arrowDir));
            temp.GetComponent<Rigidbody>().AddForce(arrowDir*10, ForceMode.Impulse);
            Debug.Log(arrowDir);
            yield return new WaitForSeconds(0.2f);
        }
        
    }
}
