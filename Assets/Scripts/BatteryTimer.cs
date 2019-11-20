using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BatteryTimer : MonoBehaviour
{
    public Slider batterySlider;
    public float battery;
    public float maxBattery;
    public bool torchOn;

    private void Start()
    {
        battery = maxBattery;
        batterySlider.maxValue = maxBattery;
        batterySlider.value = battery;
    }

    private void Update()
    {
        battery = Mathf.Clamp(battery,0,maxBattery);
        if (Input.GetKeyDown(KeyCode.F))
        {
            torchOn = !torchOn;
        }
        if(torchOn == true)
        {
            battery -= 0.7f;
            batterySlider.value = battery;
        }
        else if (torchOn == false)
        {
            battery += 0.5f;
            batterySlider.value = battery;
        }

    }
}
