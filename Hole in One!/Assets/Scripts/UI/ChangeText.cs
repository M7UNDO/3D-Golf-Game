using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ChangeText : MonoBehaviour
{
    public TextMeshProUGUI buttonTxt;
    public Color originalColor;
    public Color highlightColor;
    public bool isBoldOnHover;

    public void ChangeColour()
    {

        //buttonTxt.color = buttonTxt.color;
        buttonTxt.color = highlightColor;
        if (isBoldOnHover )
        buttonTxt.fontStyle = FontStyles.Bold;
        //buttonTxt.fontStyle = FontStyles.UpperCase;
        
    }

    public void ChangeColourBack()
    {
        buttonTxt.color = originalColor;
        if (isBoldOnHover )
        buttonTxt.fontStyle = FontStyles.Normal;
        //buttonTxt.fontStyle = FontStyles.UpperCase;



    }
}
