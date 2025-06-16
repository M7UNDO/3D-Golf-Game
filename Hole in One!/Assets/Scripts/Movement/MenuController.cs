using UnityEngine;
using TMPro;
using System.Collections;

public class MenuController : MonoBehaviour
{
    public GameObject endPanel;
    public TextMeshProUGUI[] countText;
    public int winAmount;
    public GameObject[] HUD;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void CloseHUD()
    {
        foreach (GameObject hud in HUD)
        {
            hud.SetActive(false);
        }
    }

    public void LoseGame()
    {
        CloseHUD();
        StartCoroutine(EndGame());
        endPanel.SetActive(true);
        endPanel.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "You Lose!";

    }

    public void WinGame()
    {
        CloseHUD();
        StartCoroutine(EndGame());
        endPanel.SetActive(true);
        endPanel.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Level Complete!";
    }

    public void AddCountText(int playerindex, int count)
    {
        countText[playerindex].text = count.ToString();
    }

    private IEnumerator EndGame()
    {
        yield return new WaitForSeconds(1f);
        Time.timeScale = 0f;
    }


}
