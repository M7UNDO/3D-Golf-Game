using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class RotateCamera : MonoBehaviour
{
    [Header("Camera Rotate Speed")]
    [Space(5)]
    public float Speed = 5f;
    public Transform playerCamera;


    public void Lookaround()
    {
        if(Input.GetMouseButton(1))
        {
            transform.eulerAngles += Speed * new Vector3(-Input.GetAxis("Mouse Y"), Input.GetAxis("Mouse X"), 0);
            playerCamera.eulerAngles += Speed * new Vector3(-Input.GetAxis("Mouse Y"), Input.GetAxis("Mouse X"), 0);
        }
       
    }
    
    void Update()
    {
        Lookaround(); 
    }
}
