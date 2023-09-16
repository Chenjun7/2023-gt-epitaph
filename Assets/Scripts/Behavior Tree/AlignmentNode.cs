using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using BTArchitecture;

class AlignmentNode : Node
{
    private float _alignmentFactor;
    private GoonBT _behaviorTree;

    public AlignmentNode(GoonBT behaviorTree, float alignmentFactor) {
        _behaviorTree = behaviorTree;
        _alignmentFactor = alignmentFactor;
    }

    public override NodeState Evaluate() {
        Vector3 deltaV = Vector3.zero;
        foreach (GameObject enemyObject in _behaviorTree.VisionEnemies) {
            deltaV += enemyObject.GetComponent<NavMeshAgent>().velocity.normalized;
        }

        if (_behaviorTree.VisionEnemies.Count > 0) {
            deltaV /= _behaviorTree.VisionEnemies.Count;
        }
        _behaviorTree.totalDeltaV += deltaV * _alignmentFactor;
        return NodeState.SUCCESS;
    }
}