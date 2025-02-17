using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Unity.VisualScripting;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody rb;
    private bool isGrounded;

    [Header("Moving and Looking Around")]
    [Space(5)]
    //public float moveSpeed;
    public float lookSpeed;
    public float jumpHeight;
    public PlayerControls playerInput;
    private Vector2 moveInput;
    private Vector3 velocity;
    
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
        Move();
        Jump();
        //Debug.Log(isGrounded);
        //ApplyGravity();
        //LookAround();
        //print(healthBar.fillAmount);
    }

    private void OnCollisionEnter(Collision coli)
    {
        if(coli.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
        Debug.Log(coli.gameObject);
    }

    private void OnEnable()
    {
        var playerInput = new PlayerControls();
        playerInput.Player.Enable();

        playerInput.Player.Movement.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        playerInput.Player.Movement.canceled += ctx => moveInput = Vector2.zero;

        playerInput.Player.Jumping.performed += ctx => Jump();


    }

    /*private void OnDisable()
    {
        var playerInput = new PlayerControls();
        playerInput.Player.Movement.performed -= ctx => moveInput = ctx.ReadValue<Vector2>();
        playerInput.Player.Movement.canceled -= ctx => moveInput = Vector2.zero;

       
    }
    */
    public void Jump()
    {
        
        if(isGrounded == true)
        {

            rb.AddForce(Vector3.up * jumpHeight);
            //velocity.y = Mathf.Sqrt(jumpHeight);
            //healthBar.fillAmount -= damageAmount;

        }
       
    }

    public void Move()
    {
        /* Vector3 move = new Vector3(moveInput.x, 0, moveInput.y);

         move = transform.TransformDirection(move);
         float currentSpeed = moveSpeed;
         characterController.Move(move * currentSpeed * Time.deltaTime);

         float actualSpeed = characterController.velocity.magnitude;
       */

        rb.AddForce(moveInput.x, 0, moveInput.y);

    }


    /*public void ApplyGravity()
    {
        if (characterController.isGrounded && velocity.y < 0)
        {
            velocity.y = -0.5f;
        }

        velocity.y += gravity * Time.deltaTime;
        characterController.Move(velocity * Time.deltaTime); 
    }
   */
}
