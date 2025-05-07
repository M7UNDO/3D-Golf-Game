using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LevelButton : MonoBehaviour
{
    public Image ButtonImage;
    public Color hoverColor;
    public Color hoverExitColor;
    private Button levelbtn;
    public Image levelImage;
    public Color levelColor;
    void Start()
    {
        levelbtn = GetComponent<Button>();

        if(levelbtn.interactable == false)
        {
            levelImage.color = levelColor;
        }

       
    }

    public void ColourChange()
    {
        ButtonImage.color = hoverColor;
    }

    public void ColourChangeBack()
    {
        ButtonImage.color = hoverExitColor;
    }

}
