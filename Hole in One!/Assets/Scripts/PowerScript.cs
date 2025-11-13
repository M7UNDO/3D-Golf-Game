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
    private bool charging;
    private bool goingUp = true;
    public float powerValue;

    [Header("Colors")]
    public Color lowColor = Color.yellow;
    public Color midColor = Color.green;
    public Color highColor = Color.red;

    private PlayerControls controls;

    private void Awake()
    {
        controls = new PlayerControls();

        controls.Player.Charge.performed += ctx => StartCharging();
        controls.Player.Charge.canceled += ctx => ReleasePower();
    }

    private void OnEnable() => controls.Enable();

    private void OnDisable() => controls.Disable();

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
        print(powerValue);
    }

    public float GetPowerValue() => powerValue;
}
