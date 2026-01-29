using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

public class BallCustomizationUI : MonoBehaviour
{
    public Transform scrollView;
    public GameObject itemButtonTemplate;
    public Color activeColor = Color.green;
    public Color defaultColor = Color.white;
    public BallCustomizer ballCustomizer;

    public enum CustomizationMode { Balls, Trails }
    public CustomizationMode currentMode;

    private List<BallItem> ownedBalls = new List<BallItem>();
    private List<TrailItem> ownedTrails = new List<TrailItem>();

    private void OnEnable()
    {
        StartCoroutine(DelayedRefresh());
    }

    private System.Collections.IEnumerator DelayedRefresh()
    {
        yield return null;
        RefreshUI();
    }

    public void RefreshUI()
    {
        foreach (Transform child in scrollView)
            Destroy(child.gameObject);

        if (currentMode == CustomizationMode.Balls)
            RefreshBalls();
        else
            RefreshTrails();
    }

    // ================= BALLS =================

    void RefreshBalls()
    {
        ownedBalls.Clear();

        foreach (BallItem item in ShopUI.Instance.shopItems)
            if (SaveManager.instance.IsPurchased(item))
                ownedBalls.Add(item);

        for (int i = 0; i < ownedBalls.Count; i++)
            CreateBallButton(ownedBalls[i], i);

        ApplySelectedBall();
    }

    void CreateBallButton(BallItem item, int index)
    {
        GameObject g = Instantiate(itemButtonTemplate, scrollView);

        AssignSprite(g, item.Icon);

        Button btn = g.GetComponent<Button>();
        btn.onClick.AddListener(() => OnBallClick(index));

        Image bg = g.GetComponent<Image>();
        bg.color = (SaveManager.instance.saveData.currentBallIndex ==
                   ShopUI.Instance.shopItems.IndexOf(item))
                   ? activeColor : defaultColor;
    }

    void OnBallClick(int index)
    {
        BallItem item = ownedBalls[index];
        SaveManager.instance.SetSelectedBall(
            ShopUI.Instance.shopItems.IndexOf(item)
        );

        ApplySelectedBall();
        RefreshUI();
    }

    void ApplySelectedBall()
    {
        int index = SaveManager.instance.saveData.currentBallIndex;
        ballCustomizer.ApplyBall(ShopUI.Instance.shopItems[index]);
    }

    // ================= TRAILS =================

    void RefreshTrails()
    {
        ownedTrails.Clear();

        foreach (TrailItem item in ShopUI.Instance.trailItems)
            if (SaveManager.instance.IsTrailPurchased(item))
                ownedTrails.Add(item);

        for (int i = 0; i < ownedTrails.Count; i++)
            CreateTrailButton(ownedTrails[i], i);

        ApplySelectedTrail();
    }

    void CreateTrailButton(TrailItem item, int index)
    {
        GameObject g = Instantiate(itemButtonTemplate, scrollView);

        AssignSprite(g, item.Icon);

        Button btn = g.GetComponent<Button>();
        btn.onClick.AddListener(() => OnTrailClick(index));

        Image bg = g.GetComponent<Image>();
        bg.color = (SaveManager.instance.saveData.currentTrailIndex ==
                   ShopUI.Instance.trailItems.IndexOf(item))
                   ? activeColor : defaultColor;
    }

    void OnTrailClick(int index)
    {
        TrailItem item = ownedTrails[index];
        SaveManager.instance.SetSelectedTrail(
            ShopUI.Instance.trailItems.IndexOf(item)
        );

        ApplySelectedTrail();
        RefreshUI();
    }

    void ApplySelectedTrail()
    {
        int index = SaveManager.instance.saveData.currentTrailIndex;

        if (index >= 0)
            ballCustomizer.ApplyTrail(ShopUI.Instance.trailItems[index]);
        else
            ballCustomizer.ApplyTrail(null);
    }

    // ================= SHARED =================

    void AssignSprite(GameObject g, Sprite sprite)
    {
        Transform spriteTransform = g.transform.Find("ImageMask/ItemSprite");
        spriteTransform.GetComponent<Image>().sprite = sprite;
    }
}
