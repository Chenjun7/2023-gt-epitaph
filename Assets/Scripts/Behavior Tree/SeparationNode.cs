using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using BTArchitecture;

class SeparationNode : Node
{
    private float _separationFactor;
    private GoonBT _behaviorTree;
    private Transform _thisPosition;

    public SeparationNode(GoonBT behaviorTree, float separationFactor) {
        _behaviorTree = behaviorTree;
        _separationFactor = separationFactor;
    }

    public override NodeState Evaluate() {
        Vector3 deltaV = Vector3.zero;
        foreach (GameObject enemyObject in _behaviorTree.SeparationEnemies) {
            deltaV -= enemyObject.transform.position - _behaviorTree.transform.position;
        }

        deltaV /= _behaviorTree.SeparationEnemies.Count;
        _behaviorTree.totalDeltaV += deltaV * _separationFactor;
        return NodeState.SUCCESS
    }
}