using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.HID;

// 걷기 
// 달리기 
// 점프
// 앉기

// 활 좌클릭
// 활 우클릭

// 스킬 1
// 스킬 2

public class PlayerMovement : MonoBehaviour
{
    Rigidbody rb;
    Animator animator;
    Vector3 moveDir;
    public float turnSpeed;

    float walkSpeed;
    bool isWalking;
    bool isRunning;
    float runSpeed;

    bool jumpPossible;
    bool isJump;
    float jumpCoolTime;

    public LayerMask groundLayer;
    Vector3 camForward;
    Vector3 camRight;
    Vector3 moveDirection;
    Vector2 currentInput;
    void Start()
    {
        jumpCoolTime = 2f;
        isJump = false;
        isWalking = false;
        isRunning = false;
        turnSpeed = 10f;
        runSpeed = 1f;
        walkSpeed = 4f;
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        jumpPossible = true;

    }
    private void FixedUpdate()
    {
        

        moveDirection = (camForward * moveDir.z + camRight * moveDir.x).normalized;
        if (moveDirection != Vector3.zero)
        {
            rb.velocity = transform.TransformDirection(new Vector3(0,rb.velocity.y, walkSpeed * runSpeed));

            animator.SetBool("IsWalk",true);

            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * turnSpeed);
        }
        else
        {
            rb.velocity = new Vector3(0, rb.velocity.y, 0);
            animator.SetBool("IsWalk",false);
            transform.localRotation = Quaternion.LookRotation(camForward);
        }
    }
    private void LateUpdate()
    {
    }
    void Update()
    {
        if (Input.GetKey(KeyCode.T))
        {
            animator.SetBool("IsAttack1" , true);
        }
        else
        {
            animator.SetBool("IsAttack1" , false);
        }
        camForward= Camera.main.transform.forward;
        camRight = Camera.main.transform.right;
        camForward.y = 0;
        camRight.y = 0;
        camForward.Normalize();
        camRight.Normalize();
        
        #region

        //    if (Input.GetKeyDown(KeyCode.Space))
        //    {
        //        JumpRayt();
        //        if (jumpPossible == true)
        //        {
        //            rb.AddForce(Vector3.up * 5,ForceMode.Impulse);
        //            animator.SetTrigger("Jump");
        //        }
        //    }

        //    if (Input.GetKey(KeyCode.LeftShift))
        //    {
        //        isRunning = true;
        //        runSpeed = 100;
        //        animator.SetBool("IsRun",true);
        //    }
        //    else
        //    {
        //        isRunning = false;
        //        animator.SetBool("IsRun", false);
        //    }
        //    if (isRunning == true)
        //    {
        //        runSpeed = 2;
        //    }
        //    if (isWalking == true)
        //    {
        //        runSpeed = 1;
        //    }
        #endregion
    }


    public void OnMove(InputValue val)
    {

        Vector2 temp = val.Get<Vector2>();
        moveDir = new Vector3(temp.x,0,temp.y);
        Debug.Log(moveDir);
    }
    void JumpRay()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, 1f)) { 
            jumpPossible = true;
        }
        else
        {
            jumpPossible = false;
        }
    }
    public void OnJump()
    {
        JumpRay();
        if (jumpPossible == true && isJump == false)
        {
            rb.AddForce(Vector3.up * 5, ForceMode.Impulse);
            animator.SetTrigger("Jump");
            isJump = true;
            StartCoroutine(JumpCoolDown());
        }
    }
    IEnumerator JumpCoolDown()
    {
        float a = 0;
        while (isJump == true) 
        {
            a += Time.deltaTime;
            if (a >= jumpCoolTime)
            {
                isJump = false;
            }
            yield return null;
        }
    }


    public void OnRun(InputValue val)
    {
        bool temp = val.isPressed;
        if (temp == true)
        {
            runSpeed = 2f;
            animator.SetBool("IsRun",true);
        }
        else
        {
            animator.SetBool("IsRun",false);
            runSpeed = 1;
        }
    }
}
