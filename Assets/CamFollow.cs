using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollow : MonoBehaviour
{
    public Transform target;

    public void LateUpdate()
    {
        Vector3 rot = transform.localEulerAngles;
        rot.y = target.localEulerAngles.y;
        transform.localEulerAngles = rot;
    }
}
