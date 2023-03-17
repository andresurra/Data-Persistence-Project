using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static DataManager Instance;

    private long bestScore;
    private string bestPlayer;
    private string playerName;
    private string persistentPath = "";

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        LoadData();
    }

    //Gets score and set the score and player name if score better than the score on JSON file - Saves
    public void SetLastScore(int score)
    {
        if (score > Instance.bestScore)
        {
            bestScore = score;
            bestPlayer = playerName;
            SaveData();
        }
    }

    public long GetBestScore() => Instance.bestScore;

    public string GetBestPlayer() => Instance.bestPlayer;

    public void SetPlayerName(string name)
    {
        Instance.playerName = name;
    }

    //Save between Scenes
    [Serializable]
    class TheBest
    {
        public long score;
        public string name;
    }

    public void SaveData()
    {
        //to know where is saving the Data
        persistentPath = Application.persistentDataPath + Path.AltDirectorySeparatorChar + "userData.json";
        string savePath = persistentPath;
        Debug.Log("Saving data at " + savePath);

        //Saving the data
        var userData = new TheBest() { score = Instance.bestScore, name = Instance.bestPlayer };
        var jsonData = JsonUtility.ToJson(userData);
        File.WriteAllText(Path.Combine(Application.persistentDataPath, "userData.json"), jsonData);
    }

    public void LoadData()
    {
        var userDataFile = Path.Combine(Application.persistentDataPath, "userData.json");
        if (File.Exists(userDataFile))
        {
            var jsonData = File.ReadAllText(userDataFile);
            var userData = JsonUtility.FromJson<TheBest>(jsonData);
            bestScore = userData.score;
            bestPlayer = userData.name;
        }
    }
}
