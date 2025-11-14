using UnityEngine;
using System;

[CreateAssetMenu(menuName = "Shop/BallItem")]
public class BallItem : ScriptableObject
{
    [HideInInspector] public string ItemID;       // auto-generated unique ID

    public string DisplayName;   // name shown in UI
    public Sprite Icon;          // image for UI
    public Material Material;    // material applied to ball
    public int Price;            // coin cost

    public void GenerateID()
    {
        if (string.IsNullOrEmpty(ItemID))
        {
            ItemID = DisplayName.Replace(" ", "_") + "_" + Guid.NewGuid().ToString("N");
        }
    }

    private void OnValidate()
    {
        GenerateID();
    }
}
