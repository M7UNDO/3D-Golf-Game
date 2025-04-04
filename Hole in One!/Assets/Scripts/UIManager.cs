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
    public GameObject shopCanvas;
    public GameObject menuCanvas;
    public GameObject shopButton;
    public TextMeshProUGUI buttonTxt;
    public Color originalColor;

    //public GameObject 
    [Header("Pause UI Elements")]
    [Space(5)]
    
    public GameObject pausePanel;
    //public GameObject closeBtn;

    [Header("Menu UI Elements")]
    [Space(5)]
    public GameObject menuUI;

    
    public void QuitGame()
    {
        Application.Quit();
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Level 1");//Change back after testing
    }

    public void OnEnable()
    {
        var playerInput = new PlayerControls();
        playerInput.Player.Enable();

        playerInput.Player.Pause.performed += ctx => Pause();
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
            shopCanvas.SetActive(true);
            menuCanvas.SetActive(false);
        }

        if (!toggle)
        {
            shopCanvas.SetActive(false);
            menuCanvas.SetActive(true);
        }
    }

    public void Pause()
    {
        print("called");
        toggle = !toggle;

        if (toggle)
        {
            pausePanel.SetActive(true);
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
        }

        if (!toggle)
        {
            pausePanel.SetActive(false);
            Time.timeScale = 1f;
        }
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
