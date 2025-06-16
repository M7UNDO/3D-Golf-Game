using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEditor.Experimental.GraphView.GraphView;

public class InitialSpawn : MonoBehaviour
{
    public GameObject prefab;
    public bool isSinglePlayer = false;
    public Vector3 player1Start;
    public Vector3 player2Start;
    public Transform p1Pos;
    public Transform p2Pos;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (!isSinglePlayer)
        {
            var player1 = PlayerInput.Instantiate(prefab, 0, "WASD", 0, Keyboard.current);
            var player2 = PlayerInput.Instantiate(prefab, 1, "Arrows", 1, Keyboard.current);

            player1.transform.position = p1Pos.position;
            player2.transform.position = p2Pos.position;

            player1.transform.parent.GetChild(1).GetComponent<Camera>().rect = new Rect(0, 0, .5f, 1);
            player2.transform.parent.GetChild(1).GetComponent<Camera>().rect = new Rect(.5f, 0, .5f, 1);

            player1.transform.parent.GetChild(1).GetComponent<AudioListener>().enabled = true;

            player1.GetComponent<PlayerMovement>().playerIndex = 0;
            player2.GetComponent<PlayerMovement>().playerIndex = 1;
        }
        else if (isSinglePlayer)
        {
            var player1 = PlayerInput.Instantiate(prefab, 0, "WASD", 0, Keyboard.current);
            player1.transform.position = p1Pos.position;

            player1.transform.parent.GetChild(1).GetComponent<Camera>().rect = new Rect(0, 0, 1, 1);

            player1.transform.parent.GetChild(1).GetComponent<AudioListener>().enabled = true;

            player1.GetComponent<PlayerMovement>().playerIndex = 0;
            Debug.Log(player1.GetComponent<PlayerInput>().currentControlScheme);
        }
    }

}

