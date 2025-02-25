using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackIdle : PlayerState
{
    public override void Enter(PlayerController player)
    {
        //Debug.Log("AttackIdle");
        if (player.attackCoroution == null)
        {
            player.attackCoroution = player.StartCoroutine(player.Shot());
        }
        player.animator.SetBool("IsAttack",true);
        player.transform.localRotation = Quaternion.LookRotation(player.camForward);
    }

    public override void FixedUpdate(PlayerController player)
    {
        //Debug.Log(player.attackRange.proximateMonster.name);
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
        {
            if (player.moveDir != Vector3.zero)
            {
                player.ChangeState(player.playerAttackMove);
            }
        }
        if (player.isAttacking == false)
        {
            if (player.attackCoroution != null)
            {
                player.StopCoroutine(player.attackCoroution);
                player.attackCoroution = null;
            }
            player.animator.SetBool("IsAttack", false);
            player.ChangeState(player.playerIdle);
        }
        if (player.attackRange.proximateMonster == null)
        {
            player.isAttacking = false;
        }
    }

    public override void Update(PlayerController player)
    {
    }
}