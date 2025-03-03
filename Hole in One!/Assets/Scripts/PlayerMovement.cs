using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerMovement : MonoBehaviour
{
    public PlayerControls playerInput;
    [Header("Movement")]
    [Space(5)]

    private Rigidbody rb;
    public float moveSpeed;
    public Transform playerOrientation;
    private Vector3 movementDirection;
    private Vector2 moveInput;


    [Header("Jumping")]
    [Space(5)]

    public float jumpHeight;
    private bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

        Move();
        
        //print(healthBar.fillAmount);
    }

    

    private void OnCollisionEnter(Collision coli)
    {
        if (coli.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
        else if (coli.gameObject.CompareTag("Wall"))
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
        Debug.Log(coli.gameObject);
    }

    private void OnCollisionExit(Collision coli)
    {
        if (coli.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
        else if (coli.gameObject.CompareTag("Wall"))
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = true;
        }
    }

    private void OnTriggerEnter(Collider coli)
    {
        Debug.Log(coli.gameObject);
    }

    private void OnEnable()
    {
        var playerInput = new PlayerControls();
        playerInput.Player.Enable();

        playerInput.Player.Movement.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        playerInput.Player.Movement.canceled += ctx => moveInput = Vector2.zero;

        playerInput.Player.Jump.performed += ctx => Jump();


    }

    /*
    private void OnDisable()
    {
        
        playerInput.Player.Movement.performed -= ctx => moveInput = ctx.ReadValue<Vector2>();
        playerInput.Player.Movement.canceled -= ctx => moveInput = Vector2.zero;

        playerInput.Player.Jump.performed -= ctx => Jump();

        playerInput.Player.Lookaround.performed -= ctx => lookInput = ctx.ReadValue<Vector2>();
        playerInput.Player.Lookaround.canceled -= ctx => lookInput = Vector2.zero;

        playerInput.Disable();

    }
    */
    public void Move()
    {

        movementDirection = playerOrientation.forward * moveInput.y + playerOrientation.right * moveInput.x;
        rb.AddForce(movementDirection.normalized * moveSpeed * 10f, ForceMode.Force);




    }
    public void Jump()
    {

        if (isGrounded == true)
        {

            rb.AddForce(new Vector3 (0f, 4f, 0f), ForceMode.Impulse);

        }
        

    }

}
