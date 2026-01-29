using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopUI : MonoBehaviour
{
    public static ShopUI Instance;

    public SaveManager saveManager;

    [Header("Ball Shop")]
    public List<BallItem> shopItems;

    [Header("Trail Shop")]
    public List<TrailItem> trailItems;

    [Header("UI")]
    public Transform shopScrollView;
    public GameObject shopItemTemplate;
    public TextMeshProUGUI coinsText;

    [Header("Customizer")]
    public BallCustomizer ballCustomizer;

    public enum ShopMode { Balls, Trails }
    public ShopMode currentMode;

    private void Awake()
    {
        Instance = this;
    }

    private void OnEnable()
    {
        PopulateShop();
        UpdateCoinsUI();
    }

    void PopulateShop()
    {
        foreach (Transform child in shopScrollView)
            Destroy(child.gameObject);

        if (currentMode == ShopMode.Balls)
            PopulateBalls();
        else
            PopulateTrails();
    }

    void PopulateBalls()
    {
        for (int i = 0; i < shopItems.Count; i++)
        {
            BallItem item = shopItems[i];
            GameObject g = Instantiate(shopItemTemplate, shopScrollView);

            SetupCommonUI(g, item.Icon, item.DisplayName, item.Price);

            Button buyBtn = g.transform.GetChild(3).GetComponent<Button>();
            int index = i;
            buyBtn.onClick.AddListener(() => OnBuyBall(index));

            if (saveManager.IsPurchased(item))
                buyBtn.interactable = false;
        }
    }

    void PopulateTrails()
    {
        for (int i = 0; i < trailItems.Count; i++)
        {
            TrailItem item = trailItems[i];
            GameObject g = Instantiate(shopItemTemplate, shopScrollView);

            SetupCommonUI(g, item.Icon, item.DisplayName, item.Price);

            Button buyBtn = g.transform.GetChild(3).GetComponent<Button>();
            int index = i;
            buyBtn.onClick.AddListener(() => OnBuyTrail(index));

            if (saveManager.IsTrailPurchased(item))
                buyBtn.interactable = false;
        }
    }

    void SetupCommonUI(GameObject g, Sprite icon, string name, int price)
    {
        g.transform.GetChild(1).GetChild(0).GetComponent<Image>().sprite = icon;
        g.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = name;
        g.transform.GetChild(2).GetChild(0).GetComponent<TextMeshProUGUI>().text = price.ToString();
    }

    void OnBuyBall(int index)
    {
        BallItem item = shopItems[index];

        if (saveManager.saveData.Coins < item.Price)
        {
            Debug.Log("Not enough coins!");
            return;
        }

        saveManager.saveData.Coins -= item.Price;
        saveManager.PurchaseItem(item);
        saveManager.SetSelectedBall(index);

        ballCustomizer.ApplyBall(item);

        PostPurchaseRefresh();
    }

    void OnBuyTrail(int index)
    {
        TrailItem item = trailItems[index];

        if (saveManager.saveData.Coins < item.Price)
        {
            Debug.Log("Not enough coins!");
            return;
        }

        saveManager.saveData.Coins -= item.Price;
        saveManager.PurchaseTrail(item);
        saveManager.SetSelectedTrail(index);

        ballCustomizer.ApplyTrail(item);

        PostPurchaseRefresh();
    }

    void PostPurchaseRefresh()
    {
        saveManager.SaveGame();
        UpdateCoinsUI();
        PopulateShop();

        BallCustomizationUI customizationUI = FindFirstObjectByType<BallCustomizationUI>();
        if (customizationUI != null)
            customizationUI.RefreshUI();
    }

    void UpdateCoinsUI()
    {
        coinsText.text = saveManager.saveData.Coins.ToString();
    }
}
