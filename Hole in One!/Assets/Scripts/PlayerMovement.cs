using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;


public class PlayerMovement : MonoBehaviour
{
    public UIManager uiManager;
    public PlayerControls playerInput;
    [Header("Movement")]
    [Space(5)]

    private Rigidbody rb;
    public float moveSpeed;
    public Transform playerOrientation;
    private Vector3 movementDirection;
    private Vector3 jumpDirection;
    private Vector2 moveInput;


    [Header("Jumping")]
    [Space(5)]

    public float playerJumpForce;
    public float playerHeight;
    public float jumpcoolDown;
    private bool canJump;
    private bool isGrounded;
    public LayerMask layer;
    public float ballDrag;
    public float multiplyer;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        ResetJump();
        
    }

    private void OnEnable()
    {
        var playerInput = new PlayerControls();
        playerInput.Player.Enable();

        playerInput.Player.Movement.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        
        playerInput.Player.Movement.canceled += ctx => moveInput = Vector2.zero;

        playerInput.Player.Jump.performed += ctx => JumpCheck();

        //playerInput.Player.Pause.performed += ctx => uiManager.Pause();


    }

    // Update is called once per frame
    void Update()
    {
        
        Move();
        
        isGrounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, layer);
        //print(healthBar.fillAmount);  
        if(isGrounded)
        {
            rb.linearDamping = ballDrag;
        }
        else
        {
            rb.linearDamping = 0;
        }
        //print(canJump);
        //JumpCheck();
        LimitSpeed();
    }

    private void LimitSpeed()
    {
        Vector3 flatVelocity = new Vector3 (rb.linearVelocity.x,0f, rb.linearVelocity.z);
        if(flatVelocity.magnitude > moveSpeed)
        {
            Vector3 limtedVelocity = flatVelocity.normalized * moveSpeed;
            rb.linearVelocity = new Vector3(limtedVelocity.x, rb.linearVelocity.y, limtedVelocity.z);
        }
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
        //rb.AddForce(movementDirection.normalized * moveSpeed * 10f, ForceMode.Force);

        if (isGrounded)
        {
            rb.AddForce(movementDirection.normalized * moveSpeed * 10f, ForceMode.Force);
        }
        else if (!isGrounded)
        {
            rb.AddForce(movementDirection.normalized * moveSpeed * 10f * multiplyer, ForceMode.Force);
        }

    }

    

    private void JumpCheck()
    {
        if(canJump && isGrounded)
        {
            canJump = false;
            Jump();
            Invoke(nameof(ResetJump), jumpcoolDown);  
        }
    }
    public void Jump()
    {
        rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0.5f, rb.linearVelocity.z);
        if (isGrounded == true)
        {
            //jumpDirection = new Vector3(0f, 0.5f, 0f);
            rb.AddForce(transform.up * playerJumpForce, ForceMode.Impulse);
            print("I JUST JUMPED");
        }
        

    }

    private void ResetJump()
    {
        canJump = true;
    }

}
