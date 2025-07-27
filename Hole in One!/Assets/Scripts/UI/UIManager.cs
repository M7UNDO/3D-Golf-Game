using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private bool toggle;
    private PlayerControls playerInput;
    public Animator autoSaveanim;

    [Header("Shop and Customisation")]
    public GameObject shopCanvas;
    public GameObject menuCanvas;
    public GameObject customisationPanel;
    public GameObject shopPanel;
    public GameObject shopButton;
    public Animator panelAnim;
    public Animator ShopAnim;

    [Header("Controls UI Elements")]
    [Space(5)]

    public GameObject menuUI;

    [Header("Controls UI Elements")]
    [Space(5)]

    public GameObject audioPanel;

    [Header("Level UI Elements")]
    [Space(5)]

    public GameObject levelPanel;
    private void Start()
    {
        autoSaveanim.SetInteger("autoSave", 0);
    }
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
            menuUI.SetActive(false);
            shopButton.SetActive(true);

        }

        if (toggle)
        {
            menuUI.SetActive(true);
            shopButton.SetActive(false);


        }

        
    }

    public void AudioPanel()
    {
        toggle = !toggle;

        if (toggle == false)
        {
            audioPanel.SetActive(false);
            shopButton.SetActive(true);
        }

        if (toggle)
        {
            audioPanel.SetActive(true);
            shopButton.SetActive(false);
        }


    }

    public void LevelPanel()
    {
        toggle = !toggle;

        if (toggle == false)
        {
            levelPanel.SetActive(false);
            shopButton.SetActive(true);
        }

        if (toggle)
        {
            levelPanel.SetActive(true);
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
