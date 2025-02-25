using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackMove : PlayerState
{
    public override void Enter(PlayerController player)
    {
        if (player.attackCoroution == null)
        {
            player.attackCoroution = player.StartCoroutine(player.Shot());
        }
        player.animator.SetBool("IsAttack",true);
        player.transform.localRotation = Quaternion.LookRotation(player.camForward);
    }

    public override void FixedUpdate(PlayerController player)
    {
        AttackingMovemet(player);
        player.animator.SetFloat("X",player.moveDir.x);
        player.animator.SetFloat("Y", player.moveDir.z);
        if (player.isAttacking == false)
        {
            if (player.attackCoroution != null)
            {
                player.StopCoroutine(player.attackCoroution);
                player.attackCoroution = null;
            }
            player.animator.SetBool("IsAttack", false);
            if (player.moveDir == Vector3.zero)
            {
                player.ChangeState(player.playerIdle);
            }
            else
            {
                player.ChangeState(player.playerMove);
            }
        }
        if (player.moveDir == Vector3.zero)
        {
            player.ChangeState(player.playerAttackIdle);
        }
        if (player.attackRange.proximateMonster == null)
        {
            player.isAttacking = false;
        }
    }

    public override void Update(PlayerController player)
    {
    }
    public void AttackingMovemet(PlayerController player)
    {
        player.rb.velocity = player.transform.TransformDirection(new Vector3(player.moveDir.x *2 , player.rb.velocity.y, player.moveDir.z *2));
    }
    public void AttactingLookRotation(PlayerController player)
    {

    }
}
