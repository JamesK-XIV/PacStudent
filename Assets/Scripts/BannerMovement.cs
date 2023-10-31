using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BannerMovement : MonoBehaviour
{
    private Tween activeTween = null;
    private Vector3 startPos;
    public Boolean moveUp;
    // Start is called before the first frame update
    void Start()
    {
        startPos = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        if (activeTween != null)
        {
            if (Vector3.Distance(gameObject.transform.position, activeTween.EndPos) > 0.1f)
            {
                float percentage = (Time.time - activeTween.StartTime) / (Vector3.Distance(activeTween.StartPos, activeTween.EndPos) / 2);
                gameObject.transform.position = Vector3.Lerp(activeTween.StartPos, activeTween.EndPos, percentage);
            }
            if (Vector3.Distance(gameObject.transform.position, activeTween.EndPos) < 0.1f)
            {
                gameObject.transform.position = activeTween.EndPos;
                activeTween = null;
            }
        }
        else
        {
            AddTween();
        }
        if (Mathf.Abs(Vector3.Distance(gameObject.transform.position, startPos)) >= 10.0f)
        {
            gameObject.transform.position = startPos;
        }
    }
    private void AddTween()
    {
        if (moveUp)
        {
            activeTween = new Tween(gameObject.transform.position, gameObject.transform.position + Vector3.up, Time.time);
        }
        else
        {
            {
                activeTween = new Tween(gameObject.transform.position, gameObject.transform.position + Vector3.down, Time.time);
            }
        }
    }
}
