using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
    using UnityEditor;
#endif

public class UIController : MonoBehaviour
{
    public string userName;
    public TextMeshProUGUI highScoreText;
    //public TextMeshProUGUI highScoreList;
    
    private TMP_InputField inputField;

    // Start is called before the first frame update
    void Start()
    {
        inputField = GameObject.Find("Player Name").GetComponent<TMP_InputField>();  

        if(string.IsNullOrEmpty(GameManager.Instance.maxScorePlayerName))
            highScoreText.gameObject.SetActive(false);

        highScoreText.text = "Best Score : " + GameManager.Instance.maxScorePlayerName + " : " + GameManager.Instance.HighScore;
        //Debug.Log(inputField);

        //highScoreList.text = "HighScore List: " + "/n " + GameManager.Instance.jsonAll;
    }

    // Update is called once per frame
    void Update()
    {
        userName = inputField.text;
        GameManager.Instance.playerName = userName;

        if(Input.GetKeyDown(KeyCode.Return))
        {
            StartGame();
        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Quit()
    {
        #if UNITY_EDITOR
            EditorApplication.ExitPlaymode();
        #else
            Application.Quit();
        #endif
    }

}
