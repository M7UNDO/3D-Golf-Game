using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoleVortex : MonoBehaviour
{
    public Collider holeCollider;
    public float vortexForce;
    void Start()
    {
        holeCollider = GetComponent<Collider>();
    }

    private void OnTriggerStay(Collider coli)
    {
        if (coli.gameObject.CompareTag("Player"))
        {
            Vector3 normal = coli.transform.position - holeCollider.bounds.center;
            coli.attachedRigidbody.AddForce(normal * vortexForce);
        }
    }

}
