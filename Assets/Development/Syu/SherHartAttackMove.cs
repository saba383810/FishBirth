using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// ’Ç‰Á
using UnityEngine.AI;

public class SherHartAttackMove : MonoBehaviour
{
    public GameObject target;
    private NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (target)
        {
            agent.destination = target.transform.position;
        }
    }
}
