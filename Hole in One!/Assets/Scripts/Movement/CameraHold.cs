using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHold : MonoBehaviour
{
    public Transform target; //For the camera rotation
    public float RotationSpeed;

    [Header("Player")]
    public GameObject ball;
    private Transform ballTransform;

    private void Start()
    {
        ballTransform = ball.transform.GetChild(0).GetComponent<Transform>();
    }

    private void FixedUpdate()
    {

        //store x position and update y and z
        Quaternion targetRotation = Quaternion.Euler(transform.rotation.eulerAngles.x, target.rotation.eulerAngles.y, target.rotation.eulerAngles.z);

        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, RotationSpeed * Time.deltaTime);

    }
}
