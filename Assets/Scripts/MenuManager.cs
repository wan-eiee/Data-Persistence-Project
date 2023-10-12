using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MenuManager : MonoBehaviour
{
    public TextMeshProUGUI bestScoreText;


    // Start is called before the first frame update
    void Start()
    {
        bestScoreText.text = ShowBestScore();
    }

    private string ShowBestScore(){
        if(GameManager.instance.score == 0){
            return "Best Score: " + GameManager.instance.score;
        }
        else{
            return GameManager.instance.playerName + " has Best Score: " + GameManager.instance.score;
        }
    }

    
}
