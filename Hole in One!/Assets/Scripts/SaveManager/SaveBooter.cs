using UnityEngine;

public class SaveBooter : MonoBehaviour
{
    public GameObject saveManagerPrefab;

    void Awake()
    {
        if (Save.instance == null)
        {
            Instantiate(saveManagerPrefab);
        }

        Destroy(gameObject); // optional: clean up the booter object
    }
}