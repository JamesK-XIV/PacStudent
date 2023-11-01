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
            if (countdown <= 1 )
            {
                startTxt.text = ("GO!");
            }
            if (countdown <= 0 )
            {
                startTxt.enabled = false;
                gameConnector.StartGame();
            }
        }
        else if (scoreTxt != null)
        {
            scoreTxt.text = ("Score: " + playerController.getScore().ToString());
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
        }
    }
    public void startGhostTimer()
    {
        GhostTimer.enabled = true;
        StartCoroutine(countDown());
    }
    IEnumerator countDown()
    {
        float remainTime = 10;
        while (remainTime >= 0)
        {
            GhostTimer.text = ("Scared Time: " + (remainTime).ToString());
            yield return new WaitForSeconds(1);
            remainTime--;
        }
        GhostTimer.enabled = false;
        yield return null;
    }
    public void loseLife()
    {
        Destroy(lifes[lifes.Length - 1]);
        lifes = GameObject.FindGameObjectsWithTag("Life");
    }
}
