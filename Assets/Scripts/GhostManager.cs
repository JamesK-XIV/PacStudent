using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostManager : MonoBehaviour
{
    public GameObject[] ghosts;
    private float scaredTimer;
    private float[] deadTimer = new float[4];
    // Start is called before the first frame update
    void Start()
    {
        getController(0).ghostState = 0;
        getController(1).ghostState = 0;
        getController(2).ghostState = 0;
        getController(3).ghostState = 0;
        getController(0).moving = false;
        getController(1).moving = false;
        getController(2).moving = false;
        getController(3).moving = false;

    }

    // Update is called once per frame
    void Update()
    {
        if (scaredTimer != -1)
        {
            scaredTimer += Time.deltaTime;
        }
        for (int x =  0; x < ghosts.Length; x++)
        {
            if (getController(x).ghostState == 1)
            {
                if (scaredTimer >= 10)
                {
                    getController(x).ghostState = 0;
                }
            }
            else if (getController(x).ghostState == 2)
                deadTimer[x] += Time.deltaTime;
            if (deadTimer[x] >= 10)
            {
                getController(x).ghostState = 0;
                deadTimer[x] = 0;
            }
        }
        if (scaredTimer >= 10)
        {
            scaredTimer = -1;
        }
    }

    public void scaredGhosts()
    {
        getController(0).ghostState = 1;
        getController(1).ghostState = 1;
        getController(2).ghostState = 1;
        getController(3).ghostState = 1;
        ghosts[0].GetComponent<Animator>().SetTrigger("Scared");
        ghosts[1].GetComponent<Animator>().SetTrigger("Scared");
        ghosts[2].GetComponent<Animator>().SetTrigger("Scared");
        ghosts[3].GetComponent<Animator>().SetTrigger("Scared");
        scaredTimer = 0;
    }

    public int getStatus(int ghost)
    {
        return getController(ghost).ghostState;
    }
    public void killGhost(int ghost)
    {
            ghosts[ghost].GetComponentInParent<Animator>().SetTrigger("Dead");
            if (Random.Range(0,1) == 0)
            {
                getController(ghost).moving = false;
            }
        getController(ghost).ghostState = 2;
    }
    public GhostController getController(int ghost)
    {
        return ghosts[ghost].GetComponent<GhostController>();
    }
    public void setGhosts()
    {
        getController(0).moving = true;
        getController(1).moving = true;
        getController(2).moving = true;
        getController(3).moving = true;
    }
    public bool allAlive()
    {
        return (getController(0).ghostState == 0 && getController(1).ghostState == 0 && getController(2).ghostState == 0 && getController(3).ghostState == 0);
    }
}
