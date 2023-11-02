using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderKeywordFilter;
using UnityEngine;

public class SaveGameManager : MonoBehaviour
{
    // Start is called before the first frame update
    const string Score = "";
    const string Time = "";
    public GameConnector gameMangaer = null;
    public UIManager ui;
    private bool saving;
    void Start()
    {
        loadSpeed();

    }

    // Update is called once per frame
    void Update()
    {
        if (GameConnector.currentGameState == GameConnector.GameState.End && saving == false)
        {
            StartCoroutine(saveScore());
        }
    }
    IEnumerator saveScore()
    {
        saving = true;
        Debug.Log(saving);
        PlayerPrefs.SetInt(Score, (int)gameMangaer.PacStudentController.getScore());
        if ((int)gameMangaer.PacStudentController.getScore() > PlayerPrefs.GetInt(Score))
        {
            PlayerPrefs.SetInt(Score, (int)gameMangaer.PacStudentController.getScore());
            PlayerPrefs.Save();
        }
        gameMangaer.UIManager.LoadMainMenu();
        saving = false;
        gameMangaer = null;
        yield return null;
    }
    public void loadSpeed()
    {
        if (PlayerPrefs.HasKey(Score))
        {
            ui.setScore(PlayerPrefs.GetInt(Score));
        }
    }
    public void setConnector()
    {
        gameMangaer = GameObject.FindGameObjectWithTag("Connector").GetComponent<GameConnector>();
    }
}
