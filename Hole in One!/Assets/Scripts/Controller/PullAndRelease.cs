using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

public class PullAndRelease : MonoBehaviour
{
    [Header("References")]
    [SerializeField] PowerScript powerScript;

    [Header("Player")]
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float playerHeight;
    [SerializeField] private LineRenderer lineRenderer;

    [Header("Shot Count")]
    [Space(5)]
    public float NumberOfShots;
    public TextMeshProUGUI shotsTxt;

    [Header("Ball Physics")]
    [Space(5)]
    public LayerMask groundLayer;
    public float Drag;
    public float airMultiplyer;
    private Vector3 movementDirection;
    private bool isGrounded;

    [Header("Rotation Sensitivity")]
    [Space(5)]
    private float xRotation = 0f;
    private float yRotation = 0f;
    public float xSensitivity;
    public float ySensitivity;

    [Header("Power Settings")]
    [Space(5)]
    public float minShotPower = 0.5f;
    public float maxShotPower = 2f;
    public TextMeshProUGUI powerLevel;


    [Header("SFX")]
    [Space(5)]
    public AudioSource pullSfx;
    public AudioSource releaseSfx;


    
    void Update()
    {

        if (PauseScript.IsGamePaused || Time.timeScale == 0f) return;

        if (!powerScript.charging)
        {
            lineRenderer.enabled = false;
            return;
        }
        
        isGrounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, groundLayer);// Shoot a raycast onto the ground to determain what the drag//Potential to use this for different kinds of ground types


        if (isGrounded)
        {
             rb.linearDamping = Drag;
        }
        else
        {
             rb.linearDamping = 0.5f;
        }
       


        AimingBall();
      
    }


    private void TrackShots()
    {
        NumberOfShots++;
        shotsTxt.text = NumberOfShots.ToString();
    }
    private void AimingBall()
    {
        
       transform.position = rb.position;
        if(Input.GetMouseButtonDown(0))
        {
            if(pullSfx != null) pullSfx.Play();
        }

        if (Input.GetMouseButton(0))
        {
            
            
            xRotation += Input.GetAxis("Mouse X") *  xSensitivity;
            yRotation += Input.GetAxis("Mouse Y") * ySensitivity;
            transform.rotation = Quaternion.Euler(yRotation, xRotation, 0f); // transform the rotation of the golf ball

            lineRenderer.enabled = true;
            lineRenderer.SetPosition(0, transform.position);
            lineRenderer.SetPosition(1, transform.position + transform.forward * 4f);
            yRotation = Mathf.Clamp(yRotation, -35f, 35f);


            /*if(isLevel1 == true)
            {
                if (Mathf.Abs(xRotation) > 0.01f)
                {
                    if (xRotation > 0)
                    {
                        levelUI.tutorialUI[1].color = ObjectivePassedColour;
                        levelUI.tutorialarrow[0].color = ObjectivePassedColour;
                    }
                    else
                    {
                        levelUI.tutorialUI[1].color = ObjectivePassedColour;
                        levelUI.tutorialarrow[0].color = ObjectivePassedColour;
                    }
                }

                
            }

            if (isLevel2 == true)
            {
                if (Mathf.Abs(xRotation) > 0.01f)
                {
                    if (yRotation > 0)
                    {
                        levelUI.tutorialUI[1].color = ObjectivePassedColour;
                        levelUI.tutorialarrow[0].color = ObjectivePassedColour;
                    }
                    else
                    {
                        levelUI.tutorialUI[1].color = ObjectivePassedColour;
                        levelUI.tutorialarrow[0].color = ObjectivePassedColour;
                    }
                }


            }*/




        }
        
        
    }

    public void Shoot()
    {
        if(releaseSfx != null) releaseSfx.Play();

            
        movementDirection = transform.forward;

        float shootingPower = Mathf.Lerp(minShotPower, maxShotPower, powerScript.GetPowerValue());
            if (isGrounded)
            {
                rb.AddForce(movementDirection.normalized * shootingPower * 10f, ForceMode.Impulse);
                lineRenderer.enabled = false;
                TrackShots();
            }
            

    }
}
