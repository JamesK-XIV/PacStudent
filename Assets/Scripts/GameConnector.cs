using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameConnector : MonoBehaviour
{
    public AudioManager AudioManager;
    public PacStudentController PacStudentController;
    public GhostManager GhostManager;
    public UIManager UIManager;
    // Start is called before the first frame update
    void Start()
    {
        UIManager = GameObject.FindGameObjectWithTag("UIManager").GetComponent<UIManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
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
}
