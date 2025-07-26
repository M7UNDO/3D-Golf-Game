using UnityEngine;
using System.Collections.Generic;


public class BallType : MonoBehaviour
{
    public List<GameObject> AllBallPrefabs; // Matches the full ShopItemsList
    List<GameObject> PurchasedBallTypes = new List<GameObject>();

    void Awake()
    {
        BuildPurchasedList();
        ChooseBallType(Save.instance.currentBall);
    }

    void BuildPurchasedList()
    {
        PurchasedBallTypes.Clear();
        for (int i = 0; i < Shop.Instance.ShopItemsList.Count; i++)
        {
            if (Shop.Instance.ShopItemsList[i].IsPurchased)
            {
                PurchasedBallTypes.Add(AllBallPrefabs[i]);
            }
        }
    }

    public void ChooseBallType(int _index)
    {
        if (_index >= 0 && _index < PurchasedBallTypes.Count)
        {
            Instantiate(PurchasedBallTypes[_index], transform.position, Quaternion.identity, transform);
        }
        else
        {
            Debug.LogWarning("Invalid ball index: " + _index);
        }
    }
}