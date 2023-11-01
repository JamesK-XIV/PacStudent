using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private PacStudentController playerController;
    private Text scoreTxt;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (scoreTxt != null)
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
        }
    }
}
