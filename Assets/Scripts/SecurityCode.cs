using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SecurityCode : MonoBehaviour
{
    public string[] code;
    public TMP_Text display;
    public string codeOutput;
    public string securityCode;
    public bool isUnlocked;

    public KeyCode[] numberPad;
    public string[] numberPadValue = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "0" };
    void Start()
    {

    }

    void Update()
    {
        KeyCodeInsert();
    }

    public void KeyCodeInsert()
    {
        for (int d = 0; d < code.Length; d++)
        {
            if (code[d].Contains("*"))
            {
                for (int n = 0; n < numberPad.Length; n++)
                {
                    if (Input.GetKeyDown(numberPad[n]))
                    {
                        Debug.Log("p");
                        code[d] = code[d].Replace("*", numberPadValue[n]);
                    }
                }
            }
        }
        if (code[code.Length-1] != "*")
        {
            codeOutput = display.text;
            if (codeOutput == securityCode)
            {
                isUnlocked = true;
            }
            else
            {
                for (int d = 0; d < code.Length; d++)
                {
                    code[d] = "*";
                }
                display.text = numberPadValue[0] + numberPadValue[1] + numberPadValue[2] + numberPadValue[3];

            }
        }
    }
}
