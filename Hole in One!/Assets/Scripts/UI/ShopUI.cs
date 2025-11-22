using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopUI : MonoBehaviour
{
    public static ShopUI Instance;
    public SaveManager saveManager;
    public List<BallItem> shopItems;
    public Transform shopScrollView;
    public GameObject shopItemTemplate;  // prefab
    public TextMeshProUGUI coinsText;
    public BallCustomizer ballCustomizer;

    private void Awake() { Instance = this; }

    private void OnEnable()
    {
        PopulateShop();
        UpdateCoinsUI();
    }


    void PopulateShop()
    {
        foreach (Transform child in shopScrollView) Destroy(child.gameObject);

        for (int i = 0; i < shopItems.Count; i++)
        {
            BallItem item = shopItems[i];
            GameObject g = Instantiate(shopItemTemplate, shopScrollView);

            // Assign icon and texts
            // Assign sprite
            g.transform.GetChild(1).transform.GetChild(0).GetComponent<Image>().sprite = item.Icon;

            // Assign display name
            g.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = item.DisplayName;

            // Assign price
            g.transform.GetChild(2).transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = item.Price.ToString();

            // Buy button click
            Button buyBtn = g.transform.GetChild(3).GetComponent<Button>();
            int index = i;
            buyBtn.onClick.AddListener(() => OnBuyButtonClicked(index));

            if (saveManager.IsPurchased(item))
                buyBtn.interactable = false;
        }
    }

    void OnBuyButtonClicked(int index)
    {
        BallItem item = shopItems[index];
        if (saveManager.saveData.Coins >= item.Price)
        {
            Debug.Log(message: $"{saveManager.saveData.Coins}");
            saveManager.saveData.Coins -= item.Price;
            saveManager.PurchaseItem(item);

            ballCustomizer.ApplyBall(item);
            saveManager.SetSelectedBall(index);

            UpdateCoinsUI();
            PopulateShop();


            BallCustomizationUI customizationUI = FindFirstObjectByType<BallCustomizationUI>();
            if (customizationUI != null)
                customizationUI.RefreshUI();
        }
        else
        {
            
            Debug.Log("Not enough coins!");
        }
    }

    void UpdateCoinsUI()
    {
        coinsText.text = saveManager.saveData.Coins.ToString();
    }
}
