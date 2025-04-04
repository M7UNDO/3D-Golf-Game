using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ChangeText : MonoBehaviour
{
    public TextMeshProUGUI buttonTxt;
    public Color originalColor;
    public Color highlightColor;

    public void ChangeColour()
    {
        //buttonTxt.color = buttonTxt.color;
        buttonTxt.color = highlightColor;
        
    }

    public void ChangeColourBack()
    {
        buttonTxt.color = originalColor;
        


    }
}
