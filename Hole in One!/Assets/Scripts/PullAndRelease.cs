using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEditor.Experimental.GraphView.GraphView;

public class PullAndRelease : MonoBehaviour
{
    [Header("Pull And Release Movement")]

    public Rigidbody rb;
    public float playerHeight;
    public LayerMask layer;
    public float Drag;
    private float xRotation, yRotation;
    //private float yRotation = 0f;
    public float xSensitivity;
    public float ySensitivity;
    public LineRenderer lineRenderer;
    public float shotPower;
    public float airMultiplyer;
    public Transform playerCamera;
    private Vector3 movementDirection;
    private bool isGrounded;
    //public CinemachineVirtualCamera cinemachineCamera;
    public Transform controlPoint;
    //public float offset = 4f;


    private void Start()
    {

        //if (cinemachineCamera != null)
        //{
        //    cinemachineCamera.LookAt = controlPoint; // Ensure camera follows the control point
       // }

    }

    

    void Update()
    {

        isGrounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, layer);
        if (isGrounded)
        {
             rb.drag = Drag;
        }
        else
        {
             rb.drag = 0;
        }
        //UpdateCameraRotation();
        ShootBall();
      

    }

    /*private void UpdateCameraRotation()
    {
        if (cinemachineCamera != null)
        {
            var cinemachineComponent = cinemachineCamera.GetCinemachineComponent<CinemachinePOV>();

            if (cinemachineComponent != null)
            {
                // Update Cinemachine rotation to match control point
                cinemachineComponent.m_HorizontalAxis.Value = xRotation;
                cinemachineComponent.m_VerticalAxis.Value = yRotation;
            }
        }


    }
    */
  /*
    private void UpdateCameraRotation()
    {
        if (cinemachineCamera != null)
        {
            cinemachineCamera.OnTargetObjectWarped(controlPoint, Vector3.zero); // Forces real-time update
        }
    }
  */
    private void ShootBall()
    {
       transform.position = rb.position;
        if (Input.GetMouseButton(0))
        {

            xRotation += Input.GetAxis("Mouse X") *  xSensitivity;
            yRotation += Input.GetAxis("Mouse Y") * ySensitivity;
            transform.rotation = Quaternion.Euler(yRotation, xRotation, 0f);
            //UpdateCameraRotation();

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
           
            
        }
        
        
    }
}
