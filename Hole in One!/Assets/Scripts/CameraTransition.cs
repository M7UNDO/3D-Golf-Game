using UnityEngine;
using Cinemachine;

public class CameraTransition : MonoBehaviour
{
    public CinemachineVirtualCamera startCam;
    public CinemachineVirtualCamera gameplayCam;
    public float delay;

    void Start()
    {
        startCam.Priority = 20;
        gameplayCam.Priority = 10;
        Invoke(nameof(SwitchToGameplayCam), delay);
    }

    void SwitchToGameplayCam()
    {
        gameplayCam.Priority = 30; // Now it overrides startCam
    }
}
