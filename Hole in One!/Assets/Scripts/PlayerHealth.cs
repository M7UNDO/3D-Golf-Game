using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public float damageAmount = 0.2f;
    public Image healthBar;

    public void Start()
    {
        RestartGame();
    }
    public void OnTriggerEnter(Collider coli)
    {
        if (coli.gameObject.CompareTag("Player"))
        {
            healthBar.fillAmount -= damageAmount;
            print(coli.gameObject);
        }
    }
    public void OnCollisionEnter(Collision coli)
    {
        if (coli.gameObject.CompareTag("Player"))
        {
            healthBar.fillAmount -= damageAmount;
            print(coli.gameObject);
        }
    }
    

    public void RestartGame()
    {
        if(healthBar.fillAmount <= 0)
        {
            SceneManager.LoadScene("Test");
        }
    }

    private void Update()
    {
        RestartGame();
    }


}
