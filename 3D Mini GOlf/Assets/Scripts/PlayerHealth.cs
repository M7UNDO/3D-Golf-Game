using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public float damageAmount = 0.2f;
    public Image healthBar;

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
}
