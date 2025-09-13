using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ChangeText : MonoBehaviour, ISelectHandler, IDeselectHandler
{

    public TextMeshProUGUI buttonTxt;
    public Color originalColor;
    public Color highlightColor;
    public bool isBoldOnHover;
    [SerializeField]
    private Button button;

    private void Awake()
    {
        button = GetComponent<Button>();
    }

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

    // Called when the button is selected (e.g., via navigation)
    public void OnSelect(BaseEventData eventData)
    {
        buttonTxt.color = highlightColor;
        if (isBoldOnHover)
            buttonTxt.fontStyle = FontStyles.Bold;
    }

    // Called when the button is deselected (e.g., navigating away)
    public void OnDeselect(BaseEventData eventData)
    {
        buttonTxt.color = originalColor;
        if (isBoldOnHover)
            buttonTxt.fontStyle = FontStyles.Normal;
    }
}
