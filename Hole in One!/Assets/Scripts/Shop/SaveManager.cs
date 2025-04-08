using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class SaveManager : MonoBehaviour
{
    public static SaveManager Instance;


    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SavePurchasedItems(List<int> purchasedIndices)
    {
        string data = string.Join(",", purchasedIndices);
        PlayerPrefs.SetString("PurchasedItems", data);
        PlayerPrefs.Save();
    }

    public List<int> LoadPurchasedItems()
    {
        string data = PlayerPrefs.GetString("PurchasedItems", "");
        if (string.IsNullOrEmpty(data)) return new List<int>();

        return data.Split(',').Select(int.Parse).ToList();
    }


}
