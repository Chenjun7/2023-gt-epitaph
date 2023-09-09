using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using BTArchitecture;

public class GoonBT : BehaviorTree
{
    public List<GameObject> VisionEnemies;
    public List<GameObject> SeparationEnemies;
    public Vector3 totalDeltaV;
    [SerializeField] private float visionRadius;
    [SerializeField] private float collisionRadius;
    [SerializeField] private float separationFactor;
    [SerializeField] private float cohesionFactor;
    [SerializeField] private float alignmentFactor;

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
                    new SeparationNode(this, separationFactor),
                    new CohesionNode(this, cohesionFactor),
                    new AlignmentNode(this, alignmentFactor),
                    new ApplyDeltaV(this, agent)
                })
            })
        });
        
        //Node root = new FollowPlayerNode(target, agent, speed);
        
        return root;
    }
}
