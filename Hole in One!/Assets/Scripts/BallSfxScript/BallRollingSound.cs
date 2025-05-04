using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(AudioSource))]
public class BallRollingSound : MonoBehaviour
{
    public LayerMask groundLayer;
    public float checkDistance = 0.3f;
    public float minSpeedToPlay = 0.1f;

    private Rigidbody rb;
    public AudioSource rollingAudio;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rollingAudio.loop = true;
        rollingAudio.playOnAwake = false;
    }

    private void Update()
    {
        bool isGrounded = Physics.Raycast(transform.position, Vector3.down, checkDistance, groundLayer);
        float speed = rb.velocity.magnitude;

        if (isGrounded && speed > minSpeedToPlay)
        {
            if (!rollingAudio.isPlaying)
                rollingAudio.Play();
        }
        else
        {
            if (rollingAudio.isPlaying)
                rollingAudio.Stop();
        }
    }
}
