using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartLevel : MonoBehaviour
{
    public int levelIndex;
    public PlayerMovement playerMovement;

    public void OnTriggerExit(Collider coli)
    {
        
        if (coli.gameObject.CompareTag("Player"))
        {
            SceneManager.LoadScene(levelIndex);
        }
    }

    public void OnCollisionEnter(Collision coli)
    {
        if (coli.gameObject.CompareTag("Player"))
        {
            playerMovement = coli.gameObject.GetComponent<PlayerMovement>();
            //SceneManager.LoadScene(levelIndex);
            playerMovement.Respawn();
        }
    }

}
