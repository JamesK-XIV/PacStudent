using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //private Tween activeTween;
    private Tween activeTween = null;
    [SerializeField]
    private GameObject player;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (activeTween != null)
        {
            if (Vector3.Distance(player.transform.position, activeTween.EndPos) > 0.1f)
            {
                float percentage = (Time.time - activeTween.StartTime) / Vector3.Distance(activeTween.StartPos, activeTween.EndPos);
                player.transform.position = Vector3.Lerp(activeTween.StartPos, activeTween.EndPos, percentage);
            }
            if (Vector3.Distance(player.transform.position, activeTween.EndPos) < 0.1f)
            {
                player.transform.position = activeTween.EndPos;
                activeTween = null;
            }
        }
        AddTween();
    }
    public void AddTween()
    {
        if (Vector3.Distance(player.transform.position, new Vector3(6, -1, 0)) < 0.1f)
        {
            activeTween = new Tween(player.transform.position, new Vector3(6, -5, 0), Time.time);
        }
        else if (Vector3.Distance(player.transform.position, new Vector3(1, -1, 0)) < 0.1f)
        {
            activeTween = new Tween(player.transform.position, new Vector3(6, -1, 0), Time.time);
        }
        else if (Vector3.Distance(player.transform.position, new Vector3(6, -5, 0)) < 0.1f)
        {
            activeTween = new Tween(player.transform.position, new Vector3(1, -5, 0), Time.time);
        }
        else if (Vector3.Distance(player.transform.position, new Vector3(1, -5, 0)) < 0.1f)
        {
            activeTween = new Tween(player.transform.position, new Vector3(1, -1, 0), Time.time);
        }
    }
}
