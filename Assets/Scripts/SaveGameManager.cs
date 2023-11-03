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
    }
    private void saveTime()
    {
        if ((int)gameMangaer.PacStudentController.getScore() == PlayerPrefs.GetInt(HighScore))
        {
            if (gameMangaer.UIManager.getTime() < PlayerPrefs.GetFloat(FastTime) || PlayerPrefs.GetFloat(FastTime) == 0f)
            {
                PlayerPrefs.SetFloat(FastTime, gameMangaer.UIManager.getTime());
            }
        }
        if ((int)gameMangaer.PacStudentController.getScore() > PlayerPrefs.GetInt(HighScore))
        {
            PlayerPrefs.SetFloat(FastTime, gameMangaer.UIManager.getTime());
        }
        }
    public void LoadScores()
    {
        if (PlayerPrefs.HasKey(HighScore))
        {
            ui.setScore(PlayerPrefs.GetInt(HighScore));
        }
        if (PlayerPrefs.HasKey(FastTime))
        {
            ui.setTime(PlayerPrefs.GetFloat(FastTime));
        }
    }
    public void setConnector()
    {
        gameMangaer = GameObject.FindGameObjectWithTag("Connector").GetComponent<GameConnector>();
    }
}
