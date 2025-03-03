using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PullAndRelease : MonoBehaviour
{
    float xrot, yrot = 0f;
    public Rigidbody ball;
    public float rotatespeed = 5f;
    public LineRenderer line;
    public float shootpower = 30f;

    // Update is called once per frame
    void Update()
    {
        transform.position = ball.position;
        if (Input.GetMouseButton(0))
        {

            xrot += Input.GetAxis("Mouse X") * rotatespeed;
            yrot += Input.GetAxis("Mouse Y") * rotatespeed;
            transform.rotation = Quaternion.Euler(yrot, xrot, 0f);
            //line.gameObject.SetActive(true);
            line.enabled = true;
            line.SetPosition(0, transform.position);
            line.SetPosition(1, transform.position + transform.forward * 4f);
            if (yrot < -35f)
            {

                yrot = -35f;
            }
        }
        if (Input.GetMouseButtonUp(0))
        {

            ball.velocity = transform.forward * shootpower;
            line.enabled = false;
        }

    }
}
