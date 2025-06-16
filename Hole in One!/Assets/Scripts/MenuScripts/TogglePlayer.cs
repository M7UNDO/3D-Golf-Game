using UnityEngine;

public class TogglePlayer : MonoBehaviour
{
    public GameObject[] mainMenuElement;
    public GameObject[] multiplayerMenu;
    private bool toggle;
    private bool isMultiplayerMenu;
    
    public void OpenPlayer()
    {
        toggle = !toggle;

        if (toggle)
        {
            foreach (GameObject menuElement in mainMenuElement)
            {
                menuElement.SetActive(false);
            }

            foreach (GameObject multiElement in multiplayerMenu)
            {
                multiElement.SetActive(true);
            }
        }

        if (!toggle)
        {
            foreach (GameObject menuElement in mainMenuElement)
            {
                menuElement.SetActive(true);
            }

            foreach (GameObject multiElement in multiplayerMenu)
            {
                multiElement.SetActive(false);
            }
        }
    }

}
