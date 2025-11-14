using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class HighlightText : MonoBehaviour, ISelectHandler, IDeselectHandler
{
    [Header("Text Style Settings")]
    [Space(5)]

    public TMP_FontAsset originalFont;
    public TMP_FontAsset highlightFont;
    public bool changeFont;
    public bool isBoldOnHighlight;


    [Header("Image Settings")]
    [Space(5)]

    public bool imgFillsOnHover;
    public Image buttonImage;


    [Header("Text Settings")]
    [Space(5)]

    public TextMeshProUGUI buttonTxt;
    public TextMeshProUGUI buttonTxt2;
    private float originalFontSize;
    public Color originalColor;
    public Color highlightColor;

    [SerializeField]
    private Button button;
    public bool textIsFirstChild = true;

    [Header("border Settings")]
    [Space(5)]

    public Image border;
    public bool hasBorder;



    private void Awake()
    {
        button = GetComponent<Button>();

        if (imgFillsOnHover)
        {
            buttonImage = GetComponent<Image>();
        }


        if (textIsFirstChild)
        {
            buttonTxt = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        }

        originalFontSize = buttonTxt.fontSize;
        originalFont = buttonTxt.font;
    }

    void Update()
    {

    }
    public void ChangeColour()
    {
        if (hasBorder)
        {
            border.color = highlightColor;
        }

        if (imgFillsOnHover)
        {
            buttonImage.fillCenter = true;
        }


        buttonTxt.color = highlightColor;

        if (changeFont)
        {
            buttonTxt.font = highlightFont;
        }

        if (isBoldOnHighlight)
        {
            buttonTxt.fontStyle = FontStyles.Bold;
        }

        buttonTxt.fontStyle = FontStyles.UpperCase;
        buttonTxt.fontSize += 1;
    }

    public void ChangeColourBack()
    {

        if (hasBorder)
        {
            border.color = originalColor;
        }

        if (imgFillsOnHover)
        {
            buttonImage.fillCenter = false;
        }

        buttonTxt.color = originalColor;

        if (changeFont)
        {
            buttonTxt.font = originalFont;
        }
        buttonTxt.fontStyle = FontStyles.Normal;
        buttonTxt.fontStyle = FontStyles.UpperCase;
        buttonTxt.fontSize = originalFontSize;
    }


    public void OnSelect(BaseEventData eventData)
    {
        buttonTxt.color = highlightColor;
    }


    public void OnDeselect(BaseEventData eventData)
    {
        buttonTxt.color = originalColor;
    }
}