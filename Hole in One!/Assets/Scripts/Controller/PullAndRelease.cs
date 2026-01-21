using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

public class PullAndRelease : MonoBehaviour
{
    [Header("References")]
    [SerializeField] PowerScript powerScript;

    [Header("Input Actions")]
    private InputActionMap player;
    private InputActionAsset inputAsset;
    private InputAction look;
    private InputAction charge;

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

    private void Awake()
    {
        inputAsset = rb.gameObject.GetComponent<PlayerInput>().actions;
        player = inputAsset.FindActionMap("Player");
        
    }

    private void OnEnable()
    {
        look = player.FindAction("LookAround");
        charge = player.FindAction("Charge");
        player.Enable();
    }


    void Update()
    {

        if (PauseScript.IsGamePaused || Time.timeScale == 0f) return;

        
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
        if(charge.WasPressedThisFrame())
        {
            if(pullSfx != null) pullSfx.Play();
        }

        Vector2 lookInput = look.ReadValue<Vector2>();

        if (charge.IsPressed())
        {
            
            
            xRotation += lookInput.x *  xSensitivity;
            yRotation += lookInput.y * ySensitivity;
            transform.rotation = Quaternion.Euler(yRotation, xRotation, 0f); // transform the rotation of the golf ball

            lineRenderer.enabled = true;
            Vector3 startPos = transform.position;
            Vector3 direction = transform.forward;

            lineRenderer.positionCount = 3;
            lineRenderer.SetPosition(0, startPos);

            // Detect downward aim
            bool aimingDown = Vector3.Dot(direction, Vector3.down) > 0.3f;

            if (aimingDown)
            {
                // Project forward onto ground plane
                if (Physics.Raycast(startPos + Vector3.up * 0.1f, Vector3.down, out RaycastHit groundHit, 5f))
                {
                    // Find a forward point along the ground
                    Vector3 forwardOnGround = groundHit.point + (Vector3.ProjectOnPlane(direction, groundHit.normal).normalized * 3f);

                    lineRenderer.positionCount = 2;
                    lineRenderer.SetPosition(1, forwardOnGround + groundHit.normal * 0.05f);
                }
            }
            else
            {
                // Normal wall/bounce logic
                if (Physics.Raycast(startPos, direction, out RaycastHit hit, 4f))
                {
                    Vector3 hitPoint = hit.point + hit.normal * 0.05f;
                    lineRenderer.SetPosition(1, hitPoint);

                    // Bounce prediction
                    Vector3 reflected = Vector3.Reflect(direction, hit.normal);
                    lineRenderer.SetPosition(2, hitPoint + reflected * 2f);
                }
                else
                {
                    lineRenderer.positionCount = 2;
                    lineRenderer.SetPosition(1, startPos + direction * 4f);
                }
            }

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
        else
        {
            lineRenderer.enabled = false;
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
