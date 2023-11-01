using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private PacStudentController playerController;
    private Text scoreTxt;
    private Text GhostTimer;
    private GameObject[] lifes;
    private Text startTxt;
    private float countdown = 4;
    private GameConnector gameConnector;
    private Text timerTxt;
    private float gameTime;
    private float remainTime = -1;
    // Start is called before the first frame update
    void Start()
    {
        
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
    public void LoadFirstLevel()
    {
        DontDestroyOnLoad(gameObject);
        SceneManager.LoadSceneAsync(1);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    public void LoadImprovementLevel()
    {
        DontDestroyOnLoad (gameObject);
        SceneManager.LoadSceneAsync(2);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    public void LoadMainMenu()
    {
        DontDestroyOnLoad(gameObject);
        SceneManager.LoadSceneAsync(0);
    }
    public void OnSceneLoaded(Scene scene, LoadSceneMode mode) 
    {
        if (scene.buildIndex == 1)
        {
            Button button = GameObject.FindGameObjectWithTag("ExitButton").GetComponent<Button>();
            button.onClick.AddListener(LoadMainMenu);
            playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PacStudentController>();
            scoreTxt = GameObject.FindGameObjectWithTag("ScoreTxt").GetComponent<Text>();
            GhostTimer = GameObject.FindGameObjectWithTag("GhostTimer").GetComponent<Text>();
            GhostTimer.enabled = false;
            lifes = GameObject.FindGameObjectsWithTag("Life");
            startTxt = GameObject.FindGameObjectWithTag("Start").GetComponent<Text>();
            gameConnector = GameObject.FindGameObjectWithTag("Connector").GetComponent<GameConnector>();
            timerTxt = GameObject.FindGameObjectWithTag("Timer").GetComponent <Text>();
        }
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
        Destroy(lifes[lifes.Length - 1]);
        lifes = GameObject.FindGameObjectsWithTag("Life");
    }
}
