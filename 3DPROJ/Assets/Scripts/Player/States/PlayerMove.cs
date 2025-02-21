using System.Collections;
using System.Collections.Generic;
using UnityEditor.VersionControl;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class PlayerMove : PlayerState
{
    float walkSpeed = 3f;
    float runSpeed = 1f;
    float testNum = 0;
    public override void Enter(PlayerController player)
    {
        Debug.Log("MoveState");
    }
    public override void FixedUpdate(PlayerController player)
    {
        player.LookRotation();
        PlayerMovement(player);
    }

    public override void Update(PlayerController player)
    {
        if (player.moveDir == Vector3.zero)
        {
            player.ChangeState(player.playerIdle);
            return;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            player.ChangeState(player.playerJump);
            return;
        }
        if (Input.GetKey(KeyCode.LeftShift))
        {
            runSpeed = 2f;
            testNum = SmoothAnimation(player, testNum, 1);
        }
        else
        {
            runSpeed = 1f;
            testNum = SmoothAnimation(player, testNum, 0.5f);
        }
        if (Input.GetMouseButtonDown(0))
        {
            if (player.attackRange.inRangeMonsterList.Length > 0)
            {
                player.attackCoroution = player.StartCoroutine(player.Shot());
                player.AttackAnimationOn();
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            if (player.attackCoroution != null)
            {
                player.StopCoroutine(player.attackCoroution);
            }
            player.AttackAnimationFalse();

        }

    }
    public void PlayerMovement(PlayerController player)
    {
        player.rb.velocity = player.transform.TransformDirection(new Vector3(0, player.rb.velocity.y, walkSpeed * runSpeed));
    }
    public float SmoothAnimation(PlayerController player, float speedParameterS, float speedParameterE)
    {
        var temp = Mathf.Lerp(speedParameterS, speedParameterE, 0.05f);
        player.animator.SetFloat("Speed", temp);
        return temp;
    }
}
