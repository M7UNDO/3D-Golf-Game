using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveShop : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }


}
