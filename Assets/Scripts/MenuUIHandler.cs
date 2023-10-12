using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

#if UNITY_EDITOR
using UnityEditor;
#endif

using TMPro;

public class MenuUIHandler : MonoBehaviour
{
    public TMP_InputField inputField;
    
    public void StartButtonClicked(){
        SceneManager.LoadScene(1);
    }

    public void Quit(){
        #if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
        #else
        Application.Quit();
        #endif
    }

    public void GetNameFromInputField(){
        string inputValue = inputField.text;
        GameManager.instance.Name = inputValue;
        InitInputField();
    }

    private void InitInputField(){
        inputField.text = "";
    }
}
