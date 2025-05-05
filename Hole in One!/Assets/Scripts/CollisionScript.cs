using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionScript : MonoBehaviour
{
    public ParticleSystem particles;


    public void OnCollisionEnter(Collision coli)
    {
        if (coli.gameObject.CompareTag("Wall"))
        {
            particles.Play();
            //wallHitSfx.Play();
            print("wall hit");
        }

    }
}
