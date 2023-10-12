using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int score;
    public string playerName;
    
    [System.Serializable]
    public class SaveData
    {
        public int score;
        public string name;
    }

    private void Awake(){
        if(instance == null){
            instance = this;
            DontDestroyOnLoad(gameObject);
            LoadSaveData();
        }
        else{
            Destroy(gameObject);
        }
    }

    private void onDestroyed(){
        instance = null;
    }

    public void SaveRecords(){
        SaveData data = new SaveData();
        data.score = score;
        data.name = playerName;
        string json = JsonUtility.ToJson(data);
        string path = Application.persistentDataPath + "/savefile.json";
        File.WriteAllText(path, json);
    }

    public void LoadSaveData(){
        string path = Application.persistentDataPath + "/savefile.json";
        if(File.Exists(path)){
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);
            score = data.score;
            playerName = data.name;
        }
        else{
            Debug.Log("no savefile");
        }
    }
}
