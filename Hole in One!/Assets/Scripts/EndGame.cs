using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class EndGame : MonoBehaviour
{
    public GameObject EndPanel;
    public GameObject[] hudElements;
    public PullAndRelease pullAndRelease;
    public TextMeshProUGUI scoreTxt;
    public TextMeshProUGUI coinsEarnedTxt;
    public float endScore;
    public int [] coinsEarned;
    public int [] shotsTaken;
    public ParticleSystem particles;
    public AudioSource levelWonSfx;
    public AudioSource popUPSfx;

    public void EndScore()
    {
        scoreTxt.text = pullAndRelease.NumberOfShots.ToString();

        if(pullAndRelease.NumberOfShots <= shotsTaken[0])
        {
            Save.instance.Coins += coinsEarned[0];
            coinsEarnedTxt.text = coinsEarned[0].ToString();
        }
        else if(pullAndRelease.NumberOfShots <= shotsTaken[1])
        {
            Save.instance.Coins += coinsEarned[1];
            coinsEarnedTxt.text = coinsEarned[1].ToString();
        }
        else if(pullAndRelease.NumberOfShots <= shotsTaken[2])
        {
            Save.instance.Coins += coinsEarned[2];
            coinsEarnedTxt.text = coinsEarned[2].ToString();
        }

        

        
    }
    private void OnCollisionEnter(Collision coli)
    {
        if (coli.gameObject.CompareTag("Player"))
        {
            particles.Play();
            StartCoroutine(EndGame());
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            int currentLevelIndex = currentSceneIndex - 1; // Level 1 = index 0

            FindObjectOfType<LoadLevel>().CompleteLevel(currentLevelIndex);

            //Save coins + unlocked progress
            Save.instance.SaveData();

        }

        

        IEnumerator EndGame()
        {
            yield return new WaitForSeconds(2f);
            levelWonSfx.Play();
            EndPanel.SetActive(true);
            popUPSfx.Play();


            foreach (GameObject hud in hudElements)
            {
                hud.SetActive(false);
            }
            Time.timeScale = 0f;
            EndScore();
        }

    }
}
