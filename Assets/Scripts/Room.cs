using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public GameObject room;
    public Transform roomVent;

    public void Awake()
    {
        room = this.gameObject;
    }
}
