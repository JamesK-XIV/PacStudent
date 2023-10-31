using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
        }
    }
}
