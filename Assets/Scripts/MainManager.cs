using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainManager : MonoBehaviour
{
    public Brick BrickPrefab;
    public int LineCount = 6;
    public Rigidbody Ball;

    [SerializeField] Text ScoreText;
    [SerializeField] Text BestScoreText;
    [SerializeField] GameObject NewHighScoreObject;
    [SerializeField] GameObject restartObject;
    [SerializeField] InputField playerNameInput;
    
    private bool m_Started = false;
    private int m_Points;
    
    private bool m_GameOver = false;

    
    // Start is called before the first frame update
    void Start()
    {
        ShowHighScore();
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
        else if (m_GameOver)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                HighScoreSingelton.SaveHighScoreInfo();
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }

    void AddPoint(int point)
    {
        m_Points += point;
        ScoreText.text = $"Score : {m_Points}";
    }

    public void GameOver()
    {
        m_GameOver = true;
        UpdateHighScore();
    }

    private void UpdateHighScore()
    {
        if (m_Points > HighScoreSingelton.highScore)
        {
            HighScoreSingelton.highScore = m_Points;
            NewHighScoreObject.SetActive(true);
        }
        else restartObject.SetActive(true);
    }

    private void ShowHighScore()
    {
        if (HighScoreSingelton.highScore > 0)
        {
            BestScoreText.text = "High Score: " + HighScoreSingelton.highScore + " By: " + HighScoreSingelton.playerName;
        }
        else BestScoreText.text = "";
    }

    public void UpdateName(string playerName)
    {
        HighScoreSingelton.playerName = playerName;
    }
}
