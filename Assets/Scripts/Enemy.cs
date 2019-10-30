using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    #region Variables

    public Transform waypointParent;
    public float moveSpeed = 9f;
    public float rotateSpeed = 100f;
    public float stoppingDistance = 1f;
    public float gravityDistance = 2f;
    public Rigidbody rigid;

    private Transform[] waypoints;
    private int currentIndex = 1;
    private NavMeshAgent agent;
    public enum State
    {
        Patrol,
        Seek,
        Vent
    }
    public State currentState;
    private Transform target;



    public float playerDetectionTimer = 0f;
    public float playerDetectionLimit = 10f;

    public Room room;

    #endregion Variables

    #region Start
    void Start()
    {
        waypoints = waypointParent.GetComponentsInChildren<Transform>();
        agent = GetComponent<NavMeshAgent>();
        currentState = State.Patrol;
    }
    #endregion

    #region Update
    void Update()
    {
        playerDetectionTimer += Time.deltaTime;
        switch (currentState)
        {
            case State.Patrol:
                Patrol();
                break;
            case State.Seek:
                Seek();
                break;
            case State.Vent:

                break;
            default:
                break;
        }
    }
    #endregion

    #region Gizmos
    void OnDrawGizmos()
    {
        if (waypoints != null && waypoints.Length > 0)
        {
            Transform point = waypoints[currentIndex];
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, point.position);
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(point.position, stoppingDistance);
        }
    }
    #endregion

    #region Seek
    void Seek()
    {
        agent.SetDestination(target.position);
    }
    #endregion

    #region Patrol
    void Patrol()
    {
        if (playerDetectionTimer >= playerDetectionLimit)
        {
            VentTeleport();
        }
        Transform point = waypoints[currentIndex];
        float distance = Vector3.Distance(transform.position, point.position);
        if (distance < stoppingDistance)
        {
            currentIndex++;
        }
        if (currentIndex >= waypoints.Length)
        {
            currentIndex = 1;
        }
        agent.SetDestination(point.position);
        transform.position = Vector3.MoveTowards(transform.position, point.position, moveSpeed * Time.deltaTime);
    }
    #endregion

    #region TriggerEnter
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            target = other.transform;
            currentState = State.Seek;
        }
    }
    #endregion

    #region TriggerExit
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            currentState = State.Patrol;
        }
    }
    #endregion

    void VentTeleport()
    {
        playerDetectionTimer = 0f;
    }
}