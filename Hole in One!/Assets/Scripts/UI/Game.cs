
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Game : MonoBehaviour
{
    #region SIngleton:Game

    public static Game Instance;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            //DontDestroyOnLoad(gameObject);
        }
        else
        {
            //Destroy(gameObject);
        }
    }

    #endregion

    [SerializeField] TextMeshProUGUI[] allCoinsUIText;

    //public int Coins;

    void Start()
    {
        UpdateAllCoinsUIText();

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            Save.instance.Coins += 100;
            Save.instance.SaveData();
            print("+100");
        }

        else if (Input.GetKeyDown(KeyCode.X))
        {
            Save.instance.Coins -= 100;
            Save.instance.SaveData();
            print("-100");
        }

        UpdateAllCoinsUIText();
    }

    public void UseCoins(int amount)
    {
        Save.instance.Coins -= amount;
    }

    public bool HasEnoughCoins(int amount)
    {
        return (Save.instance.Coins >= amount);
    }

    public void UpdateAllCoinsUIText()
    {
        for (int i = 0; i < allCoinsUIText.Length; i++)
        {
            allCoinsUIText[i].text = Save.instance.Coins.ToString();
        }
    }

}
