using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class ShopUI : MonoBehaviour
{
    public static ShopUI Instance;

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
            g.transform.Find("ImageMask/BallSprite").GetComponent<Image>().sprite = item.Icon;

            // Assign display name
            g.transform.Find("Name").GetComponent<TextMeshProUGUI>().text = item.DisplayName;

            // Assign price
            g.transform.Find("CoinIcon/PriceTxt").GetComponent<TextMeshProUGUI>().text = item.Price.ToString();

            // Buy button click
            Button buyBtn = g.transform.Find("BuyButton").GetComponent<Button>();
            int index = i;
            buyBtn.onClick.AddListener(() => OnBuyButtonClicked(index));

            if (SaveManager.instance.IsPurchased(item))
                buyBtn.interactable = false;
        }
    }

    void OnBuyButtonClicked(int index)
    {
        BallItem item = shopItems[index];
        if (SaveManager.instance.saveData.Coins >= item.Price)
        {
            SaveManager.instance.saveData.Coins -= item.Price;
            SaveManager.instance.PurchaseItem(item);

            // Apply to ball immediately
            ballCustomizer.ApplyBall(item);
            SaveManager.instance.SetSelectedBall(index);

            UpdateCoinsUI();
            PopulateShop(); // refresh buttons

            // Refresh customization UI
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
        coinsText.text = SaveManager.instance.saveData.Coins.ToString();
    }
}
