using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

// �ȱ� 
// �޸��� 
// ����
// �ɱ�

// Ȱ ��Ŭ��
// Ȱ ��Ŭ��

// ��ų 1
// ��ų 2

public class PlayerMoveaa : MonoBehaviour
{
    Rigidbody rb;
    Animator animator;
    Vector3 moveDir;
    public float turnSpeed;
    float walkSpeed;
    float runSpeed;
    bool jumpPossible;
    public LayerMask groundLayer;

    public Transform upperBodyRoot;
    public Transform lowerBodyRoot;

    Vector2 temp;
    Vector2 ww;
    float a;

    float mX;
    float mY;

    void Start()
    {
        turnSpeed = 150f;
        runSpeed = 1f;
        walkSpeed = 4f;
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        jumpPossible = true;

        upperBodyRoot = animator.GetBoneTransform(HumanBodyBones.Chest);

        // ��ü = ��� (Hips)
        lowerBodyRoot = animator.GetBoneTransform(HumanBodyBones.Hips);
    }
    private void FixedUpdate()
    {
        rb.velocity = transform.TransformDirection(new Vector3(moveDir.x * walkSpeed * runSpeed, rb.velocity.y, moveDir.z * walkSpeed * runSpeed));
        //animator.SetBool("IsWalk", true);
        if (moveDir == Vector3.zero)
        {
            rb.velocity = new Vector3(0, rb.velocity.y, 0);
            //animator.SetBool("IsWalk", false);
        }
    }
    private void LateUpdate()
    {
        #region
        
        //upperBodyRoot.localRotation = Quaternion.LookRotation(moveDir);

        //lowerBodyRoot.parent.localRotation = Quaternion.LookRotation(moveDir);
        // ���ʹϾ��� �������� �ִϸ��̼Ǹ� ������ ����� ����Ͽ����� ��ü�� ���θ��� �޸��� ��Ƕ������� �޸��� ������
        //Quaternion additionalRotation = Quaternion.LookRotation(moveDir,new Vector3(0,1,0));
        //lowerBodyRoot.localRotation *= additionalRotation ;

        // ���� ���¸� ��ü,��ü forward ����

        // 
        //Quaternion newRotation = Quaternion.LookRotation(moveDir, Vector3.up);
        //lowerBodyRoot.localRotation = Quaternion.Slerp(lowerBodyRoot.localRotation, newRotation, Time.deltaTime * 2);
        #endregion
    }
    void Update()
    {
        mX = Input.GetAxis("Mouse X");
        transform.Rotate(Vector3.up * turnSpeed * Time.deltaTime * mX, Space.Self);

        if (moveDir != Vector3.zero)
        {
            //animator.SetFloat("X",0);
            //animator.SetFloat("Y",1);
            //this.transform.rotation = Quaternion.LookRotation(moveDir);
        }
        else
        {
            //animator.SetFloat("X", 0);
            //animator.SetFloat("Y", 0);
        }
    }

    void JumpRay()
    {

        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, 0.3f))
        {
            jumpPossible = true;
        }
        else
        {
            jumpPossible = false;
        }
    }

    public void OnMove(InputValue val)
    {
        Vector2 temp = val.Get<Vector2>();
        Debug.Log
            (temp);
        moveDir = new Vector3(temp.x, 0, temp.y);
        animator.SetFloat("X", moveDir.x, 0.1f, Time.deltaTime *100);
        animator.SetFloat("Y", moveDir.z,0.1f,Time.deltaTime * 100);
    }
    public void OnJump()
    {
        JumpRay();
        if (jumpPossible == true)
        {
            rb.AddForce(Vector3.up * 5, ForceMode.Impulse);
            animator.SetTrigger("Jump");
        }
    }
    public void OnRun(InputValue val)
    {
        bool temp = val.isPressed;
        if (temp == true)
        {
            runSpeed = 2f;
            animator.SetBool("IsRun", true);
        }
        else
        {
            animator.SetBool("IsRun", false);
            runSpeed = 1;
        }
    }
}
