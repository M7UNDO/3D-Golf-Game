using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody rb;
    private int count;
    private float movementX;
    private float movementY;
    public float speed = 0;
    private ScoreHandler scoreHandler;
    private MenuController menuController;
    private TextMeshProUGUI countText;
    public Transform respawnPoint;
    public int playerIndex;
    void Start()
    {
        // Get and store the Rigidbody component attached to the player.
        rb = GetComponent<Rigidbody>();
        menuController = GameObject.Find("Canvas").GetComponent<MenuController>();
        scoreHandler = GameObject.Find("Canvas/CountPanel").GetComponent<ScoreHandler>();
        GameObject shotcount = GameObject.Find("CountText");
        respawnPoint = GameObject.Find("RespawnPoint").GetComponent<Transform>();
        countText = shotcount.GetComponent<TextMeshProUGUI>();

        count = 0;

        // Update the count display.
        SetCountText();
    }

    private void Update()
    {
        if (transform.position.y < -10)
        {
            Respawn();
        }
    }

    // This function is called when a move input is detected.
    void OnMove(InputValue movementValue)
    {
        // Convert the input value into a Vector2 for movement.
        Vector2 movementVector = movementValue.Get<Vector2>();

        // Store the X and Y components of the movement.
        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    // FixedUpdate is called once per fixed frame-rate frame.
    private void FixedUpdate()
    {
        // Create a 3D movement vector using the X and Y inputs.
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);

        // Apply force to the Rigidbody to move the player.
        rb.AddForce(movement * speed);
    }


    void OnTriggerEnter(Collider other)
    {
        // Check if the object the player collided with has the "PickUp" tag.
        if (other.gameObject.CompareTag("PickUp"))
        {
            // Deactivate the collided object (making it disappear).
            other.gameObject.SetActive(false);

            // Increment the count of "PickUp" objects collected.
            count = count + 1;
            scoreHandler.score++;
            Debug.Log(count);

            // Update the count display.
            SetCountText();
        }
    }

    private void OnCollisionEnter(Collision coli)
    {
        if (coli.gameObject.CompareTag("Enemy"))
        {
            //Respawn();
            EndGame();
        }
    }

    // Function to update the displayed count of "PickUp" objects collected.
    void SetCountText()
    {
        // Update the count text with the current count.
        //countText.text = "Count: " + count.ToString();
        menuController.AddCountText(playerIndex, count);
        // Check if the count has reached or exceeded the win condition.
        if (scoreHandler.score >= 12)
        {
            // Display the win text.
            //winTextObject.SetActive(true);
            menuController.WinGame();
        }
    }

    public void Respawn()
    {
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        rb.Sleep();
        transform.position = respawnPoint.position;
    }

    public void EndGame()
    {
        menuController.LoseGame();
        gameObject.SetActive(false);
    }
}
