using System.Collections;
using TMPro;
using UnityEditor.ShaderGraph;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PullAndRelease : MonoBehaviour
{
    [Header("References")]
    [SerializeField] PowerScript powerScript;
    public bool isLevel1 = false;
    public bool isLevel2 = false;
    public bool isLevel3 = false;
    public LevelUI levelUI;
    public Color ObjectivePassedColour;
    [Header("Player")]
    [Header("Shot Count")]
    [Space(5)]
    public float NumberOfShots;
    public TextMeshProUGUI shotsTxt;
    private Transform lastPos;

    [Header("Pull And Release Mechanic")]
    [Space(5)]
    [SerializeField] private Rigidbody rb;
    public float playerHeight;
    public LayerMask groundLayer;
    public float Drag;
    private float xRotation = 0f;
    private float yRotation = 0f;

    public LineRenderer lineRenderer;
    public float airMultiplyer;
    private Vector3 movementDirection;
    private bool isGrounded;

    [Header("Rotation Sensitivity")]
    [Space(5)]
    public float xSensitivity;
    public float ySensitivity;

    [Header("Set Power")]
    [Space(5)]
    public float minShotPower = 0.5f;
    public float maxShotPower = 2f;
    public TextMeshProUGUI powerLevel;
    public AudioSource pullSfx;
    public AudioSource releaseSfx;
    public Slider powerSlider;
    private bool isCyclingPower = false;

    private PlayerControls playerInput;
    private System.Action<InputAction.CallbackContext> powerCallback;



    public void Start()
    {

    }
    void Update()
    {
        /*if (Input.GetKeyDown(KeyCode.R))
        {
            Save.instance.ResetSave();
        }*/

        if (Time.timeScale == 0f) return;
        if(isLevel1 ==false)
        {
            shotsTxt.text = NumberOfShots.ToString();
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
    }
    private void AimingBall()
    {
        
       transform.position = rb.position;
        if (Input.GetMouseButtonDown(0))
        {
            pullSfx.Play();
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


            if(isLevel1 == true)
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


            }




        }
        
        
    }

    public void Shoot()
    {

        releaseSfx.Play();
        movementDirection = transform.forward;

        float shootingPower = Mathf.Lerp(minShotPower, maxShotPower, powerScript.GetPowerValue());
            if (isGrounded)
            {
                rb.AddForce(movementDirection.normalized * shootingPower * 10f, ForceMode.Impulse);
                lineRenderer.enabled = false;
                 print("Shot Power: " + shootingPower);
            }
            TrackShots();

    }

    IEnumerator DisplayUIOff()
    {
        yield return new WaitForSeconds(3f);
        if(isLevel1 == true)
        {
            levelUI.UIElements[1].SetActive(false);

        }

        if (isLevel2 == true)
        {
            levelUI.UIElements[1].SetActive(false);

        }

    }
}
