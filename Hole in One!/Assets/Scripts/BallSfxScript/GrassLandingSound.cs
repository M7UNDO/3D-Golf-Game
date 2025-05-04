using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class GrassLandingSound : MonoBehaviour
{
    public float minVelocityToTrigger = 2f; // Ignore weak landings
    private Rigidbody rb;
    private bool wasGrounded = false;
    public AudioSource landSfx;
    private float lastPlayTime = -1f;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision coli)
    {
        if (coli.gameObject.CompareTag("Ground"))
        {
            landSfx.Play();
            lastPlayTime = Time.time;
        }
    }
}