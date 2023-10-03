using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathTrigger : MonoBehaviour
{
    // Start is called before the first frame update
    public Animator animatorController;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        animatorController.SetTrigger("Death");
    }
}
