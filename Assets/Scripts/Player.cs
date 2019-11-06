using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Room
{
    public float runSpeed = 8f;
    public float walkSpeed = 6f;
    public float gravity = -40f;
    public float jumpHeight = 15f;
    public float groundRayDistance = 1.1f;
    public LayerMask groundLayer;
    public float currentSpeed;
    // public int curWeapon;
    // public GameObject activeWeapon;
    // public Transform[] equipment;
    // public Animator arm;

    private CharacterController controller;
    private Vector3 motion;
    private bool isJumping = false;

    public Transform player;


    public BoxCollider[] roomDetection;
    public int currentRoom;

    public Camera cam;
    public LayerMask layerMask;
    // private bool isSwitching;

    public Transform lastPosition;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        currentSpeed = walkSpeed;
        Cursor.lockState = CursorLockMode.Locked;
        // isSwitching = true;
    }

    void Update()
    {
        float inputH = Input.GetAxis("Horizontal");
        float inputV = Input.GetAxis("Vertical");
        // float w = Input.GetAxis("Mouse ScrollWheel");
        bool inputJump = Input.GetButtonDown("Jump");
        Move(inputH, inputV);
        //jump
        if (IsGrounded() && (Input.GetButtonDown("Jump")))
        {
            Jump(jumpHeight);
        }
        if (!IsGrounded() && isJumping)
        {
            isJumping = false;
        }
        if (IsGrounded() && !isJumping)
        {
            motion.y = 0f;
        }
        //toggle Sprint
        //on
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            currentSpeed = runSpeed;

        }
        //off
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            currentSpeed = walkSpeed;
            lastPosition.position = transform.position;

        }
        //applies motion to controller
        motion.y += gravity * Time.deltaTime;
        controller.Move(motion * Time.deltaTime);

        /*if(w > 0f)
        {
            curWeapon = curWeapon + 1;
            arm.SetTrigger("WeaponSwap");
            isSwitching = true;

        }
        else if(w < 0f)
        {
            curWeapon = curWeapon - 1;
            arm.SetTrigger("WeaponSwap");
            isSwitching = true;
        }
        if (curWeapon < 0)
        {
            curWeapon = equipment.Length - 1;
        }
        if (curWeapon > equipment.Length - 1)
        {
            curWeapon = 0;
        }
        */
    }

    //test if the player is grounded
    private bool IsGrounded()
    {
        Ray groundRay = new Ray(transform.position, -transform.up);
        //preforming raycast
        if (Physics.Raycast(groundRay, groundRayDistance))
        {
            return true;// exits function
        }
        return false;//exits function
        //smaller version ^
        //return Physics.Raycast(transform.position, -transform.up, groundRayDistance);
    }

    public void Move(float inputH, float inputV)
    {
        Vector3 direction = new Vector3(inputH, 0f, inputV);

        direction = transform.TransformDirection(direction);

        motion.x = direction.x * currentSpeed;
        motion.z = direction.z * currentSpeed;
    }

    public void Jump(float height)
    {
        motion.y = jumpHeight;
        isJumping = true;
    }

    public void InteractCamera()
    {
        RaycastHit hit;
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, 1f, layerMask))
        {

        }
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
                    roomValue = i;
                    Debug.Log(roomValue);
                    return;
                }
            }
        }
    }

    /*public void WeaponCycle()
    {
       
        for (int i = 0; i < equipment.Length; i++)
        {
            if (i == curWeapon)
            {
                activeWeapon = equipment[i].gameObject;
                activeWeapon.SetActive(true);
            }              

            else
            {
                equipment[i].gameObject.SetActive(false);
            }
        }

    }
    */
}
