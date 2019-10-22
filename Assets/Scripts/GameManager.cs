using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public struct SecurityDoor
    {
        public string name;
        public bool isUnlocked;
        public GameObject door;
        public GameObject[] doorRequirements;
        public Animator doorAnim;
    };

    void Start()
    {
        
    }


    void Update()
    {
        
    }
}
