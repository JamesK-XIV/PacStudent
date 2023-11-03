using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameConnector : MonoBehaviour
{
    public SaveGameManager saveGameManager;
    public AudioManager AudioManager;
    public PacStudentController PacStudentController;
    public GhostManager GhostManager;
    public UIManagerGame UIManager;
    public enum GameState { Menu, Start, Wait, End }
    public static GameState currentGameState;
    // Start is called before the first frame update
    public static bool CurrentGameState
    {
        get { return currentGameState == GameState.Start; }
    }
    void Start()
    {
        currentGameState = GameState.Menu;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.FindGameObjectWithTag("UIManager") != null)
        {
            UIManager = GameObject.FindGameObjectWithTag("UIManager").GetComponent<UIManagerGame>();
        }
    }
    public void EndGame()
    {
        if (GameConnector.currentGameState == GameState.Start)
        {
            UIManager.showGameOver();
            GameConnector.currentGameState = GameState.Wait;
        }
    }
    public void returnToMenu()
    {
        currentGameState = GameState.End;
        Destroy(gameObject);
    }
    public void PowerUp()
    {
        GhostManager.scaredGhosts();
        AudioManager.scaredGhosts();
        UIManager.startGhostTimer();
    }
    public void deadGhost(int ghost)
    {
        GhostManager.killGhost(ghost);
        AudioManager.ghostRecover();
    }
    public void StartGame()
    {
        currentGameState = GameState.Start;
    }
    public void MenuState()
    {
        currentGameState = GameState.Menu;
    }
    public void setupScene()
    {
        AudioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
        PacStudentController = GameObject.FindGameObjectWithTag("Player").GetComponent<PacStudentController>();
        GhostManager = GameObject.FindGameObjectWithTag("GhostManager").GetComponent<GhostManager>();
    }
}
