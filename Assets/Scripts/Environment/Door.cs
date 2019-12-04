using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public GameObject keyPadParent;
    public Animator anim;
    public Keypad keyPad;

    public bool keycodeRequired;
    public bool keycardRequired;
    public bool doorOpened = false;

    void Update()
    {
        if (doorOpened == false)
        {
            if (keycodeRequired == true && keycardRequired == false)
        {
            if (keyPad.isUnlocked == true)
            {
                
                    anim.Play("door");
                    doorOpened = true;
                    return;
                }
            }
        }
       /* else if (keycodeRequired == false && keycardRequired == true)
        {

        }
        else if (keycodeRequired == true && keycardRequired == true)
        {
            if (keyPad.isUnlocked == true)
            {
                if (doorOpened == false)
                {
                    anim.Play("door");
                    doorOpened = true;
                }
            }
        }*/
    }
}
