using UnityEngine;

public class VortexForce : MonoBehaviour
{
    public Transform vortexCenter; // Assign this in the Inspector
    public float vortexForce = 100f;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            Debug.LogError("Rigidbody component not found!");
            enabled = false; // Disable the script if no Rigidbody
        }
        if (vortexCenter == null)
        {
            Debug.LogError("Vortex center not assigned!");
            enabled = false; // Disable the script if no vortex center is assigned
        }
    }

    void FixedUpdate()
    {
        if (vortexCenter == null) return; //Exit if vortex center is not assigned
        Vector3 direction = vortexCenter.position - transform.position;
        float distance = direction.magnitude;
        if (distance > 0.1f)
        { //Prevent division by zero or excessive force at very close range
            direction.Normalize();
            rb.AddForce(direction * vortexForce / (distance * distance));
        }
    }
}