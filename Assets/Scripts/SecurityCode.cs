using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SecurityCode : MonoBehaviour
{
    public string[] code;
    public TMP_Text display;
    public int codeOutput;
    public int securityCode;
    public bool isUnlocked;
    void Start()
    {
        
    }

    void Update()
    {
        if(code[3] != "*")
        {
            codeOutput = int.Parse(display.text);
            if(codeOutput == securityCode)
            {
                isUnlocked = true;
            }
            else
            {
                for (int i = 0; i < code.Length; i++)
                {
                    code[i] = "*";
                }
                display.text = code[0] + code[1] + code[2] + code[3];

            }
        }
    }

    public void Keypad(int value)
    {
        Debug.Log(value);
        for (int i = 0; i < code.Length; i++)
        {
            Debug.Log("loop");

            if (code[i].Contains("*"))
            {
                Debug.Log("Contains");
                code[i] = code[i].Replace("*", value.ToString());
                Debug.Log("Replace");
                display.text = code[0] + code[1] + code[2] + code[3];
                return;

            }
        }
    }


}
