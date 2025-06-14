using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class GrassLandingSound : MonoBehaviour
{
    public float minVelocityToTrigger = 2f; // Ignore weak landings
    private Rigidbody rb;
    private bool wasGrounded = false;
    public AudioSource landSfx;
    private float lastPlayTime = -1f;
    private Vector3 lastVelocity;
    public float landingVelocityThreshold = 2f;


    void FixedUpdate()
    {
        lastVelocity = GetComponent<Rigidbody>().linearVelocity;
    }


    private void OnCollisionEnter(Collision coli)
    {
       /* if (coli.gameObject.CompareTag("Ground"))
        {
            landSfx.Play();
            
        }
        */
        if (coli.relativeVelocity.magnitude > landingVelocityThreshold &&
            coli.gameObject.CompareTag("Ground"))
        {
            landSfx.Play();
            lastPlayTime = Time.time;
        }
    }
}