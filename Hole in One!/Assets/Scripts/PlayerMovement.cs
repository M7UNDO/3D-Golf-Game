using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    [Space(5)]
    private Rigidbody rb;
    [SerializeField] private Transform playerCamera;
    [SerializeField] private float ballSpeed;
    private bool isGrounded;
    public ParticleSystem _particleSystem;
    private Vector2 moveInput;
    private Vector2 lookInput;
    private Vector2 velocity;
    public PlayerControls playerInput;
    public float lookSpeed;
    public float jumpHeight;
    public float rotationSpeed;
   




    [Header("Camera")]
    [Space(5)]
    //public float moveSpeed;
    private float verticalLookRotation;    //private Vector3 velocity;


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

        

        playerInput.Player.Lookaround.performed += ctx => lookInput = ctx.ReadValue<Vector2>();
        playerInput.Player.Lookaround.canceled += ctx => lookInput = Vector2.zero;

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
    public void Jump()
    {

        if (isGrounded == true)
        {

            rb.AddForce(new Vector3 (0f, 4f, 0f), ForceMode.Impulse);
            //rb.AddForce(Vector3.up * jumpHeight);
            //healthBar.fillAmount -= damageAmount;
        }
        

    }

   

    public void Move()
    {

        Vector3 movementDirection = new Vector3(moveInput.x, 0f, moveInput.y) * ballSpeed;

        rb.AddForce(movementDirection);
        movementDirection.Normalize();

        if(movementDirection != Vector3.zero)
        {
            transform.forward = movementDirection;
        }
       

    }


    
}
