using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    private bool toggle;
    [Header("Pause UI Elements")]
    [Space(5)]
    
    public GameObject pausePanel;
    public GameObject closeBtn;

    [Header("Menu UI Elements")]
    [Space(5)]
    public GameObject menuUI;

 
    public void QuitGame()
    {
        Application.Quit();
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("SampleScene");//Change back after testing
    }

    public void ControlPanel()
    {
        toggle = !toggle;

        if (toggle == false)
        {
            menuUI.SetActive(false);

        }

        if (toggle)
        {
            menuUI.SetActive(true);
            
        }

        
    }

    public void Pause()
    {
        print("called");
        toggle = !toggle;

        if (toggle)
        {
            pausePanel.SetActive(true);
            closeBtn.SetActive(false);
            Time.timeScale = 0f;
        }

        if (!toggle)
        {
            pausePanel.SetActive(false);
            closeBtn.SetActive(true);
            Time.timeScale = 0f;
        }
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
