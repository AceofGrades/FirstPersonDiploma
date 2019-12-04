using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    public Animator anim;
    public Door door;
    public bool PlayerInElevator = false;
    public bool closed;
    void Start()
    {

    }

    void Update()
    {
        if(!closed)
        {
            if (door.doorOpened == true && PlayerInElevator == true)
            {
                anim.SetTrigger("Close");
                closed = true;
                return;
            }
        }
       
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerInElevator = true;
        }
    }
}
