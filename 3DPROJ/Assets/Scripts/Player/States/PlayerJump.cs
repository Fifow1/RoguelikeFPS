using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : PlayerState
{
    bool jumpPossible = false;
    bool isJump = false;
    float jumpCoolTime = 2f;
    public override void Enter(PlayerController player)
    {
        JumpRay(player);
        if (jumpPossible == true && isJump == false)
        {
            player.rb.AddForce(new Vector3(player.rb.velocity.x, 5,player.rb.velocity.z), ForceMode.Impulse);
            player.animator.SetTrigger("Jump");
            isJump = true;
            player.StartCoroutine(JumpCoolDown(player));
        }
    }

    public override void FixedUpdate(PlayerController player)
    {
    }

    public override void Update(PlayerController player)
    {
        if (player.currentHp <= 0)
        {
            player.ChangeState(player.playerDie);
        }
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
        {
            player.ChangeState(player.playerMove);
        }
        else
        {
            player.ChangeState(player.playerIdle);
        }
    }
    void JumpRay(PlayerController player)
    {
        RaycastHit hit;
        if (Physics.Raycast(player.transform.position, Vector3.down, out hit, 1f))
        {
            jumpPossible = true;
        }
        else
        {
            jumpPossible = false;
        }
    }
    IEnumerator JumpCoolDown(PlayerController player)
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
}
