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
        }
        else if(coli.gameObject.CompareTag("Ground_Grass"))
        {

        }
        else if (coli.gameObject.CompareTag("Ground_Wood"))
        {

        }
        else if (coli.gameObject.CompareTag("Ground_Stone"))
        {

        }

    }
}
