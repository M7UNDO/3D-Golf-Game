using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleScript : MonoBehaviour
{
    [SerializeField]
    private Vector3 pointA;
    [SerializeField]
    private Vector3 pointB;
    [SerializeField]
    private float moveSpeed;
    private Vector3 platformMotion;
    private Vector3 targetPosition; //Between point A and B
    private Vector3 lastPosition;
    Rigidbody rb;
    public bool StartingPositionIsA;

    void Start()
    {
        targetPosition = pointB;
        lastPosition = transform.position;
        if (StartingPositionIsA)
        {
            pointA = transform.position;
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

        platformMotion = transform.position - lastPosition;
        lastPosition = transform.position;

        //Target Switching

        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            targetPosition = targetPosition == pointA ? pointB : pointA;
        }

    }

}