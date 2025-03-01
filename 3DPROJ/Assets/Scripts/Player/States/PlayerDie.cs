using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerDie : PlayerState
{
    public override void Enter(PlayerController player)
    {
        player.animator.SetTrigger("IsDie");
        player.StartCoroutine(DiePopup(player));
    }

    IEnumerator DiePopup(PlayerController player)
    {
        yield return new WaitUntil(()=> player.isDie == true);
        UiManager.instance.OnGameOverUi();
    }
    public void DieAnimationEvent(PlayerController player)
    {
        player.cl.height = 0;
        player.cl.center = new Vector3(0,1,0);
        player.isDie = true;
    }

    public override void Update(PlayerController player)
    {
    }

    public override void FixedUpdate(PlayerController player)
    {
    }
}
