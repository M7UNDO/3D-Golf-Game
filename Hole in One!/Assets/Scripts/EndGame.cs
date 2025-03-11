using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EndGame : MonoBehaviour
{
    public GameObject EndPanel;

    private void OnCollisionEnter(Collision coli)
    {
        if (coli.gameObject.CompareTag("Player"))
        {
            EndPanel.SetActive(true);
           

        }
        
    }
}
