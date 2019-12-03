using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float runSpeed = 8f;
    public float walkSpeed = 6f;
    public float gravity = -40f;
    public float jumpHeight = 15f;
    public float groundRayDistance = 1.1f;
    public LayerMask groundLayer;
    public float currentSpeed;

    private CharacterController controller;
    private Vector3 motion;
    private bool isJumping = false;

    public GameObject player;
    public Transform currentPosition;

    public int playerRoomValue;

    public BoxCollider[] roomDetection;
    public int currentRoom;

    public Camera cam;
    public LayerMask interactLayer;

    public Transform lastPosition;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        currentSpeed = walkSpeed;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        currentPosition = transform;
        float inputH = Input.GetAxis("Horizontal");
        float inputV = Input.GetAxis("Vertical");
        // float w = Input.GetAxis("Mouse ScrollWheel");
        bool inputJump = Input.GetButtonDown("Jump");
        Move(inputH, inputV);
        //toggle Sprint
        //on
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            currentSpeed = runSpeed;

        }
        //off
        if (Input.GetKey(KeyCode.LeftShift))
        {
            lastPosition.position = player.transform.position;
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            currentSpeed = walkSpeed;
            lastPosition.position = player.transform.position;

        }
        //applies motion to controller
        motion.y += gravity * Time.deltaTime;
        controller.Move(motion * Time.deltaTime);
    }

    public void Move(float inputH, float inputV)
    {
        Vector3 direction = new Vector3(inputH, 0f, inputV);

        direction = transform.TransformDirection(direction);

        motion.x = direction.x * currentSpeed;
        motion.z = direction.z * currentSpeed;
    }

    public void OnTriggerEnter(Collider other)
    {
        //detects which room the player is in
        if (other.CompareTag("Room"))
        {
            //searches through collider array
            for (int i = 0; i < roomDetection.Length; i++)
            {
                //if collider is in the array we are searching through
                if (other == roomDetection[i])
                {
                    //set room value to colliders value
                    playerRoomValue = i;
                    Debug.Log(playerRoomValue);
                    return;
                }
            }
        }
    }
}
