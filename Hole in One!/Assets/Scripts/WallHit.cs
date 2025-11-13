using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class WallHit : MonoBehaviour
{
    public AudioSource wallHitSfx;
    public void OnCollisionEnter(Collision coli)
    {
        if (coli.gameObject.CompareTag("Player"))
        {

            wallHitSfx.Play();
        }
    }
}
