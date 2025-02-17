using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ChangeText : MonoBehaviour
{
    public TextMeshProUGUI buttonTxt;


    public void ChangeColour()
    {
        buttonTxt.color = buttonTxt.color;
        buttonTxt.color = Color.black;
    }

    public void ChangeColourBack()
    {
        buttonTxt.color = Color.white;


    }
}
