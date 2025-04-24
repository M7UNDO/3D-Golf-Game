using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class WallHit : MonoBehaviour
{
    public AudioSource wallHitSfx;
    // Start is called before the first frame update
    public void OnCollisionEnter(Collision coli)
    {
        if (coli.gameObject.CompareTag("Player"))
        {
            //particles.Play();
            wallHitSfx.Play();
            print("wall hit");
        }

        /*if (coli.gameObject.CompareTag("Ground"))
        {
            landSfx.Play();
            print("landed");
        }
        */
    }
}
