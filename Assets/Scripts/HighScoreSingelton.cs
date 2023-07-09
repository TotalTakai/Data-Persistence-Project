using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class HighScoreSingelton : MonoBehaviour
{
    public static HighScoreSingelton Instance;
    public static int highScore = 0;
    public static string playerName;


    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        LoadHighScoreInfo();
    }

    [System.Serializable]
    class SaveHighScore
    {
        public int highScore;
        public string playerName;
    }

    public static void SaveHighScoreInfo()
    {
        SaveHighScore data = new SaveHighScore();
        data.highScore = highScore;
        data.playerName = playerName;

        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void LoadHighScoreInfo()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveHighScore data = JsonUtility.FromJson<SaveHighScore>(json);

            highScore = data.highScore;
            playerName = data.playerName;
        }
    }
}
