using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    private bool toggle;
    public GameObject menuUI;
    public GameObject pausePanel;
    public GameObject closeBtn;
    public void QuitGame()
    {
        Application.Quit();
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("SampleScene");
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
