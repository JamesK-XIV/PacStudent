using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject save;
    private PacStudentController playerController;
    public Text scoreTxt;
    private Text GhostTimer;
    private GameObject[] lifes;
    private Text startTxt;
    private float countdown = 4;
    public GameConnector gameConnector;
    private Text timerTxt;
    private Text gameOverTxt;
    private float gameTime;
    private float remainTime = -1;
    // Start is called before the first frame update
    void Start()
    {
        scoreTxt = GameObject.FindGameObjectWithTag("ScoreTxt").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().name == "Scene1")
        {
        if (startTxt != null)
        {
            countdown -= Time.deltaTime;
            startTxt.text = ((int)countdown).ToString();
            if (countdown <= 1)
            {
                startTxt.text = ("GO!");
            }
            if (countdown <= 0)
            {
                startTxt.enabled = false;
                gameConnector.StartGame();
                startTxt = null;
            }
        }
        else
        {
            if (scoreTxt != null)
            {
                scoreTxt.text = ("Score: " + playerController.getScore().ToString());
            }
            if (timerTxt != null)
            {
                string displayTime = "";
                gameTime += Time.deltaTime;
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
    }
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
        scoreTxt = GameObject.FindGameObjectWithTag("ScoreTxt").GetComponent<Text>();
        scoreTxt.text = ("Score: " + Score.ToString());
    }
}
