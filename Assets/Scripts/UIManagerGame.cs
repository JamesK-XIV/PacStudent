using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManagerGame : MonoBehaviour
{
    public GameObject save;
    public Text scoreTxt;
    public Text GhostTimer;
    public GameObject[] lifes;
    public Text startTxt;
    private float countdown = 4;
    public GameConnector gameConnector;
    private GameObject ConnectorGameObject;
    public Text timerTxt;
    public Text gameOverTxt;
    private float gameTime;
    private float remainTime = -1;
    // Start is called before the first frame update
    void Start()
    {
        ConnectorGameObject = GameObject.FindGameObjectWithTag("Connector");
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
        Destroy(ConnectorGameObject);
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
        while (gameConnector.GhostManager.getScaredTime() >= 0)
        {
            GhostTimer.text = ("Scared Time: " + (10-(int)gameConnector.GhostManager.getScaredTime()).ToString());
            yield return null;
        }
        GhostTimer.enabled = false;
        remainTime = -1;
        yield return null;
    }
    public void loseLife(int life)
    {
        Destroy(lifes[life]);
    }
    public void setScore(int Score)
    {
        scoreTxt = GameObject.FindGameObjectWithTag("ScoreTxt").GetComponent<Text>();
        scoreTxt.text = ("Score: " + Score.ToString());
    }
    public float getTime()
    {
        return gameTime;
    }
    public void showGameOver()
    {
        StartCoroutine(gameOver());
    }

    IEnumerator gameOver()
    {
        timerTxt = null;
        int overTime = 3;
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
