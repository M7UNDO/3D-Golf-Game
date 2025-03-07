using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEditor.Experimental.GraphView.GraphView;
using TMPro;

public class PullAndRelease : MonoBehaviour
{
    [Header("Shot Count")]
    [Space(5)]
    public float NumberOfShots;
    public TextMeshProUGUI shotsTxt;

    [Header("Pull And Release Mechanic")]
    [Space(5)]
    public Rigidbody rb;
    public float playerHeight;
    public LayerMask layer;
    public float Drag;
    private float xRotation = 0f;
    private float yRotation = 0f;

    public LineRenderer lineRenderer;
    public float shotPower;
    public float airMultiplyer;
    private Vector3 movementDirection;
    private bool isGrounded;
    public Transform controlPoint;

    [Header("Rotation Sensitivity")]
    [Space(5)]
    public float xSensitivity;
    public float ySensitivity;


    public void Start()
    {
       
    }
    void Update()
    {
        shotsTxt.text = NumberOfShots.ToString();
        isGrounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, layer);// Shoot a raycast onto the ground to determain what the drag//Potential to use this for different kinds of ground types
        if (isGrounded)
        {
             rb.drag = Drag;
        }
        else
        {
             rb.drag = 0;
        }
       
        ShootBall();
      
    }

    private void TrackShots()
    {
        NumberOfShots++;
    }

    private void ShootBall()
    {
       transform.position = rb.position;
        
        if (Input.GetMouseButton(0))
        {

            xRotation += Input.GetAxis("Mouse X") *  xSensitivity;
            yRotation += Input.GetAxis("Mouse Y") * ySensitivity;
            transform.rotation = Quaternion.Euler(yRotation, xRotation, 0f); // transform the rotation of the golf ball

            lineRenderer.enabled = true;
            lineRenderer.SetPosition(0, transform.position);
            lineRenderer.SetPosition(1, transform.position + transform.forward * 4f);
            yRotation = Mathf.Clamp(yRotation, -35f, 35f);
        }
        

        if (Input.GetMouseButtonUp(0))
        {
            movementDirection = transform.forward;
            if (isGrounded)
            {
                rb.AddForce(movementDirection.normalized * shotPower * 10f, ForceMode.Impulse);
                lineRenderer.enabled = false;
            }
            else if(!isGrounded)
            {
                rb.AddForce(movementDirection.normalized * shotPower * 10f * airMultiplyer, ForceMode.Impulse);
            }

            TrackShots();
            print(NumberOfShots);
            
        }
        
        
    }
}
