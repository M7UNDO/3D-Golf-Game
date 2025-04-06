using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartLevel : MonoBehaviour
{

    public void OnTriggerExit(Collider coli)
    {
        
        if (coli.gameObject.CompareTag("Player"))
        {
            SceneManager.LoadScene("Level 1");
        }
    }

}
