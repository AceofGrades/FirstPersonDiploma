using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IconFollow : MonoBehaviour
{
    public Transform target;

    public void LateUpdate()
    {
        transform.position = target.position;
        transform.position += Vector3.up * 10;
        Vector3 rot = transform.localEulerAngles;
        rot.y = target.localEulerAngles.y;
        transform.localEulerAngles = rot;
    }
}
