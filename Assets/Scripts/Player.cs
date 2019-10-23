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
    public int curWeapon;
    public GameObject activeWeapon;
    public Transform[] equipment;
    public Animator arm;

    private CharacterController controller;
    private Vector3 motion;
    private bool isJumping = false;
    private bool isSwitching;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        currentSpeed = walkSpeed;
        isSwitching = true;
    }

    void Update()
    {
        float inputH = Input.GetAxis("Horizontal");
        float inputV = Input.GetAxis("Vertical");
        float w = Input.GetAxis("Mouse ScrollWheel");
        bool inputJump = Input.GetButtonDown("Jump");
        Move(inputH, inputV);
        //jump
        if(IsGrounded() && (Input.GetButtonDown("Jump")))
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
        }
        //applies motion to controller
        motion.y += gravity * Time.deltaTime;
        controller.Move(motion * Time.deltaTime);

        if(w > 0f)
        {
            curWeapon = curWeapon + 1;
            arm.SetBool("weaponSwap", true);
            isSwitching = true;

        }
        else if(w < 0f)
        {
            curWeapon = curWeapon - 1;

            isSwitching = true;
        }
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

    public void WeaponCycle(int num)
    {
        curWeapon = num;
        for (int i = 0; i < equipment.Length; i++)
        {
            if (i == num)
                equipment[i].gameObject.SetActive(true);
            else
                equipment[i].gameObject.SetActive(false);
        }
    }
}
