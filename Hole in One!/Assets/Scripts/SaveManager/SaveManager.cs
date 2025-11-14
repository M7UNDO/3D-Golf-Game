using UnityEngine;
using System.Collections.Generic;

public class SaveManager : MonoBehaviour
{
    public static SaveManager instance { get; private set; }
    public SaveData saveData;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);

        LoadGame();
    }

    public void SaveGame() => SaveSystem.Save(saveData);
    public void LoadGame() => saveData = SaveSystem.Load();

    public void ResetGame()
    {
        saveData = new SaveData();
        SaveGame();
    }

    public bool IsPurchased(BallItem item) => saveData.purchasedBallIDs.Contains(item.ItemID);
    public void PurchaseItem(BallItem item)
    {
        if (!IsPurchased(item))
        {
            saveData.purchasedBallIDs.Add(item.ItemID);
            SaveGame();
        }
    }

    public void SetSelectedBall(int index)
    {
        saveData.currentBallIndex = index;
        SaveGame();
    }
}
