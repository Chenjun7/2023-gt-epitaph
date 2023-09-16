using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BTArchitecture;

class SeparationNode : Node
{
    private float _separationFactor;
    private GoonBT _behaviorTree;
    private GameObject _myEnemy;

    public SeparationNode(GoonBT behaviorTree, float separationFactor) {
        _behaviorTree = behaviorTree;
        _separationFactor = separationFactor;
        _myEnemy = behaviorTree.gameObject;
    }

    public override NodeState Evaluate() {
        Vector3 deltaV = Vector3.zero;
        foreach (GameObject enemyObject in _behaviorTree.SeparationEnemies) {
            deltaV -= enemyObject.transform.position - _myEnemy.transform.position;
        }

        if (_behaviorTree.SeparationEnemies.Count > 0) {
            deltaV /= _behaviorTree.SeparationEnemies.Count;
        }
        _behaviorTree.totalDeltaV += deltaV * _separationFactor;
        return NodeState.SUCCESS;
    }
}