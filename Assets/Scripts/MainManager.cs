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

    public Text ScoreText;
    public Text HighScoreText;
    public GameObject GameOverText;
    
    private bool m_Started = false;
    private int m_Points;
    
    private bool m_GameOver = false;

    
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

        // We Load the Highscore in Start(), as it is the First thing to get called each Session and Scene Reload.
        // And then we simply add the Highscore value. Below we check for New Highscore, and save it there, and then Load the Highscore again. 
        GameManager.Instance.LoadHighScore();
        HighScoreText.text = "Best Score : " + GameManager.Instance.maxScorePlayerName + " : " + GameManager.Instance.HighScore;    //
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
            // We check if the current score value is Greater than Current Highscore. 
            // If it is, then we add that new score value to our Highscore. 
            // And we also Save our HighScore here, bcoz we this is the New HighScore we got. (We only want to Save a Highscore when it exceeds its previous one)
            if(m_Points > GameManager.Instance.HighScore)
            {
                GameManager.Instance.SaveHighScore();
                HighScoreText.text = "Best Score : " + GameManager.Instance.maxScorePlayerName + " : " + GameManager.Instance.HighScore;
            } //

            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }

    void AddPoint(int point)
    {
        m_Points += point;
        ScoreText.text = $"Score : {m_Points}";
        
        // This line adds score to our GameManager Class
        GameManager.Instance.score = m_Points;  //
    }

    public void GameOver()
    {
        m_GameOver = true;
        GameOverText.SetActive(true);
    }

    // Function to go back to Main Menu Scene
    public void BackToMenu()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1); //
    }
}
