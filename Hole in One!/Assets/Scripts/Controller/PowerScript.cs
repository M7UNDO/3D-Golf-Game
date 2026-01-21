using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PowerScript : MonoBehaviour
{
    [SerializeField] PullAndRelease pullAndRelease;
    

    [Header("UI Reference")]
    public Image powerFill;
    public GameObject barContainer;

    [Header("Settings")]
    public float fillSpeed = 1f;
    public bool charging;
    private bool goingUp = true;
    public float powerValue;

    [Header("Colors")]
    public Color lowColor = Color.yellow;
    public Color midColor = Color.green;
    public Color highColor = Color.red;

    private PlayerControls controls;

    [Header("Input Actions")]
    private InputActionMap player;
    private InputActionAsset inputAsset;

    private void Awake()
    {
        inputAsset = GetComponent<PlayerInput>().actions;
        player = inputAsset.FindActionMap("Player");
    }

    private void CancelCharge()
    {
        charging = false;
        barContainer.SetActive(false);
    }

    private void OnEnable()
    {
        player.FindAction("Charge").performed += ctx => StartCharging();
        player.FindAction("Charge").canceled += ctx => ReleasePower();
        player.FindAction("CancelCharge").performed += ctx => CancelCharge();
        player.Enable();
    }

    //private void OnEnable() => controls.Enable();

    private void OnDisable() => player.Disable();

    private void Update()
    {

        if (!charging)
            return;

        if (goingUp)
        {
            powerFill.fillAmount += fillSpeed * Time.deltaTime;
            if (powerFill.fillAmount >= 1f)
            {
                powerFill.fillAmount = 1f;
                goingUp = false;
            }
        }
        else
        {
            powerFill.fillAmount -= fillSpeed * Time.deltaTime;
            if (powerFill.fillAmount <= 0f)
            {
                powerFill.fillAmount = 0f;
                goingUp = true;
            }
        }

        if (powerFill.fillAmount < 0.5f)
        {
            powerFill.color = Color.Lerp(lowColor, midColor, powerFill.fillAmount * 2f);
        }
        else
        {
            powerFill.color = Color.Lerp(midColor, highColor, (powerFill.fillAmount - 0.5f) * 2f);
        }
    }

    private void StartCharging()
    {
        barContainer.SetActive(true);
        charging = true;
    }

    private void ReleasePower()
    {
        if (!charging)
            return;

        charging = false;
        powerValue = powerFill.fillAmount;
        barContainer.SetActive(false);
        pullAndRelease.Shoot();
    }

    public float GetPowerValue() => powerValue;
}
