using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace AI
{
    public class Enemy : MonoBehaviour
    {
        #region Variables
        [Header("Variables:")]
        //parent of all waypoints
        public Transform waypointParent;
        //enemy movement speed
        public float moveSpeed = 9f;
        //enemy rotation speed
        public float rotateSpeed = 100f;
        //enemy stopping distance
        public float stoppingDistance = 1f;
        //enemy gravity distance
        public float gravityDistance = 2f;
        //enemy's rigidbody
        public Rigidbody rigid;

        //array of waypoints
        private Transform[] waypoints;
        //current index number in array
        private int currentIndex = 1;
        //navmesh for enemy too see
        private NavMeshAgent agent;
        //enum of a seek state and patrol state
        public enum State
        {
            Patrol,
            Seek
        }
        //current state for enum
        public State currentState;
        //player's transform
        private Transform target;
        #endregion Variables

        #region Start
        void Start()
        {
            // Get the childern from waypointParent
            waypoints = waypointParent.GetComponentsInChildren<Transform>();
            // Get the AI component
            agent = GetComponent<NavMeshAgent>();
            //just in case make state patrol
            currentState = State.Patrol;
        }
        #endregion

        #region Update
        void Update()
        {
            switch (currentState)
            {
                //sets state to patrol
                case State.Patrol:
                    Patrol();
                    break;
                //sets state to seek
                case State.Seek:
                    Seek();
                    break;
                //sets state to default
                default:
                    break;
            }
        }
        #endregion

        #region Gizmos
        void OnDrawGizmos()
        {
            // If waypoints is not null AND waypoints is not empty
            if (waypoints != null && waypoints.Length > 0)
            {
                // Get current waypoint
                Transform point = waypoints[currentIndex];
                // Draw line from position to waypoint
                Gizmos.color = Color.red;
                Gizmos.DrawLine(transform.position, point.position);
                // Draw stopping distance sphere
                Gizmos.color = Color.blue;
                Gizmos.DrawWireSphere(point.position, stoppingDistance);
                // Draw gravity sphere
            }
        }
        #endregion

        #region Seek
        void Seek()
        {
            //sets destination to player's position
            agent.SetDestination(target.position);
        }
        #endregion

        #region Patrol
        void Patrol()
        {
            //get the current waypoint
            Transform point = waypoints[currentIndex];
            //get diestance from waypoint
            float distance = Vector3.Distance(transform.position, point.position);
            //if distance is less than stopping distance
            if (distance < stoppingDistance)
            {
                //move to next waypoint
                currentIndex++;
            }
            //resets array index when it reaches the end
            if (currentIndex >= waypoints.Length)
            {
                //sets current index to 1 in array
                currentIndex = 1;
            }
            //use NavMeshAgent to follow the current waypoint
            agent.SetDestination(point.position);
            //translate Enemy towards current waypoint
            #region Look then walk
            //direction = Target - Current
            Vector3 desiredDirection = point.position - transform.position;
            //generate a quaternion rotation
            Quaternion finalRotation = Quaternion.LookRotation(desiredDirection);
            //smooth rotation
            transform.rotation = Quaternion.RotateTowards(transform.rotation, finalRotation, rotateSpeed * Time.deltaTime);
            #endregion
            //if looking in direction of waypoint the enemy is allowed to move
            Vector3 currentDirection = transform.forward;
            if (Vector3.Angle(currentDirection, desiredDirection) < 1f)
            {
                transform.position = Vector3.MoveTowards(transform.position, point.position, moveSpeed * Time.deltaTime);
            }
        }
        #endregion

        #region TriggerEnter
        private void OnTriggerEnter(Collider other)
        {
            //if other gameobject is player
            if (other.gameObject.CompareTag("Player"))
            {
                //set target to the thing in zone
                target = other.transform;
                //switch state to seek
                currentState = State.Seek;
            }

        }
        #endregion

        #region TriggerExit
        private void OnTriggerExit(Collider other)
        {
            //if other gameobject is player
            if (other.gameObject.CompareTag("Player"))
            {
                //switch state to patrol
                currentState = State.Patrol;
            }
        }
        #endregion
    }
}

