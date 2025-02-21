using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterAi : MonoBehaviour
{
    public Transform player; // �÷��̾� ��ġ
    private NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (player != null)
        {
            agent.SetDestination(player.position);
        }
    }
}
