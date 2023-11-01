using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostManager : MonoBehaviour
{
    private bool ghostScared;
    public GameObject[] ghosts;
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
                ghostScared = false;
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
}
