using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManagerGame : MonoBehaviour
{
    public GameObject save;
    private PacStudentController playerController;
    public Text scoreTxt;
    public Text GhostTimer;
    public GameObject[] lifes;
    public Text startTxt;
    private float countdown = 4;
    public GameConnector gameConnector;
    public Text timerTxt;
    public Text gameOverTxt;
    private float gameTime;
    private float remainTime = -1;
    // Start is called before the first frame update
    void Start()
    {
        gameConnector = GameObject.FindGameObjectWithTag("Connector").GetComponent<GameConnector>();
        scoreTxt = GameObject.FindGameObjectWithTag("ScoreTxt").GetComponent<Text>();
        GhostTimer.enabled = false;
        gameOverTxt.enabled = false;
        save = GameObject.FindGameObjectWithTag("Save");
        save.GetComponent<SaveGameManager>().setConnector();
        gameConnector.GetComponent<GameConnector>().setupScene();
    }

    // Update is called once per frame
    void Update()
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
                scoreTxt.text = ("Score: " + gameConnector.PacStudentController.getScore().ToString());
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
    public void LoadMainMenu()
    {
        Destroy(save);
        gameConnector.MenuState();
        SceneManager.LoadSceneAsync(0);
    }
    public void startGhostTimer()
    {
        GhostTimer.enabled = true;
        if (remainTime != -1)
        {
            StartCoroutine(scaredCount());
        }
        else
        {
            StopAllCoroutines();
            StartCoroutine(scaredCount());

        }
    }
    IEnumerator scaredCount()
    {
        remainTime = 10;
        while (remainTime >= 0)
        {
            GhostTimer.text = ("Scared Time: " + (remainTime).ToString());
            yield return new WaitForSeconds(1);
            remainTime--;
        }
        GhostTimer.enabled = false;
        remainTime = -1;
        yield return null;
    }
    public void loseLife()
    {
        Debug.Log(lifes.Length);
        Destroy(lifes[lifes.Length - 1]);
        lifes = GameObject.FindGameObjectsWithTag("Life");
    }
    public void setScore(int Score)
    {
        scoreTxt = GameObject.FindGameObjectWithTag("ScoreTxt").GetComponent<Text>();
        scoreTxt.text = ("Score: " + Score.ToString());
    }
    public void showGameOver()
    {
        StartCoroutine(gameOver());
    }

    IEnumerator gameOver()
    {
        int overTime = 10;
        while (overTime >= 0)
        {
            gameOverTxt.enabled = true;
            yield return new WaitForSeconds(1);
            overTime--;
        }
        gameOverTxt.enabled = false;
        gameConnector.returnToMenu();
        yield return null;
    }
}
