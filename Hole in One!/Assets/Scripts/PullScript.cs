using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PullScript : MonoBehaviour
{
    float xroat, yroat = 0f;
    public Rigidbody ball;
    public float rotatespeed = 5f;
    public LineRenderer Line;
    public float shootpower = 30f;
    private Vector3 movementDirection;
    public Transform playerOrientation;

    
    // Update is called once per frame
    void Update()
    {
        transform.position = ball.position;
        if (Input.GetMouseButton(0))
        {

            xroat += Input.GetAxis("Mouse X") * rotatespeed;
            yroat += Input.GetAxis("Mouse Y") * rotatespeed;
            transform.rotation = Quaternion.Euler(yroat, xroat, 0f);

            Line.enabled = true;
            Line.SetPosition(0, transform.position);
            Line.SetPosition(1, transform.position + transform.forward * 4f);
            yroat = Mathf.Clamp(yroat, -35f, 35f);
        }
        if (Input.GetMouseButtonUp(0))
        {
            movementDirection = ball.transform.forward;
            ball.AddForce(movementDirection.normalized * shootpower * 10f, ForceMode.Force);
            Line.enabled = false;
        }

    }
}
