using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHold : MonoBehaviour
{
    public float senseX;
    public float senseY;//XY sensitivity 

    public Transform playerOrientation; //player tranform
    public Transform controlPoint; //player tranform

    private float xRotation;
    private float yRotation; //For the camera rotation

    void Start()
    {
        //Cursor.lockState = CursorLockMode.Locked;
        //Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(1))
        {
            float MouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * senseX;
            float MouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * senseY;

            yRotation += MouseX;

            xRotation -= MouseY;

            xRotation = Mathf.Clamp(xRotation, -90f, 90f);//Clamping view
            transform.rotation = Quaternion.Euler(xRotation, yRotation, 0f);
            playerOrientation.rotation = Quaternion.Euler(xRotation, yRotation, 0);

            controlPoint.transform.rotation = transform.localRotation;
        }
        
    }
}
