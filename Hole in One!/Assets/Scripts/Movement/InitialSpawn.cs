using UnityEngine;
using UnityEngine.InputSystem;

public class InitialSpawn : MonoBehaviour
{
    public GameObject prefab;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        var player1 = PlayerInput.Instantiate(prefab, 0, "WASD", 0, Keyboard.current );
        var player2 = PlayerInput.Instantiate(prefab, 0, "Arrows", 0, Keyboard.current );

        player1.transform.position = new Vector3(-3.642f, 0.399f, -6.98f);
        player2.transform.position = new Vector3(-6f, 0.399f, -6.98f);

        player1.transform.parent.GetChild(1).GetComponent<Camera>().rect = new Rect(0, 0, .5f,1);
        player2.transform.parent.GetChild(1).GetComponent<Camera>().rect = new Rect(.5f, 0, .5f,1);

        player1.transform.parent.GetChild(1).GetComponent<AudioListener>().enabled = true;

        player1.GetComponent<PlayerMovement>().playerIndex = 0;
        player2.GetComponent<PlayerMovement>().playerIndex = 1;
    }

}
