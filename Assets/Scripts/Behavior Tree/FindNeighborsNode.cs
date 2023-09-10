using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using BTArchitecture;

public class FindNeighborsNode : Node
{
    private GoonBT _behaviorTree;
    private float _visionRadius;
    private float _separationRadius;

    public FindNeighborsNode(GoonBT behaviorTree, float visionRadius, float separationRadius) {
        _behaviorTree = behaviorTree;
        _visionRadius = visionRadius;
        _separationRadius = separationRadius;
    }

    public override NodeState Evaluate() {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        List<GameObject> enemiesInSeparaionRange = new List<GameObject>();
        List<GameObject> enemiesInVisionRange = new List<GameObject>();
        GameObject thisEnemy = _behaviorTree.gameObject;

        foreach (GameObject enemy in enemies) {
            if (enemy.Equals(thisEnemy)) {
                continue;
            }

            NavMeshAgent agent = enemy.GetComponent<NavMeshAgent>();
            float distance = Vector3.Distance(enemy.transform.position, thisEnemy.transform.position);
            if (distance < _visionRadius && agent != null) {
                enemiesInVisionRange.Add(enemy);
            }
            if (distance < _separationRadius) {
                enemiesInSeparaionRange.Add(enemy);
            }
        }

        if (enemiesInVisionRange.Count == 0 && enemiesInSeparaionRange.Count == 0) {
            return NodeState.FAILURE;
        }

        _behaviorTree.SeparationEnemies = enemiesInSeparaionRange;
        _behaviorTree.VisionEnemies = enemiesInVisionRange;
        return NodeState.SUCCESS;
    }
}
