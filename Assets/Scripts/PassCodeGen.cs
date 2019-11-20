using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PassCodeGen : MonoBehaviour
{
    public GameObject securityPanel;

    public string[] numbers = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "0" };
    public string passCode;
    public int randomNumber;
    public string randomCode;
    public string randomNumbers;

    public PassCodeGen keypad;
    public bool codeReceived = false;
    public Text text;

    private void Update()
    {
        if (keypad.enabled == true && codeReceived == false)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                RandomNumber();
                codeReceived = true;
                text.text = passCode;
            }
        }
    }

    public void RandomNumber()
    {
        for (int i = 0; i < 4; i++)
        {
            randomNumber = Random.Range(0, numbers.Length);
            randomNumbers = numbers[randomNumber];

            passCode += randomNumbers;
        }
        Debug.Log(passCode);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            securityPanel.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            securityPanel.SetActive(false);
        }
    }
}
