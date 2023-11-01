using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostManager : MonoBehaviour
{
    public GameObject[] ghosts;
    private float[] scaredTimer = new float[4];
    private float[] deadTimer = new float[4];
    // Start is called before the first frame update
    void Start()
    {
        getController(0).ghostState = 0;
        getController(1).ghostState = 0;
        getController(2).ghostState = 0;
        getController(3).ghostState = 0;
    }

    // Update is called once per frame
    void Update()
    {
        for (int x =  0; x < ghosts.Length; x++)
        {
            if (getController(x).ghostState == 1)
            {
                scaredTimer[x] += Time.deltaTime;
                if (scaredTimer[x] >= 10)
                {
                    getController(x).ghostState = 0;
                    scaredTimer[x] = 0;
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
}
