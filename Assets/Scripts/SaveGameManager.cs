using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderKeywordFilter;
using UnityEngine;

public class SaveGameManager : MonoBehaviour
{
    // Start is called before the first frame update
    const string HighScore = "Score";
    const string FastTime = "Time";
    public GameConnector gameMangaer = null;
    public UIManager ui;
    private bool saving;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameConnector.currentGameState == GameConnector.GameState.End && saving == false)
        {
            saveTime();
            saveScore();
            gameMangaer.UIManager.LoadMainMenu();
            saving = false;
            gameMangaer = null;
        }
    }
    private void saveScore()
    {
        saving = true;
        if ((int)gameMangaer.PacStudentController.getScore() > PlayerPrefs.GetInt(HighScore))
        {
            PlayerPrefs.SetInt(HighScore, (int)gameMangaer.PacStudentController.getScore());
        }
        Debug.Log("Score: " + PlayerPrefs.GetInt(HighScore));
    }
    private void saveTime()
    {
        if ((int)gameMangaer.PacStudentController.getScore() == PlayerPrefs.GetInt(HighScore))
        {
            Debug.Log("PassA");
            if (gameMangaer.UIManager.getTime() < PlayerPrefs.GetFloat(FastTime) || PlayerPrefs.GetFloat(FastTime) == 0f)
            {
                Debug.Log("PassB");
                PlayerPrefs.SetFloat(FastTime, gameMangaer.UIManager.getTime());
            }
        }
        if ((int)gameMangaer.PacStudentController.getScore() > PlayerPrefs.GetInt(HighScore))
        {
            Debug.Log("PassC");
            PlayerPrefs.SetFloat(FastTime, gameMangaer.UIManager.getTime());
        }
        }
    public void LoadScores()
    {
        if (PlayerPrefs.HasKey(HighScore))
        {
            Debug.Log("Score: " + HighScore);
            ui.setScore(PlayerPrefs.GetInt(HighScore));
        }
        if (PlayerPrefs.HasKey(FastTime))
        {
            Debug.Log("A" + PlayerPrefs.GetFloat(FastTime));
            ui.setTime(PlayerPrefs.GetFloat(FastTime));
        }
    }
    public void setConnector()
    {
        gameMangaer = GameObject.FindGameObjectWithTag("Connector").GetComponent<GameConnector>();
    }
}
