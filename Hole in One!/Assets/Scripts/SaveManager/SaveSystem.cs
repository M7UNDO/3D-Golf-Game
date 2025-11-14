using System.IO;
using UnityEngine;

public static class SaveSystem
{
    private static string saveFileName = "playerSave.json";
    private static string SavePath => Path.Combine(Application.persistentDataPath, saveFileName);

    public static void Save(SaveData data)
    {
        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(SavePath, json);
        Debug.Log("Saved JSON to: " + SavePath);
    }

    public static SaveData Load()
    {
        if (!File.Exists(SavePath))
        {
            Debug.Log("No save file found. Creating new SaveData.");
            return new SaveData();
        }

        string json = File.ReadAllText(SavePath);
        return JsonUtility.FromJson<SaveData>(json);
    }

    public static void DeleteSave()
    {
        if (File.Exists(SavePath))
            File.Delete(SavePath);
    }
}
