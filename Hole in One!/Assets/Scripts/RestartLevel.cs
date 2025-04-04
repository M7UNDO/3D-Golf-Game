using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartLevel : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void OnTriggerExit(Collider coli)
    {
        
        if (coli.gameObject.CompareTag("Player"))
        {
            SceneManager.LoadScene("Level 1");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
