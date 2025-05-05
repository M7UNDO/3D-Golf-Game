using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using TMPro;
using Unity.VisualScripting;
using System.Threading;

public class PullAndRelease : MonoBehaviour
{
    public bool isLevel1 = false;
    public bool isLevel2 = false;
    public bool isLevel3 = false;
    public LevelUI levelUI;
    public Color ObjectivePassedColour;
    [Header("Player")]

    public GameObject ball;
    [Header("Shot Count")]
    [Space(5)]
    public float NumberOfShots;
    public TextMeshProUGUI shotsTxt;
    private Transform lastPos;

    [Header("Pull And Release Mechanic")]
    [Space(5)]
    private Rigidbody rb;
    public float playerHeight;
    public LayerMask layer;
    public LayerMask layer2;
    public float Drag;
    private float xRotation = 0f;
    private float yRotation = 0f;

    public LineRenderer lineRenderer;
    public float shotPower;
    public float airMultiplyer;
    private Vector3 movementDirection;
    private bool isGrounded;

    [Header("Rotation Sensitivity")]
    [Space(5)]
    public float xSensitivity;
    public float ySensitivity;

    [Header("Set Power")]
    [Space(5)]

    public float HighShotAirMultiplyer;
    public float LowMediumAirMultiplyer;
    public float lowPowerShot = 0.5f;
    public float mediumPowerShot = 1.2f;
    public float highPowerShot = 2f;
    public TextMeshProUGUI powerLevel;
    public AudioSource pullSfx;
    public AudioSource releaseSfx;
    //public ParticleEffectScript particleEffectScript;

    private PlayerControls playerInput;
    private System.Action<InputAction.CallbackContext> powerCallback;



    public void Start()
    {
        rb = ball.transform.GetChild(0).GetComponent<Rigidbody>();
        lineRenderer = ball.transform.GetChild(0).GetComponent<LineRenderer>();
        //particleEffectScript.particles = ball.transform.GetChild(0).GetChild(2).GetComponent<ParticleSystem>();
        
        shotPower = mediumPowerShot;

        if(isLevel1 == false)
        {
            powerLevel.color = Color.green;
            powerLevel.text = "Medium Power Shot";
        }



    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Save.instance.ResetSave();
        }

        if (Time.timeScale == 0f) return;
        if(isLevel1 ==false)
        {
            shotsTxt.text = NumberOfShots.ToString();
        }
        
        isGrounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, layer);// Shoot a raycast onto the ground to determain what the drag//Potential to use this for different kinds of ground types


        if (isGrounded)
        {
             rb.drag = Drag;
        }
        else
        {
             rb.drag = 0.5f;
        }

        if(isLevel1 == true || isLevel2 == true)
        {
            if(levelUI.tutorialUI[1].color == ObjectivePassedColour && levelUI.tutorialUI[2].color == ObjectivePassedColour)
            {
                levelUI.tutorialUI[0].color = ObjectivePassedColour;
                StartCoroutine(DisplayUIOff());
            }
        }
       


        ShootBall();
      
    }


    void OnEnable()
    {
        playerInput = new PlayerControls();
        playerInput.Player.Enable();

        powerCallback = ctx => SetPower();
        playerInput.Player.SetPower.performed += powerCallback;
    }

    void OnDisable()
    {
        playerInput.Player.SetPower.performed -= powerCallback;
        playerInput.Player.Disable();
    }
    private void TrackShots()
    {
        NumberOfShots++;
    }

    private void SetPower()
    {
        if (isLevel1 == true)
        {
            return;

        }

        if (isLevel2 == true)
        {

            levelUI.tutorialUI[2].color = ObjectivePassedColour;
            levelUI.tutorialarrow[1].color = ObjectivePassedColour;
        }
        if (shotPower == mediumPowerShot)
        {
            airMultiplyer = LowMediumAirMultiplyer;
            shotPower = highPowerShot;
            powerLevel.color = Color.red;
            powerLevel.text = "High Power Shot";
            print(shotPower);
        }
        else if (shotPower == highPowerShot)
        {
            airMultiplyer = HighShotAirMultiplyer;
            shotPower = lowPowerShot;
            powerLevel.color = Color.yellow;
            powerLevel.text = "Low Power Shot";
            print(shotPower);
        }
        else if(shotPower == lowPowerShot)
        {
            airMultiplyer = LowMediumAirMultiplyer;
            shotPower = mediumPowerShot;
            powerLevel.color = Color.green;
            powerLevel.text = "Medium Power Shot";
            print(shotPower);
        }
    }

    private void ShootBall()
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
        

        if (Input.GetMouseButtonUp(0))
        {
            
            releaseSfx.Play();
            movementDirection = transform.forward;
            if (isGrounded)
            {   
                rb.AddForce(movementDirection.normalized * shotPower * 10f, ForceMode.Impulse);
                lineRenderer.enabled = false;
            }
            else if(!isGrounded)
            {
                rb.AddForce(movementDirection.normalized * shotPower * 10f * airMultiplyer, ForceMode.Impulse);
            }

            TrackShots();
            print(NumberOfShots);

            if(isLevel1 == true)
            {
                levelUI.tutorialUI[2].color = ObjectivePassedColour;
                levelUI.tutorialarrow[1].color = ObjectivePassedColour;
            }

            if (isLevel2 == true)
            {
                levelUI.tutorialUI[2].color = ObjectivePassedColour;
                levelUI.tutorialarrow[1].color = ObjectivePassedColour;
            }


        }
        
        
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
