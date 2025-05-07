using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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

    private bool gameStarted = false;

    [Header("Intro Transition")]
    public float introDelay = 2.5f; // match your Cinemachine blend
    public CanvasGroup fadeCanvas;
    [Header("Player")]

    public GameObject ball;
    [Header("Shot Count")]
    [Space(5)]
    public float NumberOfShots;
    public TextMeshProUGUI shotsTxt;
    private Transform lastPos;
    public GameObject[] startUI;

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
    private bool canShoot = true;
    public float shootCooldown = 1f;

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
    public Slider powerSlider;
private bool isCyclingPower = false;
    //public ParticleEffectScript particleEffectScript;

    private PlayerControls playerInput;
    private System.Action<InputAction.CallbackContext> powerCallback;



    public void Start()
    {
        if(isLevel1 ==false)
        {
            StartCoroutine(DisplayUI());
        }

        StartCoroutine(DelayedStart());

        rb = ball.transform.GetChild(0).GetComponent<Rigidbody>();
        rb.collisionDetectionMode = CollisionDetectionMode.Continuous;
        lineRenderer = ball.transform.GetChild(0).GetComponent<LineRenderer>();
        //particleEffectScript.particles = ball.transform.GetChild(0).GetChild(2).GetComponent<ParticleSystem>();

        //shotPower = mediumPowerShot;
        if(isLevel1 == false)
        {
            powerSlider.minValue = lowPowerShot;
            powerSlider.maxValue = highPowerShot;
            powerSlider.value = mediumPowerShot;
            powerSlider.onValueChanged.AddListener(UpdatePowerFromSlider);
        }
        

        if (isLevel1 == false)
        {
            powerLevel.color = Color.green;
            powerLevel.text = "Medium Power Shot";
        }



    }
    void Update()
    {
        if (!gameStarted || Time.timeScale == 0f) return;

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
             rb.linearDamping = Drag;
        }
        else
        {
             rb.linearDamping = 0.5f;
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

    private void UpdatePowerFromSlider(float value)
    {
        if (isCyclingPower) return; // prevent feedback loop
        shotPower = value;

        if (Mathf.Approximately(value, lowPowerShot))
        {
            powerLevel.color = Color.yellow;
            powerLevel.text = "Low Power Shot";
            airMultiplyer = LowMediumAirMultiplyer;
        }
        else if (Mathf.Approximately(value, mediumPowerShot))
        {
            powerLevel.color = Color.green;
            powerLevel.text = "Medium Power Shot";
            airMultiplyer = LowMediumAirMultiplyer;
        }
        else if (Mathf.Approximately(value, highPowerShot))
        {
            powerLevel.color = Color.red;
            powerLevel.text = "High Power Shot";
            airMultiplyer = HighShotAirMultiplyer;
        }
    }

    private void TrackShots()
    {
        NumberOfShots++;
    }

    private void SetPower()
    {
        if (isLevel1) return;

        if (isLevel2)
        {
            levelUI.tutorialUI[2].color = ObjectivePassedColour;
            levelUI.tutorialarrow[1].color = ObjectivePassedColour;
        }

        isCyclingPower = true;

        if (Mathf.Approximately(shotPower, mediumPowerShot))
        {
            shotPower = highPowerShot;
            airMultiplyer = HighShotAirMultiplyer;
            powerLevel.color = Color.red;
            powerLevel.text = "High Power Shot";
        }
        else if (Mathf.Approximately(shotPower, highPowerShot))
        {
            shotPower = lowPowerShot;
            airMultiplyer = LowMediumAirMultiplyer;
            powerLevel.color = Color.yellow;
            powerLevel.text = "Low Power Shot";
        }
        else
        {
            shotPower = mediumPowerShot;
            airMultiplyer = LowMediumAirMultiplyer;
            powerLevel.color = Color.green;
            powerLevel.text = "Medium Power Shot";
        }

        powerSlider.value = shotPower;

        isCyclingPower = false;
    }


    private void ShootBall()
    {
        
       transform.position = rb.position;
        if (Input.GetMouseButtonDown(0))
        {
            if (!isGrounded) return;
            pullSfx.Play();
        }

        if (Input.GetMouseButton(0))
        {

            if (!isGrounded) return;
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
            if (!isGrounded || !canShoot)
            {
                lineRenderer.enabled = false;
                return;
            }

            releaseSfx.Play();
            movementDirection = transform.forward;
            rb.AddForce(movementDirection.normalized * shotPower * 10f, ForceMode.Impulse);
            lineRenderer.enabled = false;

            TrackShots();
            print(NumberOfShots);

            if (isLevel1 || isLevel2)
            {
                levelUI.tutorialUI[2].color = ObjectivePassedColour;
                levelUI.tutorialarrow[1].color = ObjectivePassedColour;
            }

            StartCoroutine(ShootCooldown());
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

    IEnumerator ShootCooldown()
    {
        canShoot = false;
        yield return new WaitForSeconds(shootCooldown);
        canShoot = true;
    }

    IEnumerator DisplayUI()
    {
        yield return new WaitForSeconds(1.5f);
        startUI[0].SetActive(true);
        startUI[1].SetActive(true);
    }

    IEnumerator DelayedStart()
    {
        gameStarted = false;

        if (fadeCanvas != null)
        {
            fadeCanvas.alpha = 1f;
            fadeCanvas.blocksRaycasts = true;

            float fadeTime = introDelay;
            while (fadeCanvas.alpha > 0)
            {
                fadeCanvas.alpha -= Time.deltaTime / fadeTime;
                yield return null;
            }

            fadeCanvas.blocksRaycasts = false;
            fadeCanvas.alpha = 0f;
        }
        else
        {
            yield return new WaitForSeconds(introDelay);
        }

        gameStarted = true;
    }


}
