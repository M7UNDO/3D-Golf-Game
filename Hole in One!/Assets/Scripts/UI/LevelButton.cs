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
    public RectTransform rectTransform;
    void Start()
    {
        levelbtn = GetComponent<Button>();
        rectTransform = GetComponent<RectTransform>();

        if (levelbtn.interactable == false)
        {
            levelImage.color = levelColor;
        }


    }

    public void ColourChange()
    {
        ButtonImage.color = hoverColor;
        rectTransform.offsetMin = new Vector2(10, 10);
        rectTransform.offsetMax = new Vector2(-10, -10);

    }

    public void ColourChangeBack()
    {
        ButtonImage.color = hoverExitColor;
        rectTransform.offsetMin = new Vector2(0, 0);
        rectTransform.offsetMax = new Vector2(0, 0);
    }

}
