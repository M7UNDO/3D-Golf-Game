using UnityEngine;

public class BallCustomizer : MonoBehaviour
{
    [Header("Assign the MeshRenderer of this ball")]
    public MeshRenderer meshRenderer;

    void Start()
    {
        ApplySelectedBall();
        Debug.Log(meshRenderer.material);
    }

    public void ApplyBall(BallItem item)
    {
        if (meshRenderer != null && item != null && item.Material != null)
        {
            meshRenderer.material = item.Material;
            Debug.Log("Applied material: " + item.Material.name);
            
        }
        else
        {
            Debug.LogWarning("Material not applied! Check meshRenderer or BallItem.Material.");
        }
    }


    void ApplySelectedBall()
    {
        if (SaveManager.instance == null || ShopUI.Instance == null) return;

        int selectedIndex = SaveManager.instance.saveData.currentBallIndex;

        if (selectedIndex < 0 || selectedIndex >= ShopUI.Instance.shopItems.Count) return;

        BallItem selectedItem = ShopUI.Instance.shopItems[selectedIndex];
        ApplyBall(selectedItem);
    }
}
