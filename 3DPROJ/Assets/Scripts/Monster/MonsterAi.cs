using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterAi : MonoBehaviour
{
    public Transform player; // 플레이어 위치
    private NavMeshAgent agent;
    bool isDie;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        StartCoroutine(PlayerFind());
    }

    IEnumerator PlayerFind()
    {
        while (isDie == false)
        {
            if (player != null)
            {
                agent.SetDestination(player.position);
            }
            yield return new WaitForSeconds(1f);
        }
    }
}
