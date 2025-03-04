using System.Collections;
using UnityEngine;

public class PlayerDie : PlayerState
{
    public override void Enter(PlayerController player)
    {
        player.StopAllCoroutines();
        player.animator.SetTrigger("IsDie");
        player.StartCoroutine(DiePopup(player));
    }

    IEnumerator DiePopup(PlayerController player)
    {
        yield return new WaitUntil(()=> player.isDie == true);
        UiManager.instance.OnGameOverUi();
    }

    public override void Update(PlayerController player)
    {
    }

    public override void FixedUpdate(PlayerController player)
    {
    }
}
