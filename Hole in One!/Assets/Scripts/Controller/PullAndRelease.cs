using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using Cinemachine;

public class PullAndRelease : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Rigidbody rb;
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private TrailRenderer trailRenderer;
    [SerializeField] private CinemachineVirtualCamera vCam;
    [SerializeField] private CinemachineImpulseSource impulseSource;

    [Header("Input")]
    private InputActionMap player;
    private InputAction look;
    private InputAction charge;

    [Header("Aiming")]
    public float rotationSensitivity = 3f;
    public float maxPullDistance = 3f;

    [Header("Shot Power")]
    public float minShotPower = 5f;
    public float maxShotPower = 25f;
    public float cancelThreshold = 0.1f;

    [Header("Line Power Feedback")]
    public Gradient powerGradient;

    [Header("Camera Zoom Feedback")]
    public float maxZoomOutFOV = 55f;
    public float zoomLerpSpeed = 8f;

    [Header("Shot Count")]
    public float NumberOfShots;
    public TextMeshProUGUI shotsTxt;

    private float currentRotationY;
    private Vector2 pullStartScreenPos;
    private float currentPullDistance;
    private bool isCharging;
    private float baseFOV;

    private void Awake()
    {
        var inputAsset = rb.gameObject.GetComponent<PlayerInput>().actions;
        player = inputAsset.FindActionMap("Player");
    }

    private void Start()
    {
        baseFOV = vCam.m_Lens.FieldOfView;
        lineRenderer.enabled = false;
        lineRenderer.colorGradient = powerGradient;
    }

    private void OnEnable()
    {
        look = player.FindAction("LookAround");
        charge = player.FindAction("Charge");
        player.Enable();
    }

    private void Update()
    {
        if (PauseScript.IsGamePaused || Time.timeScale == 0f)
            return;

        HandlePullAim();
        HandleCameraZoomReset();
    }

    private void HandlePullAim()
    {
        Vector2 lookInput = look.ReadValue<Vector2>();

        if (charge.WasPressedThisFrame())
        {
            isCharging = true;
            pullStartScreenPos = Mouse.current.position.ReadValue();
            currentPullDistance = 0f;
            lineRenderer.enabled = true;
            trailRenderer.enabled = false;
            
        }

        if (charge.IsPressed() && isCharging)
        {
            // Rotate only on ground (Y axis)
            currentRotationY += lookInput.x * rotationSensitivity;
            transform.rotation = Quaternion.Euler(0f, currentRotationY, 0f);

            // Calculate pull distance (screen-space drag)
            Vector2 currentMousePos = Mouse.current.position.ReadValue();
            float dragAmount = (pullStartScreenPos.y - currentMousePos.y) * 0.01f;

            currentPullDistance = Mathf.Clamp(dragAmount, 0f, maxPullDistance);

            DrawPullLine();
            ApplyCameraTension();
        }

        if (charge.WasReleasedThisFrame() && isCharging)
        {
            Shoot();
            trailRenderer.enabled = true;
        }
    }

    private void DrawPullLine()
    {
        Vector3 ballPos = rb.position + Vector3.up * 0.02f;
        Vector3 pullDir = -transform.forward;

        float power01 = currentPullDistance / maxPullDistance;
        Color powerColor = powerGradient.Evaluate(power01);

        lineRenderer.positionCount = 2;
        lineRenderer.SetPosition(0, ballPos);
        lineRenderer.SetPosition(1, ballPos + pullDir * currentPullDistance);
        lineRenderer.startColor = powerColor;
        lineRenderer.endColor = powerColor;
    }

    private void ApplyCameraTension()
    {
        float power01 = currentPullDistance / maxPullDistance;

        // Camera shake
        CameraShakeManager.instance.shakeForce = Mathf.Lerp(0.05f, 0.35f, power01);
        CameraShakeManager.instance.CameraShake(impulseSource);

        // Camera zoom out
        float targetFOV = Mathf.Lerp(baseFOV, maxZoomOutFOV, power01);
        vCam.m_Lens.FieldOfView = Mathf.Lerp(
            vCam.m_Lens.FieldOfView,
            targetFOV,
            Time.deltaTime * zoomLerpSpeed
        );
    }

    private void HandleCameraZoomReset()
    {
        if (!isCharging)
        {
            vCam.m_Lens.FieldOfView = Mathf.Lerp(
                vCam.m_Lens.FieldOfView,
                baseFOV,
                Time.deltaTime * zoomLerpSpeed
            );
        }
    }

    private void Shoot()
    {
        isCharging = false;
        lineRenderer.enabled = false;

        // Cancel shot if pull too small
        if (currentPullDistance <= cancelThreshold)
        {
            currentPullDistance = 0f;
            return;
        }

        float power01 = currentPullDistance / maxPullDistance;
        float shotPower = Mathf.Lerp(minShotPower, maxShotPower, power01);

        rb.AddForce(transform.forward * shotPower, ForceMode.Impulse);

        currentPullDistance = 0f;
        TrackShots();
    }

    private void TrackShots()
    {
        NumberOfShots++;
        if (shotsTxt != null)
            shotsTxt.text = NumberOfShots.ToString();
    }
}
