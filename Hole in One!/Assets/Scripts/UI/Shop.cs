
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

public class Shop : MonoBehaviour
{
    #region Singlton:Shop

    public static Shop Instance;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    #endregion

    [System.Serializable]
    public class ShopItem
    {
        public Sprite Image;
        public int Price;
        public bool IsPurchased = false;
        public int Order;
    }

    public List<ShopItem> ShopItemsList;
    [SerializeField] Animator NoCoinsAnimation;


    [SerializeField] GameObject ItemTemplate;
    GameObject g;
    [SerializeField] Transform ShopScrollView;
    [SerializeField] GameObject ShopPanel;
    Button buyBtn;


    
    void Start()
    {
        Profile.Instance.GetAvailableAvatars();
        SyncShopItemsWithSave();
        int len = ShopItemsList.Count;

        for (int i = 0; i < len; i++)
        {
            g = Instantiate(ItemTemplate, ShopScrollView);
            g.transform.GetChild(0).GetChild(0).GetComponent<Image>().sprite = ShopItemsList[i].Image;
            g.transform.GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>().text = ShopItemsList[i].Price.ToString();
            buyBtn = g.transform.GetChild(2).GetComponent<Button>();
            if (ShopItemsList[i].IsPurchased)
            {
                DisableBuyButton();
            }
            buyBtn.AddEventListener(i, OnShopItemBtnClicked);
        }
    }

    void SyncShopItemsWithSave()
    {
        for (int i = 0; i < ShopItemsList.Count; i++)
        {
            if (i < Save.instance.ballsUnlocked.Length)
            {
                ShopItemsList[i].IsPurchased = Save.instance.ballsUnlocked[i];
            }
        }
    }

    void OnShopItemBtnClicked(int itemIndex)
    {
        if (Game.Instance.HasEnoughCoins(ShopItemsList[itemIndex].Price))
        {
            Game.Instance.UseCoins(ShopItemsList[itemIndex].Price);
            //purchase Item
            ShopItemsList[itemIndex].IsPurchased = true;
            Save.instance.ballsUnlocked[itemIndex] = true;
            Save.instance.SaveData();

            //disable the button
            buyBtn = ShopScrollView.GetChild(itemIndex).GetChild(2).GetComponent<Button>();
            DisableBuyButton();
            //change UI text: coins
            Game.Instance.UpdateAllCoinsUIText();

            //add avatar
            Profile.Instance.AddAvatar(ShopItemsList[itemIndex].Image);
        }
        else
        {
            NoCoinsAnimation.SetTrigger("NoCoins");
            Debug.Log("You don't have enough coins!!");
        }

        
    }

    void DisableBuyButton()
    {
        buyBtn.interactable = false;
        buyBtn.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "PURCHASED";
    }

    
}
