using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public string playerName;
    public string maxScorePlayerName;
    public int score;
    public int HighScore;

    //public List<string> jsonAll = new List<string>();

    private void Awake() 
    {
        if(Instance != null)
        {
            Destroy(gameObject);
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        LoadHighScore();
    }

    private void Update() 
    {
   
    }

    [System.Serializable]
    public class SaveData
    {
        public int highScore = 0;
        public string userName;
    }

    public void SaveHighScore()
    {
        SaveData data = new SaveData();
        data.highScore = score;
        data.userName = playerName;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void LoadHighScore()
    {
        string path = Application.persistentDataPath + "/savefile.json";

        if(File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            // if(data.highScore > HighScore)
            // {
                HighScore = data.highScore;
                maxScorePlayerName = data.userName;
                Debug.Log("highScore : " + data.highScore);
            //}
        }
    }

    // public void LoadAllHighScores()
    // {
    //     string path = Application.persistentDataPath + "/savefile.json";

    //     if(File.Exists(path))
    //     {
    //         string json = File.ReadAllText(path);

    //         jsonAll.Add(json);

    //         foreach(string jsonFile in jsonAll)
    //         {
    //             SaveData data = JsonUtility.FromJson<SaveData>(jsonFile);

    //             HighScore = data.highScore;
    //             maxScorePlayerName = data.userName;                
    //         }
    //     }
    // }
}
