using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
public class LoadLevel : MonoBehaviour
{
    public Button[] levelButtons;
    // Start is called before the first frame update
    private void Start()
    {
        SetupLevelButtons();
    }
    public void LoadLevelNumber(int _index)
    {
        Time.timeScale = 1;
       SceneManager.LoadScene(_index);
    }

   public void Play()
   {
        int levelToLoad = GetLastUnlockedLevel();
        Time.timeScale = 1;
        SceneManager.LoadScene(levelToLoad);
   }

   int GetLastUnlockedLevel()
   {
        bool[] unlocked = Save.instance.LevelsUnlocked;

        for (int i = unlocked.Length - 1; i >= 0; i--)
        {
            if (unlocked[i])
                return i + 1; // Offset to match Build Index
        }

        return 1; // Fallback to Level 1 (scene index 1)
   }


    /*
    public void LoadLevel(int levelIndex)
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(levelIndex);
    }
    */
    // Sets up UI based on which levels are unlocked

    public void CompleteLevel(int currentLevelIndex)
    {
        int nextLevel = currentLevelIndex + 1;

        if (nextLevel < Save.instance.LevelsUnlocked.Length)
        {
            Save.instance.LevelsUnlocked[nextLevel] = true;
            Save.instance.SaveData();
        }
    }

    // Loads a level scene by index
    public void LoadLevelByIndex(int levelIndex)
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(levelIndex);
    }
    void SetupLevelButtons()
    {
        for (int i = 0; i < Save.instance.LevelsUnlocked.Length && i < levelButtons.Length; i++)
        {
            int levelIndex = i;

            bool isUnlocked = Save.instance.LevelsUnlocked[levelIndex];

            levelButtons[i].interactable = isUnlocked;

            if (isUnlocked)
            {
                // Add 1 to match the Build Index (Main Menu is at index 0)
                int sceneBuildIndex = levelIndex + 1;
                levelButtons[i].onClick.AddListener(() => LoadLevelByIndex(sceneBuildIndex));
            }
        }
    }

}
