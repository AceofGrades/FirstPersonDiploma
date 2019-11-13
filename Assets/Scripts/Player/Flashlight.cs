using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashlight : MonoBehaviour
{
    public Light flashlight;
    public Light cameraLight;

    private void Awake()
    {
        flashlight.enabled = false;
        flashlight.transform.parent.GetComponent<MeshRenderer>().material.SetColor("_EmissionColor", Color.black * 2f);
        cameraLight.enabled = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && flashlight.enabled == false)
        {
            flashlight.enabled = true;
            flashlight.transform.parent.GetComponent<MeshRenderer>().material.SetColor("_EmissionColor", Color.white * 2f);
            cameraLight.enabled = true;
        }
        else if (Input.GetKeyDown(KeyCode.F) && flashlight.enabled == true)
        {
            flashlight.enabled = false;
            flashlight.transform.parent.GetComponent<MeshRenderer>().material.SetColor("_EmissionColor", Color.black);
            cameraLight.enabled = false;
        }
    }
}
