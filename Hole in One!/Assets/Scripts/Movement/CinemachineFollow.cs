using Cinemachine;
using UnityEngine;

public class CinemachineFollow : MonoBehaviour
{
    [Header("Player")]
    public GameObject ball;
    private Transform ballTransform;
    public CinemachineVirtualCamera cinemachine;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ballTransform = ball.transform.GetChild(0).GetComponent<Transform>();
        cinemachine.Follow = ballTransform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
