using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public TextMeshProUGUI bestScoreText;
    public TextMeshProUGUI playerNameInput;

    private void Start()
    {
        Debug.Log("MenuManager.Start");
        bestScoreText.text = $"Best score: {DataManager.Instance.GetBestScore()} by {DataManager.Instance.GetBestPlayer()}";
    }

    public void StartButton_Click()
    {
        DataManager.Instance.SetPlayerName(playerNameInput.text);
        SceneManager.LoadScene("main");
    }

    public void QuitButton_Click()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }

}
