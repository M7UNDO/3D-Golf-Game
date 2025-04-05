using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EndGame : MonoBehaviour
{
    public GameObject EndPanel;
    public GameObject[] hudElements;
    public PullAndRelease pullAndRelease;
    public TextMeshProUGUI scoreTxt;
    public float endScore;

    public void EndScore()
    {
        scoreTxt.text = pullAndRelease.NumberOfShots.ToString();
    }
    private void OnCollisionEnter(Collision coli)
    {
        if (coli.gameObject.CompareTag("Player"))
        {
            Time.timeScale = 0f;
            EndPanel.SetActive(true);
            
            foreach (GameObject hud in hudElements)
            {
               hud.SetActive(false);
            }
            
        }

        EndScore();

    }
}
