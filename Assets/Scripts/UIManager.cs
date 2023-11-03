using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject save;
    public Text scoreTxt;
    private Text startTxt;
    public GameConnector gameConnector;
    private Text timerTxt;
    private float gameTime;
    // Start is called before the first frame update
    void Start()
    {
        save.GetComponent<SaveGameManager>().LoadScores();
        scoreTxt = GameObject.FindGameObjectWithTag("ScoreTxt").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void LoadFirstLevel()
    {
        DontDestroyOnLoad(save);
        DontDestroyOnLoad(gameConnector);
        SceneManager.LoadSceneAsync(1);
    }
    public void LoadImprovementLevel()
    {
        DontDestroyOnLoad (gameObject);
        SceneManager.LoadSceneAsync(2);
    }
    public void setScore(int Score)
    {
        Debug.Log(Score);
        scoreTxt.text = ("Score: " + Score.ToString());
    }
    public void setTime(float gameTime)
    {
        string displayTime = "";
        timerTxt = GameObject.FindGameObjectWithTag("Timer").GetComponent<Text>();
        int minuteTime = ((int)gameTime / 60);
        if (minuteTime < 10)
        {
            displayTime = "0" + minuteTime.ToString() + ":";
        }
        else
        {
            displayTime = minuteTime.ToString() + ":";
        }
        int secondTime = ((int)gameTime) - 60 * minuteTime;
        if (secondTime < 10)
        {
            displayTime += "0" + secondTime.ToString() + ":";
        }
        else
        {
            displayTime += secondTime.ToString() + ":";
        }
        int millisecondTime = (int)((gameTime - (int)gameTime) * 100);
        if (millisecondTime < 10)
        {
            displayTime += "0" + millisecondTime.ToString();
        }
        else
        {
            displayTime += millisecondTime.ToString();
        }

        timerTxt.text = ("Timer: " + displayTime);
    }
}
