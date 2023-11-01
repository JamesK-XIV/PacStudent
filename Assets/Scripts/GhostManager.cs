using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostManager : MonoBehaviour
{
    private bool ghostScared;
    public GameObject[] ghosts;
    private bool[] moving = new bool[4];
    private float scaredTimer;
    // Start is called before the first frame update
    void Start()
    {
        ghostScared = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (ghostScared)
        {
            scaredTimer += Time.deltaTime;
            if (scaredTimer >= 10)
            {
                ghostScared = true;
            }
        }
    }

    public void scaredGhosts()
    {
        ghostScared = true;
        ghosts[0].GetComponent<Animator>().SetTrigger("Scared");
        ghosts[1].GetComponent<Animator>().SetTrigger("Scared");
        ghosts[2].GetComponent<Animator>().SetTrigger("Scared");
        ghosts[3].GetComponent<Animator>().SetTrigger("Scared");
    }

    public bool getStatus()
    {
        return ghostScared;
    }
    public void killGhost(string name)
    {
        if (name.Equals("GreenGhostPhone"))
        {
            ghosts[0].GetComponent<Animator>().SetTrigger("Dead");
            if (Random.Range(0,1) == 0)
            {
                moving[0] = false;
            }
        }
    }
}
