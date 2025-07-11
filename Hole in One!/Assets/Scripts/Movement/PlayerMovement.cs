using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody rb;
    private int count;
    private float movementX;
    private float movementY;
    public float speed;
    public float rotationSpeed;
    private ScoreHandler scoreHandler;
    private MenuController menuController;
    private TextMeshProUGUI countText;
    public Transform respawnPoint;
    public int playerIndex;
    public AudioSource pickUpSfx;
    void Start()
    {
        // Get and store the Rigidbody component attached to the player
   
        rb = GetComponent<Rigidbody>();
        menuController = GameObject.Find("Canvas").GetComponent<MenuController>();
        scoreHandler = GameObject.Find("Canvas/CountPanel").GetComponent<ScoreHandler>();
        GameObject shotcount = GameObject.Find("CountText");
        respawnPoint = GameObject.Find("RespawnPoint").GetComponent<Transform>();
        pickUpSfx = transform.GetChild(0).GetComponent<AudioSource>();
        countText = shotcount.GetComponent<TextMeshProUGUI>();
        count = 0;

        // Update the count display.
        SetCountText();
        Respawn();
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
        Debug.Log("OnMove called: " + movementVector);

        // Store the X and Y components of the movement.
        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    // FixedUpdate is called once per fixed frame-rate frame.
    private void FixedUpdate()
    {
        // Create a 3D movement vector using the X and Y inputs.
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);
        //movement.Normalize();
        //transform.Translate(movement * speed * Time.deltaTime, Space.World);

        if(movement != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(movement, Vector3.up);
            
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }

        // Apply force to the Rigidbody to move the player.
        rb.AddForce(movement * speed);
    }


    void OnTriggerEnter(Collider other)
    {
        // Check if the object the player collided with has the "PickUp" tag.
        if (other.gameObject.CompareTag("PickUp"))
        {
            // Deactivate the collided object (making it disappear).
            pickUpSfx.Play();
            other.gameObject.transform.GetChild(0).GetComponent<ParticleSystem>().Play();
            
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
            EndGame();
        }
    }

    // Function to update the displayed count of "PickUp" objects collected.
    void SetCountText()
    {
        menuController.AddCountText(playerIndex, count);
        // Check if the count has reached or exceeded the win condition.
        if (scoreHandler.score >= menuController.winAmount)
        {
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
