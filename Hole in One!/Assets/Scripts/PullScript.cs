using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PullScript : MonoBehaviour
{
    [Header("Pull And Release Movement")]

    private float xRotation = 0f;
    private float yRotation = 0f;
    public float xSensitivity;
    public float ySensitivity;
    //public LineRenderer lineRenderer;
    public Transform playerOrientation;
    public PullAndRelease pillrelease;




    private void Start()
    {


    }



    void Update()
    {

        

        Rotate ();


    }

    private void Rotate()
    {
       

        if (Input.GetMouseButton(1))
        {

            xRotation += Input.GetAxis("Mouse X") * xSensitivity;
            yRotation += Input.GetAxis("Mouse Y") * ySensitivity;
            transform.rotation = Quaternion.Euler(yRotation, xRotation, 0f);
            playerOrientation.rotation = Quaternion.Euler(yRotation, xRotation, 0f);
            playerOrientation.transform.forward = transform.forward;
            yRotation = Mathf.Clamp(yRotation, -35f, 35f);

          /*  lineRenderer.enabled = true;
            lineRenderer.SetPosition(0, transform.position);
            lineRenderer.SetPosition(1, transform.position + transform.forward * 4f);
           */
        }


        


    }
}
