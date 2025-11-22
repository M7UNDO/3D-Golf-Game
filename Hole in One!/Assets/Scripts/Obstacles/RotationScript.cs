using UnityEngine;

public class RotationScript : MonoBehaviour
{
    [Header("Rotation Settings")]
    public float rotationSpeed = 50f;

    public bool rotateOnX;
    public bool rotateOnY;
    public bool rotateOnZ;
    
    void Start()
    {

    }

    void Update()
    {

        if(rotateOnZ)
        {
            transform.Rotate(rotationSpeed * Time.deltaTime, 0, 0, Space.Self);
        }
        else if(rotateOnX)
        {
            transform.Rotate(0, rotationSpeed * Time.deltaTime, 0 , Space.Self);
        }
        
        if (rotateOnY)
        {
            transform.Rotate(0, 0, rotationSpeed * Time.deltaTime, Space.Self);
        }


    }
}