using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadManager : MonoBehaviour
{
    public GameObject UI;
    public GameObject Save;
    // Start is called before the first frame update
    void Start()
    {
        if (GameObject.FindGameObjectWithTag("UIManager") == null)
        {
            Instantiate(UI);
        }
        if (GameObject.FindGameObjectWithTag("Save") == null)
        {
            Instantiate(Save);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
