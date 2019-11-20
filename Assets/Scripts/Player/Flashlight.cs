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
    public float batteryCheese;
    public bool torchOn;
    public bool flashlightEmpty = true;

    public bool b3,b2,b1;

    private void Awake()
    {
        flashlight.enabled = false;
        flashlight.transform.parent.GetComponent<MeshRenderer>().material.SetColor("_EmissionColor", Color.black * 2f);
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
        battery = Mathf.Clamp(battery, 0, maxBattery);
        batteryCheese = Mathf.Clamp(batteryCheese, 0, maxBattery);

        if (battery >= 66.66f && b1 == true && b2 == true && b3 == true)
        {
            batteryCheese = 100f;
        }
        if (battery <= 66.66f && b1 == true && b2 == true && b3 == false)
        {
            batteryCheese = 100f;
        }
        if (battery >= 33.33f && b1 == true && b2 == true && b3 == false)
        {
            batteryCheese = 66.66f;
            b3 = false;
        }
        if (battery <= 0)
        {
            batteryCheese = battery;
        }

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
            batterySlider.value = batteryCheese;
        }
        else if (torchOn == false)
        {
            battery += 0.5f;
            batterySlider.value = batteryCheese;
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
