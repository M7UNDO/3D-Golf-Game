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
    public TextMeshProUGUI coinsEarnedTxt;
    public float endScore;
    public int [] coinsEarned;

    public void EndScore()
    {
        scoreTxt.text = pullAndRelease.NumberOfShots.ToString();

        if(pullAndRelease.NumberOfShots <= 4)
        {
            Save.instance.Coins += coinsEarned[0];
            coinsEarnedTxt.text = coinsEarned[0].ToString();
        }
        else if(pullAndRelease.NumberOfShots <= 6)
        {
            Save.instance.Coins += coinsEarned[1];
            coinsEarnedTxt.text = coinsEarned[1].ToString();
        }
        else if(pullAndRelease.NumberOfShots <= 10)
        {
            Save.instance.Coins += coinsEarned[2];
            coinsEarnedTxt.text = coinsEarned[2].ToString();
        }

        

        
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
