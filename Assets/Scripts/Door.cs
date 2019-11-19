using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public GameObject keyPadParent;
    public Animator anim;
    public SecurityCode keyPad;

    void Update()
    {
        if (keyPad.isUnlocked == true)
        {
            anim.Play("doorAnim");
        }
    }
}
