using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class LoadLevel : MonoBehaviour
{
    [Header("UI Buttons")]
    public Button[] singlePlayerLevelButtons;
    public Button[] multiplayerLevelButtons;

    private void Start()
    {
        SetupSinglePlayerLevelButtons();
        SetupMultiplayerLevelButtons();
    }

    // ------------------- SINGLE PLAYER -------------------

    public void PlaySinglePlayer()
    {
        int levelToLoad = GetLastUnlockedSinglePlayerLevel();
        Time.timeScale = 1;
        SceneManager.LoadScene(levelToLoad);
    }

    int GetLastUnlockedSinglePlayerLevel()
    {
        bool[] unlocked = Save.instance.SinglePlayerLevelsUnlocked;

        for (int i = unlocked.Length - 1; i >= 0; i--)
        {
            if (unlocked[i])
                return i + 1; // Assuming SP levels start at build index 1
        }

        return 1; // Fallback to Level 1
    }

    void SetupSinglePlayerLevelButtons()
    {
        for (int i = 0; i < Save.instance.SinglePlayerLevelsUnlocked.Length && i < singlePlayerLevelButtons.Length; i++)
        {
            int levelIndex = i;
            bool isUnlocked = Save.instance.SinglePlayerLevelsUnlocked[levelIndex];
            singlePlayerLevelButtons[i].interactable = isUnlocked;

            if (isUnlocked)
            {
                int sceneBuildIndex = levelIndex + 1; // Adjust for SP build indices
                singlePlayerLevelButtons[i].onClick.AddListener(() => LoadLevelByIndex(sceneBuildIndex));
            }
        }
    }

    public void CompleteSinglePlayerLevel(int currentLevelIndex)
    {
        int nextLevel = currentLevelIndex + 1;

        if (nextLevel < Save.instance.SinglePlayerLevelsUnlocked.Length)
        {
            Save.instance.SinglePlayerLevelsUnlocked[nextLevel] = true;
            Save.instance.SaveData();
        }
    }

    // ------------------- MULTIPLAYER -------------------

    public void PlayMultiplayer()
    {
        int levelToLoad = GetLastUnlockedMultiplayerLevel();
        Time.timeScale = 1;
        SceneManager.LoadScene(levelToLoad);
    }

    int GetLastUnlockedMultiplayerLevel()
    {
        bool[] unlocked = Save.instance.MultiplayerLevelsUnlocked;

        for (int i = unlocked.Length - 1; i >= 0; i--)
        {
            if (unlocked[i])
                return 20 + i; // Assuming MP levels start at build index 20
        }

        return 20; // Fallback to first MP level
    }

    void SetupMultiplayerLevelButtons()
    {
        for (int i = 0; i < Save.instance.MultiplayerLevelsUnlocked.Length && i < multiplayerLevelButtons.Length; i++)
        {
            int levelIndex = i;
            bool isUnlocked = Save.instance.MultiplayerLevelsUnlocked[levelIndex];
            multiplayerLevelButtons[i].interactable = isUnlocked;

            if (isUnlocked)
            {
                int sceneBuildIndex = 20 + levelIndex; // Adjust for MP build indices
                multiplayerLevelButtons[i].onClick.AddListener(() => LoadLevelByIndex(sceneBuildIndex));
            }
        }
    }

    public void CompleteMultiplayerLevel(int currentLevelIndex)
    {
        int nextLevel = currentLevelIndex + 1;

        if (nextLevel < Save.instance.MultiplayerLevelsUnlocked.Length)
        {
            Save.instance.MultiplayerLevelsUnlocked[nextLevel] = true;
            Save.instance.SaveData();
        }
    }

    // ------------------- COMMON -------------------

    public void LoadLevelByIndex(int levelIndex)
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(levelIndex);
    }
}
