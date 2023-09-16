using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using BTArchitecture;


public class StopAgentNode : Node
{
    // stops the agent and automatically returns success
    private NavMeshAgent _agent;

    public StopAgentNode(UnityEngine.AI.NavMeshAgent agent) {
        _agent = agent;
    }

    public override NodeState Evaluate() {
        _agent.velocity = Vector3.zero;
        _agent.isStopped = true;
        return NodeState.SUCCESS;
    }
}
