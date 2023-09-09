using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BTArchitecture;

class CohesionNode : Node
{
    private float _cohesionFactor;
    private GoonBT _behaviorTree;
    private GameObject _myEnemy;

    public CohesionNode(GoonBT behaviorTree, float cohesionFactor) {
        _behaviorTree = behaviorTree;
        _cohesionFactor = cohesionFactor;
        _myEnemy = behaviorTree.gameObject;
    }

    public override NodeState Evaluate() {
        Vector3 deltaV = Vector3.zero;
        foreach (GameObject enemyObject in _behaviorTree.VisionEnemies) {
            deltaV += enemyObject.transform.position - _myEnemy.transform.position;
        }
        
        if (_behaviorTree.VisionEnemies.Count > 0) {
            deltaV /= _behaviorTree.VisionEnemies.Count;
        }
        _behaviorTree.totalDeltaV += deltaV * _cohesionFactor;
        return NodeState.SUCCESS;
    }
}