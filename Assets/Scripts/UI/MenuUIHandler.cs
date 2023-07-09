using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class MenuUIHandler : MonoBehaviour
{
    [SerializeField] Text highScoreText;
    [SerializeField] GameObject highScoreObject;
    // Start is called before the first frame update
    void Start()
    {
        UpdateHighScore();
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void Exit()
    {
#if (UNITY_EDITOR)
        EditorApplication.ExitPlaymode();
#else
        Application.Quit(); // original code to quit Unity player
#endif
    }

    private void UpdateHighScore()
    {
        if (HighScoreSingelton.highScore == 0)
        {
            highScoreObject.SetActive(false);
        }
        else
        {
            highScoreObject.SetActive(true);
            highScoreText.text = "High Score: " + HighScoreSingelton.highScore + " By: " + HighScoreSingelton.playerName;
        }
    }
}
