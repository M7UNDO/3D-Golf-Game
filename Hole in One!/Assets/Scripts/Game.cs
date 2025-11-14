using UnityEngine;
using TMPro;

public class Game : MonoBehaviour
{
    #region Singleton:Game

    public static Game Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            // Uncomment if you want this object to persist across scenes
            // DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    #endregion

    [SerializeField] private TextMeshProUGUI[] allCoinsUIText;
    [SerializeField] private SaveManager saveManager;

    private void Start()
    {
        UpdateAllCoinsUIText();
    }

    private void Update()
    {
        // Debug keys for testing coins
        if (Input.GetKeyDown(KeyCode.Z))
        {
            AddCoins(100);
            Debug.Log("+100 Coins");
        }
        else if (Input.GetKeyDown(KeyCode.X))
        {
            RemoveCoins(100);
            Debug.Log("-100 Coins");
        }
        else if (Input.GetKeyDown(KeyCode.R))
        {
            ResetGame();
        }
    }

    public void ResetGame()
    {
        saveManager.ResetGame();
    }

    #region Coins Management

    public void AddCoins(int amount)
    {
        SaveManager.instance.saveData.Coins += amount;
        SaveManager.instance.SaveGame();
        UpdateAllCoinsUIText();
    }

    public void RemoveCoins(int amount)
    {
        SaveManager.instance.saveData.Coins = Mathf.Max(0, SaveManager.instance.saveData.Coins - amount);
        SaveManager.instance.SaveGame();
        UpdateAllCoinsUIText();
    }

    public bool HasEnoughCoins(int amount)
    {
        return SaveManager.instance.saveData.Coins >= amount;
    }

    public void UseCoins(int amount)
    {
        if (HasEnoughCoins(amount))
        {
            RemoveCoins(amount);
        }
        else
        {
            Debug.LogWarning("Not enough coins!");
        }
    }

    #endregion

    public void UpdateAllCoinsUIText()
    {
        int coins = SaveManager.instance.saveData.Coins;

        foreach (var txt in allCoinsUIText)
        {
            if (txt != null)
                txt.text = coins.ToString();
        }
    }
}
