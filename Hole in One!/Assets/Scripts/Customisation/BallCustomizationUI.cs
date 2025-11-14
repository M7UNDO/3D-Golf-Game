using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

public class BallCustomizationUI : MonoBehaviour
{
    public Transform ballsScrollView;
    public GameObject ballButtonTemplate;  // prefab: Button + Image + Text
    public Color activeColor = Color.green;
    public Color defaultColor = Color.white;
    public BallCustomizer ballCustomizer;

    private List<BallItem> ownedBalls = new List<BallItem>();

    private void OnEnable()
    {
        // Delay execution by one frame so ShopUI.Instance is ready
        StartCoroutine(DelayedRefresh());
    }

    private System.Collections.IEnumerator DelayedRefresh()
    {
        yield return null; // wait one frame
        if (ShopUI.Instance != null)
        {
            RefreshUI();
        }
        else
        {
            Debug.LogError("ShopUI.Instance is still null after delay!");
        }
    }

    public void RefreshUI()
    {
        if (ballsScrollView == null)
        {
            Debug.LogError("ballsScrollView is not assigned!");
            return;
        }

        if (ballButtonTemplate == null)
        {
            Debug.LogError("ballButtonTemplate is not assigned!");
            return;
        }

        // Clear existing buttons
        foreach (Transform child in ballsScrollView)
        {
            Destroy(child.gameObject);
        }

        ownedBalls.Clear();

        if (ShopUI.Instance == null)
        {
            Debug.LogError("ShopUI.Instance is null!");
            return;
        }

        if (SaveManager.instance == null || SaveManager.instance.saveData == null)
        {
            Debug.LogError("SaveManager.instance or saveData is null!");
            return;
        }

        // Collect owned balls
        foreach (BallItem item in ShopUI.Instance.shopItems)
        {
            if (SaveManager.instance.IsPurchased(item))
                ownedBalls.Add(item);
        }

        for (int i = 0; i < ownedBalls.Count; i++)
        {
            BallItem item = ownedBalls[i];
            GameObject g = Instantiate(ballButtonTemplate, ballsScrollView);

            // Assign sprite
            Transform spriteTransform = g.transform.Find("ImageMask/ItemSprite");
            if (spriteTransform == null)
            {
                Debug.LogError($"ItemSprite not found in ballButtonTemplate for {item.DisplayName}");
                continue;
            }
            Image spriteImage = spriteTransform.GetComponent<Image>();
            if (spriteImage != null)
                spriteImage.sprite = item.Icon;
            else
                Debug.LogError("ItemSprite does not have an Image component!");

            // Button click handled by root Button
            Button btn = g.GetComponent<Button>();
            if (btn != null)
            {
                int index = i; // local copy for lambda
                btn.onClick.AddListener(() => OnBallClick(index));
            }
            else
            {
                Debug.LogError("BallButtonTemplate root does not have a Button component!");
            }

            // Set background color for selection
            Image bg = g.GetComponent<Image>();
            if (bg != null)
            {
                bg.color = (SaveManager.instance.saveData.currentBallIndex ==
                            ShopUI.Instance.shopItems.IndexOf(item))
                            ? activeColor : defaultColor;
            }
            else
            {
                Debug.LogWarning("BallButtonTemplate root does not have an Image component for background color.");
            }
        }

        ApplySelectedBall();
    }


    void OnBallClick(int index)
    {
        BallItem item = ownedBalls[index];
        SaveManager.instance.SetSelectedBall(ShopUI.Instance.shopItems.IndexOf(item));
        ApplySelectedBall();
        RefreshUI();
    }

    void ApplySelectedBall()
    {
        int selectedIndex = SaveManager.instance.saveData.currentBallIndex;
        BallItem item = ShopUI.Instance.shopItems[selectedIndex];
        ballCustomizer.ApplyBall(item);
    }
}
