using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SandScript : MonoBehaviour
{
    public PullAndRelease pullRelease;
    private void OnTriggerEnter(Collider coli)
    {
        if (coli.gameObject.CompareTag("Player"))
        {
            pullRelease.Drag = 5f;
        }
    }

    private void OnTriggerExit(Collider coli)
    {
        if (coli.gameObject.CompareTag("Player"))
        {
            pullRelease.Drag = 0.5f;
        }
    }
}
