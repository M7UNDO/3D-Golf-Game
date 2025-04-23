using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;

public class LevelUI : MonoBehaviour
{
    public bool toggle;
    public TextMeshProUGUI[] tutorialUI;
    public Image[] tutorialarrow;
    public GameObject objectivePanel;
    public GameObject[] UIElements;
    [SerializeField] private string[] tutorialstring;
    private PlayerControls playerInput;
    private System.Action<InputAction.CallbackContext> closeObjectiveAction;
    public PullAndRelease pullAndRelease;

    void Start()
    {
        tutorialUI[0].text = tutorialstring[0];
        tutorialUI[1].text = tutorialstring[1];
        tutorialUI[2].text = tutorialstring[2];


        StartCoroutine(DisplayUI());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable()
    {
        playerInput = new PlayerControls();
        playerInput.Player.Enable();

        closeObjectiveAction = ctx => CloseObjective();
        playerInput.Player.Objective.performed += closeObjectiveAction;
    }

    private void OnDisable()
    {
        playerInput.Player.Objective.performed -= closeObjectiveAction;
        playerInput.Player.Disable();
    }

    public void CloseObjective()
    {
        toggle = !toggle;

        if (toggle)
        {
            objectivePanel.SetActive(true);
        }
        else
        {
            objectivePanel.SetActive(false);
        }

        print("Pause toggled: " + toggle);
    }

    IEnumerator DisplayUI()
    {
        yield return new WaitForSeconds(1f);
        foreach(GameObject UI in UIElements)
        {
            UI.SetActive(true);
        }
    }
}
