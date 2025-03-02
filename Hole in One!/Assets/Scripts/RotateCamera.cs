using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCamera : MonoBehaviour
{
    public PlayerControls playerInput;
    public float Speed = 5f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnEnable()
    {
        var playerInput = new PlayerControls();
        playerInput.Player.Enable();
        playerInput.Player.RotateCam.performed += ctx => Lookaround();

    }

    public void Lookaround()
    {
        transform.eulerAngles += Speed * new Vector3(-Input.GetAxis("Mouse Y"), Input.GetAxis("Mouse X"), 0);

    }
    // Update is called once per frame
    void Update()
    {
        Lookaround(); 
    }
}
