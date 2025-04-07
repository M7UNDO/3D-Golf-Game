using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using TMPro;

public class UIManager : MonoBehaviour
{
    private bool toggle;
    private PlayerControls playerInput;
    public PullAndRelease pullAndRelease;

    [Header("Shop and Customisation")]
    public GameObject shopCanvas;
    public GameObject menuCanvas;
    public GameObject customisationPanel;
    public GameObject shopPanel;
    public GameObject shopButton;
    public TextMeshProUGUI buttonTxt;
    public Animator panelAnim;
    public Animator ShopAnim;
    public Color originalColor;

    [Header("Menu UI Elements")]
    [Space(5)]
    public GameObject menuUI;

    public void QuitGame()
    {
        Application.Quit();
    }

    public void PlayGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Level 1");
        
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1f;
    }
    

    public void ControlPanel()
    {
        toggle = !toggle;

        if (toggle == false)
        {
            buttonTxt.color = originalColor;
            menuUI.SetActive(false);
            shopButton.SetActive(true);

        }

        if (toggle)
        {
            menuUI.SetActive(true);
            shopButton.SetActive(false);


        }

        
    }

    public void ShopOpenClose()
    {
        toggle = !toggle;

        if (toggle)
        {
            ShopAnim.ResetTrigger("closeShop");
            ShopAnim.SetTrigger("openShop");
            menuCanvas.SetActive(false);
        }

        if (!toggle)
        {
            ShopAnim.ResetTrigger("openShop");
            ShopAnim.SetTrigger("closeShop");
            menuCanvas.SetActive(true);
        }
    }

    public void CustomisationPanel()
    {
        toggle = !toggle;

        if(toggle)
        {
            panelAnim.ResetTrigger("slide");
            panelAnim.SetTrigger("slideBack");

        }

        if (!toggle)
        {
            panelAnim.ResetTrigger("slideBack");
            panelAnim.SetTrigger("slide");

        }

        print("CustomisationPanel");
    }

    public void LoadMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

}
