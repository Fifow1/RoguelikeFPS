using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class PlayerState
{
    public abstract void Enter(PlayerController player);
    public abstract void Update(PlayerController player);
    public abstract void FixedUpdate(PlayerController player);

}

public class PlayerIdle : PlayerState
{
    public override void Enter(PlayerController player)
    {
        Debug.Log("IdleState");
        player.animator.SetFloat("Speed", 0f);
        player.transform.localRotation = Quaternion.LookRotation(player.camForward);
    }

    public override void FixedUpdate(PlayerController player)
    {
    }

    public override void Update(PlayerController player)
    {
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
        {
            if (player.moveDir != Vector3.zero)
            {
                player.ChangeState(player.playerMove);
            }
        }
        if (Input.GetKey(KeyCode.Space))
        {
            player.ChangeState(player.playerJump);
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
            //player.StopCoroutine(player.ArrowCreate());

            //player.StopCoroutine(player.testCOr1);
            //player.StopCoroutine(player.testCOr2);
            player.AttackAnimationFalse();

        }
    }

}