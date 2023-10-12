using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public TextMeshProUGUI bestScoreText;

    private int score;
    public int bestScore{
        get{return score;}
        set{score = value;}
    }

    private string playerName;
    public string Name{
        get{return playerName;}
        set{playerName = value;}
    }
    

    private void Awake(){
        if(instance != null){
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else{
            Destroy(gameObject);
        }

        LoadBestScore();
    }

    private void onDestroyed(){
        instance = null;
    }

    public void SaveBestScore(){
        string json = JsonUtility.ToJson(instance);
        string path = Application.persistentDataPath + "/savefile.json";
        File.WriteAllText(path, json);
    }

    public void LoadBestScore(){
        string path = Application.persistentDataPath + "/savefile.json";
        string json = File.ReadAllText(path);
        instance = JsonUtility.FromJson<GameManager>(json);
        bestScoreText.text =  Name + "has Best Score: " + bestScore;
    }
}
