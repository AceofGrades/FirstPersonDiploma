using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Flashlight : MonoBehaviour
{
    public Light flashlight;
    public Light cameraLight;

    public Slider batterySlider;
    public float battery;
    public float maxBattery;
    public bool torchOn;
    public bool flashlightEmpty = true;

    private void Awake()
    {
        flashlight.enabled = false;
        flashlight.transform.parent.GetComponent<MeshRenderer>().material.SetColor("_EmissionColor", Color.black * 2f);
        cameraLight.enabled = false;
        battery = maxBattery;
        batterySlider.maxValue = maxBattery;
        batterySlider.value = battery;
    }

    void Update()
    {
        battery = Mathf.Clamp(battery, 0, maxBattery);
        if (flashlightEmpty == true)
        {
            if (Input.GetKeyDown(KeyCode.F) && flashlight.enabled == false)
            {

                flashlight.enabled = true;
                flashlight.transform.parent.GetComponent<MeshRenderer>().material.SetColor("_EmissionColor", Color.white * 2f);
                cameraLight.enabled = true;
            }
            if (Input.GetKeyDown(KeyCode.F) && flashlight.enabled == true && torchOn == true)
            {

                flashlight.enabled = false;
                flashlight.transform.parent.GetComponent<MeshRenderer>().material.SetColor("_EmissionColor", Color.black * 2f);
                cameraLight.enabled = false;
            }
        }
        else
        {
            flashlight.enabled = false;
            flashlight.transform.parent.GetComponent<MeshRenderer>().material.SetColor("_EmissionColor", Color.black);
            cameraLight.enabled = false;
        }


        if (Input.GetKeyDown(KeyCode.F))
        {
            torchOn = !torchOn;
        }
        if (torchOn == true)
        {
            battery -= 0.7f;
            batterySlider.value = battery;
        }
        else if (torchOn == false)
        {
            battery += 0.5f;
            batterySlider.value = battery;
        }
        if (battery <= 0)
        {
            flashlightEmpty = false;
            torchOn = false;
        }
        else
        {
            flashlightEmpty = true;
        }
    }
}
