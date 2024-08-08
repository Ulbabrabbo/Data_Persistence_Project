using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{

    public static MenuManager Instance;

    private int finalScore;
    public string playerName;
    public int highScore;

    public TextMeshProUGUI playerHighScoreText;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        if (LoadHighScore())
        {
            playerHighScoreText.text = playerName + "High Score: " + highScore.ToString();
        }
    }

    [System.Serializable]
    class SaveData
    {
        public string playerName;
        public int highScore;
    }

    public void SaveHighScore()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            highScore = data.highScore;

            if (finalScore > highScore)
            {
                SaveData data_to_save = new SaveData();
                data_to_save.playerName = playerName;
                data_to_save.highScore = finalScore;
                string new_json = JsonUtility.ToJson(data_to_save);
                File.WriteAllText(Application.persistentDataPath + "/savefile.json", new_json);
            }
        } 
        else
        {
            SaveData data_to_save = new SaveData();
            data_to_save.playerName = playerName;
            data_to_save.highScore = finalScore;
            string new_json = JsonUtility.ToJson(data_to_save);
            File.WriteAllText(Application.persistentDataPath + "/savefile.json", new_json);
        }  
    }

    public bool LoadHighScore()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            playerName = data.playerName;
            highScore = data.highScore;
            return true;
        }
        else return false;
    }
}
