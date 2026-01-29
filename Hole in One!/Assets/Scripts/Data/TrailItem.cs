using UnityEngine;
using System;

[CreateAssetMenu(menuName = "Shop/TrailItem")]
public class TrailItem : ScriptableObject
{
    public string ItemID;
    public string DisplayName;
    public Sprite Icon;
    public TrailRenderer trailRenderer;
    public float TrailTime = 0.5f;
    public float StartWidth = 0.2f;
    public float EndWidth = 0.05f;
    public int Price;

    private void OnValidate()
    {
        if (string.IsNullOrEmpty(ItemID))
            ItemID = DisplayName.Replace(" ", "_") + "_" + Guid.NewGuid().ToString("N");
    }
}
