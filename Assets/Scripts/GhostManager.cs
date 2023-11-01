using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostManager : MonoBehaviour
{
    public GameObject[] ghosts;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void scaredGhosts()
    {
        ghosts[0].GetComponent<Animator>().SetTrigger("Scared");
        ghosts[1].GetComponent<Animator>().SetTrigger("Scared");
        ghosts[2].GetComponent<Animator>().SetTrigger("Scared");
        ghosts[3].GetComponent<Animator>().SetTrigger("Scared");
    }
}
