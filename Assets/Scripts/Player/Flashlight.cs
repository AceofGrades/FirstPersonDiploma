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
    public float maxBattery = 100f;
    public float batteryCheese;
    public bool torchOn;
    public bool flashlightEmpty = true;

    public bool b3, b2, b1;
    public bool rechargeLock = false;

    private void Awake()
    {
        flashlight.enabled = false;
        this.GetComponent<MeshRenderer>().material.SetColor("_EmissionColor", Color.black * 2f);
        cameraLight.enabled = false;
        battery = maxBattery;
        batterySlider.maxValue = maxBattery;
        batterySlider.value = batteryCheese;
        batteryCheese = maxBattery;
        b1 = true;
        b2 = true;
        b3 = true;
    }

    void Update()
    {
        battery = Mathf.Clamp(battery, -5, maxBattery);
        batteryCheese = Mathf.Clamp(batteryCheese, 0, maxBattery);

        if (battery >= 66f)
        {
            b1 = true;
            b2 = true;
            b3 = true;
            batteryCheese = 100f;
        }
        else if (battery <= 66f && battery >= 33f)
        {
            b3 = false;
            batteryCheese = 66f;
        }
        else if (battery <= 66f && battery <= 33f && battery >= 0f)
        {
            b2 = false;
            b3 = false;
            batteryCheese = 33f;
        }
        else if (battery <= 66f && battery <= 33f && battery <= 0f)
        {
            b1 = false;
            b2 = false;
            b3 = false;
        }

        if (flashlightEmpty == true)
        {
            if (Input.GetKeyDown(KeyCode.F) && flashlight.enabled == false)
            {
                flashlight.enabled = true;
                GetComponent<MeshRenderer>().material.SetColor("_EmissionColor", Color.white * 2f);
                cameraLight.enabled = true;
            }
            if (Input.GetKeyDown(KeyCode.F) && flashlight.enabled == true && torchOn == true)
            {
                flashlight.enabled = false;
                GetComponent<MeshRenderer>().material.SetColor("_EmissionColor", Color.black * 2f);
                cameraLight.enabled = false;
            }
        }
        else
        {
            flashlight.enabled = false;
            GetComponent<MeshRenderer>().material.SetColor("_EmissionColor", Color.black);
            cameraLight.enabled = false;
        }

        if (flashlightEmpty == true)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                torchOn = !torchOn;
            }
        }
        if (torchOn == true)
        {
            battery -= 0.7f;
            batterySlider.value = batteryCheese;
        }
        if (torchOn == false)
        {
            battery += 0.75f;
            batterySlider.value = battery;

        }
        if (battery <= 0)
        {
            flashlightEmpty = false;
            torchOn = false;
        }
        if(battery >= 100f)
        {
            flashlightEmpty = true;
        }
    }
}
