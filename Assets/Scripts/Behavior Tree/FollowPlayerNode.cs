using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using BTArchitecture;

public class FollowPlayerNode : Node
{
    
    // follows the player and always returns success

    private GameObject _target;
    private NavMeshAgent _agent;

    public FollowPlayerNode(GameObject target, UnityEngine.AI.NavMeshAgent agent) {
        _target = target;
        _agent = agent;
    }

    public override NodeState Evaluate() {
        _agent.isStopped = false;
        _agent.SetDestination(_target.transform.position);
        return NodeState.SUCCESS;
    }
}
