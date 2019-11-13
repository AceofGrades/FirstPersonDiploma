using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponCycle : MonoBehaviour
{
    public int curWeapon;
    public GameObject activeWeapon;
    public Transform[] equipment;
    public Animator arm;

    private bool isSwitching;

    // Start is called before the first frame update
    void Start()
    {
        isSwitching = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            arm.SetTrigger("WeaponSwap");
            curWeapon = curWeapon + 1;
            isSwitching = true;
        }
        else if (Input.GetKeyDown(KeyCode.R))
        {
            arm.SetTrigger("WeaponSwap");
            curWeapon = curWeapon - 1;
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
    }

    public void WeaponSwap()
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
}
