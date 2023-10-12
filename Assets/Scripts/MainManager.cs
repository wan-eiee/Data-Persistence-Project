using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MainManager : MonoBehaviour
{
    public Brick BrickPrefab;
    public int LineCount = 6;
    public Rigidbody Ball;

    public Text ScoreText;
    public Text bestScoreText;
    public GameObject backToMenuButton;
    public GameObject quitButton;
    public GameObject GameOverText;
    public GameObject askYourNameText;
    public GameObject inputField;
    private TMP_InputField iField;
    
    private bool m_Started = false;
    private int m_Points;
    
    public bool m_GameOver = false;

    private bool onInput = false;

    
    // Start is called before the first frame update
    void Start()
    {
        const float step = 0.6f;
        int perLine = Mathf.FloorToInt(4.0f / step);
        
        int[] pointCountArray = new [] {1,1,2,2,5,5};
        for (int i = 0; i < LineCount; ++i)
        {
            for (int x = 0; x < perLine; ++x)
            {
                Vector3 position = new Vector3(-1.5f + step * x, 2.5f + i * 0.3f, 0);
                var brick = Instantiate(BrickPrefab, position, Quaternion.identity);
                brick.PointValue = pointCountArray[i];
                brick.onDestroyed.AddListener(AddPoint);
            }
        }
        iField = inputField.GetComponent<TMP_InputField>();
    }

    private void Update()
    {
        if (!m_Started)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                m_Started = true;
                float randomDirection = Random.Range(-1.0f, 1.0f);
                Vector3 forceDir = new Vector3(randomDirection, 1, 0);
                forceDir.Normalize();

                Ball.transform.SetParent(null);
                Ball.AddForce(forceDir * 2.0f, ForceMode.VelocityChange);
            }
        }
        else if (m_GameOver && !onInput)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
        DisplayBestScorePlayer();
    }

    void AddPoint(int point)
    {
        m_Points += point;
        ScoreText.text = $"Score : {m_Points}";
    }

    public void GameOver()
    {
        m_GameOver = true;
        
        if(IsNewRecord()){
            askYourNameText.SetActive(true);
            inputField.SetActive(true);
            GetBestScore();
            onInput = true;
        }
        else {
            GameOverText.SetActive(true);
            ActivateMenuButtons();
        }
    }

    private bool IsNewRecord(){
        if(GameManager.instance.score < m_Points){
            return true;
        }
        else{
            return false;
        }
    }

    private void GetBestScore(){
        GameManager.instance.score = m_Points;
    }


    //call inputfield end edit
    public void GetNameFromInputField(){
        string inputValue = iField.text;
        GameManager.instance.playerName = inputValue;
        InitInputField();
    }

    private void InitInputField(){
        iField.text = "";
        inputField.SetActive(false);
        askYourNameText.SetActive(false);
        onInput = false;
        GameOverText.SetActive(true);
        ActivateMenuButtons();
    }

    private void ActivateMenuButtons(){
        backToMenuButton.SetActive(true);
        quitButton.SetActive(true);
    }

    private void DisplayBestScorePlayer(){
        if(GameManager.instance.score == 0){
            bestScoreText.text = "Best Score: 0";
            return ; 
        }
        else{
            bestScoreText.text = GameManager.instance.playerName + " has best score: " + GameManager.instance.score;
        }
    }
}
