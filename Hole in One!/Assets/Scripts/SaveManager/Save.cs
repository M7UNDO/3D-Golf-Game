using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using TMPro;
using UnityEngine;

public class Save: MonoBehaviour
{
    public static Save instance { get; private set; }
    // Start is called before the first frame update

    public int currentBall;
    public int Coins;
    public bool[] ballsUnlocked = new bool[10] { true, false, false, false, false, false, false, false, false, false };
    public bool[] LevelsUnlocked = new bool[4] { true, false, false, false };

    private void Awake()
    {
        if (instance != null && instance != this)
            Destroy(gameObject);
        else
            instance = this;

        DontDestroyOnLoad(gameObject);
        Load();
    }

    public void ResetSave()
    {
        if (File.Exists(Application.persistentDataPath + "/playerInfo.dat"))
            File.Delete(Application.persistentDataPath + "/playerInfo.dat");

        currentBall = 0;
        Coins = 0;
        ballsUnlocked = new bool[10] { true, false, false, false, false, false, false, false, false, false };
        LevelsUnlocked = new bool[4] { true, false, false, false };

        SaveData();
    }

    public void Load()
    {
        if(File.Exists(Application.persistentDataPath + "/playerInfo.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/playerInfo.dat",FileMode.Open);
            PlayerData_Storage data = (PlayerData_Storage)bf.Deserialize(file);

            currentBall = data.currentBall;
            Coins = data.Coins;
            ballsUnlocked = data.ballsUnlocked;
            LevelsUnlocked = data.LevelsUnlocked;
            file.Close();
            
            if(data.ballsUnlocked == null)
            {
                ballsUnlocked = new bool[10] { true, false, false, false, false, false, false, false, false, false };

            }

            if (data.LevelsUnlocked == null)
            {
                LevelsUnlocked = new bool[4] { true, false, false, false };
            }
           
            
        }
        
    }

    public void SaveData()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/playerInfo.dat");
        PlayerData_Storage data = new PlayerData_Storage();

        data.currentBall = currentBall;
        data.Coins  = Coins;
        data.ballsUnlocked = ballsUnlocked;
        data.LevelsUnlocked = LevelsUnlocked;
        bf.Serialize(file, data);
        file.Close();
        //print("Save found" + currentBall);


    }

    [Serializable]
    class PlayerData_Storage
    {
        public int currentBall;
        public int Coins;
        public bool[] ballsUnlocked = new bool[10] { true, false, false, false, false, false, false, false, false, false };
        public bool[] LevelsUnlocked = new bool[4] { true, false, false, false };


    }
}
