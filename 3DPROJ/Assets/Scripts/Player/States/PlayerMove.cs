using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : PlayerState
{
    float walkSpeed = 3f;
    float runSpeed = 1f;
    float testNum = 0;
    
    public override void Enter(PlayerController player)
    {
    }
    public override void FixedUpdate(PlayerController player)
    {
        player.LookRotation();
        PlayerMovement(player);
    }

    public override void Update(PlayerController player)
    {
        if (player.currentHp <= 0)
        {
            player.ChangeState(player.playerDie);
        }
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
        if (player.isAttacking == true && player.attackRange.proximateMonster != null)
        {
            player.ChangeState(player.playerAttackMove);
        }
        #region
        //if (Input.GetMouseButtonDown(0))
        //{
        //    if (player.attackRange.inRangeMonsterList.Length > 0)
        //    {
        //        player.transform.localRotation = Quaternion.LookRotation(player.camForward);
        //        player.isAttacking = true;
        //        player.attackCoroution = player.StartCoroutine(player.Shot());
        //        player.AttackAnimationOn();
        //    }
        //}
        //if (Input.GetMouseButtonUp(0))
        //{
        //    if (player.attackCoroution != null)
        //    {
        //        player.isAttacking = false;
        //        player.StopCoroutine(player.attackCoroution);
        //    }
        //    player.AttackAnimationFalse();
        //
        //}
        //
        #endregion
    }
    public void PlayerMovement(PlayerController player)
    {
        player.rb.velocity = player.transform.TransformDirection(Vector3.forward*3);
    }
    public float SmoothAnimation(PlayerController player, float speedParameterS, float speedParameterE)
    {
        var temp = Mathf.Lerp(speedParameterS, speedParameterE, 0.05f);
        player.animator.SetFloat("Speed", temp);
        return temp;
    }
}
