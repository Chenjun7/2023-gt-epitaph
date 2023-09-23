using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CustomNavMeshAgent : MonoBehaviour
{
    public float speed;
    public float angularSpeed;
    public float acceleration;
    public bool isStopped;

    [Tooltip("")]
    [SerializeField] private float navigationFactor;
    [SerializeField] private float separationFactor;
    [SerializeField] private float cohesionFactor;
    [SerializeField] private float alignmentFactor;
    [SerializeField] private float separationDistance;
    [SerializeField] private float neighborDistance;

    private new Rigidbody rigidbody;
    private Vector3 destination;
    private NavMeshPath path;
    private Vector3 totalSteering;

    void Start()
    {
        destination = transform.position;
        path = new NavMeshPath();
        rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isStopped) return;
        totalSteering = Vector3.zero;
        totalSteering += CalculateNavigation() * navigationFactor;

    }

    private Vector3 CalculateNavigation() {
        NavMesh.CalculatePath(transform.position, destination, NavMesh.AllAreas, path);
        return path.corners[0] - transform.position;
    }

    private Vector3 CalculateSeparaion() {
        return Vector3.zero;
    }
}
