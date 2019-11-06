using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : Room
{
    public BoxCollider[] roomDetection;
    public int currentRoom;

    private void Start()
    {

    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            roomDetection.SetValue(currentRoom, roomValue);
            Debug.Log(roomValue);
        }
    }

}
