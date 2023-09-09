using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using BTArchitecture;

public class GoonBT : BehaviorTree
{
    [SerializeField] private float visionRadius;
    [SerializeField] private float collisionRadius;
    [SerializeField] private float separationFactor;
    [SerializeField] private float cohesionFactor;
    [SerializeField] private float alignmentFactor;
    private List<NavMeshAgent> _visionEnemies;
    private List<NavMeshAgent> _separationEnemies;

    // Start is called before the first frame update
    protected override Node SetupTree() {
        GameObject target = GameObject.FindWithTag("Player");
        NavMeshAgent agent = GetComponent<NavMeshAgent>();

        agent.updateRotation = false;
        agent.updateUpAxis = false;

        Node root = new SelectorNode(new List<Node>{
            new SequenceNode(new List<Node>{
                new DetectPlayerNode(this.gameObject),
                new StopAgentNode(agent),
            }),
            new SequenceNode(new List<Node>{
                new FollowPlayerNode(target, agent),
                new SequenceNode(new List<Node>{
                    new FindNeighborsNode(this, visionRadius, collisionRadius),
                    new SeparationNode(this, _separationEnemies, separationFactor),
                    new CohesionNode(this, _visionEnemies, cohesionFactor),
                    new AlignmentNode(this,_visionEnemies, alignmentFactor),
                    new ApplyDeltaV(this, agent)
                })
            })
        });
        
        //Node root = new FollowPlayerNode(target, agent, speed);
        
        return root;
    }

    public void SetNeighborList(List<NavMeshAgent> visionEnemies, List<NavMeshAgent> separationEnemies) {
        _visionEnemies = visionEnemies;
        _separationEnemies = separationEnemies;
    }
}
