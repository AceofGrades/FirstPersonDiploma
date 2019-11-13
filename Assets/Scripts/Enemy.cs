using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    #region Variables

    Player player;

    public GameObject enemy;

    public Transform waypointParent;
    public float moveSpeed = 2f;
    public float rotateSpeed = 100f;
    public float stoppingDistance = 1f;
    public float gravityDistance = 2f;
    public Rigidbody rigid;

    private Transform[] waypoints;
    private int currentIndex = 1;
    private NavMeshAgent agent;

    public BoxCollider[] roomDetection;
    public int currentRoom;
    public Transform[] vents;
    public int currentVent;

    public enum State
    {
        Patrol,
        Seek,
        VentTeleport,
        Idle
    }
    public State currentState;
    private Transform target;

    public float playerDetectionTimer = 0f;
    public float playerDetectionLimit = 50f;

    public int teleportToRoom;

    #endregion Variables

    #region Start
    void Start()
    {
        if (!player) player = FindObjectOfType<Player>();

        waypoints = waypointParent.GetComponentsInChildren<Transform>();
        agent = GetComponent<NavMeshAgent>();
        currentState = State.Idle;
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
            case State.Idle:
                Idle();
                break;
            default:
                break;
        }
    }
    #endregion


    #region Seek
    void Seek()
    {
        agent.SetDestination(player.lastPosition.position);
        if (enemy.transform == player.lastPosition)
        {
            currentState = State.Idle;
        }
    }
    #endregion

    #region Patrol
    void Patrol()
    {
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
        agent.SetDestination(target.position);
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
        if (other.CompareTag("Room"))
        {
            for (int i = 0; i < roomDetection.Length; i++)
            {
                if (other == roomDetection[i])
                {
                    int roomValue = i;
                    Debug.Log("enemy in room " + roomValue);
                    return;
                }
            }
        }
    }
    #endregion

    #region TriggerExit
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerDetectionTimer = 0f;
            currentState = State.Idle;
            target = player.lastPosition;
        }
    }
    #endregion

    void Idle()
    {
        if (player.currentPosition != player.lastPosition)
        {
            currentState = State.Seek;
        }
    }
}