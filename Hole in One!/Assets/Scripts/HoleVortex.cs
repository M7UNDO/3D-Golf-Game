using UnityEngine;

public class HoleVortex : MonoBehaviour
{
    public float vortexForce = 10f;
    public float snapDistance = 0.3f;
    public Transform holeCenter;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Rigidbody rb = other.attachedRigidbody;
            if (rb == null) return;

            Vector3 directionToHole = holeCenter.position - other.transform.position;
            float distance = directionToHole.magnitude;

            // Pull ball toward hole
            rb.AddForce(directionToHole.normalized * vortexForce);

            // If close enough, snap ball into the hole
            if (distance < snapDistance)
            {
                rb.linearVelocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
                other.transform.position = holeCenter.position;

                // Optional: Disable movement or trigger goal logic here
                // Destroy(other.gameObject); // or send event to GameManager
            }
        }
    }
}
    