using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderKeywordFilter;
using UnityEngine;
using UnityEngine.Windows;

public class GhostController : MonoBehaviour
{
    // Start is called before the first frame update
    public int ghostState { get; set; }
    public bool moving { get; set; }

    private Tween activeTween = null;
    private int lastDirection;
    public GameObject player;

    // Update is called once per frame
    void Update()
    {
        if (GameConnector.currentGameState == GameConnector.GameState.Start)
        {
            if (activeTween != null)
            {
                tweener();
            }
            else
            {
                DecideMovement(checkMovement(lastDirection));
            }
        }
    }
    private bool[] checkMovement(int back)
    {
        bool[] valid = new bool[4];
        valid[0] = false;
        valid[1] = false;
        valid[2] = false;
        valid[3] = false;
        if (back != 1)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position + Vector3.down, Vector2.down, 0.01f);
            if (hit.collider != null && hit.collider.gameObject.tag.Equals("Wall")) {
                valid[0] = false;
            }
            else
            {
                valid[0] = true;
            }
        }
        if (back != 2)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position + Vector3.right, Vector2.right, 0.01f);
            if (hit.collider != null && hit.collider.gameObject.tag.Equals("Wall"))
            {
                valid[1] = false;
            }
            else
            {
                valid[1] = true;
            }
        }
        if (back != 3)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position + Vector3.left, Vector2.left, 0.01f);
            if (hit.collider != null && hit.collider.gameObject.tag.Equals("Wall"))
            {
                valid[2] = false;
            }
            else
            {
                valid[2] = true;
            }
        }
        if (back != 4)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position + Vector3.up, Vector2.up, 0.01f);
            if (hit.collider != null && hit.collider.gameObject.tag.Equals("Wall"))
            {
                valid[3] = false;
            }
            else
            {
                valid[3] = true;
            }
        }
        return valid;
    }
    private void DecideMovement(bool[] checks)
    {
        float upDistance = 1000;
        float rightDistance = 1000;
        float leftDistance = 1000;
        float downDistance = 1000;
        if (checks[0])
        {
            downDistance = Vector3.Distance(transform.position + Vector3.down, player.transform.position);
        }
        if (checks[1])
        {
            rightDistance = Vector3.Distance(transform.position + Vector3.right, player.transform.position);
        }
        if (checks[2])
        {
            leftDistance = Vector3.Distance(transform.position + Vector3.left, player.transform.position);
        }
        if (checks[3])
        {
            upDistance = Vector3.Distance(transform.position + Vector3.up, player.transform.position);
        }
        if (downDistance < upDistance && downDistance < rightDistance && downDistance < leftDistance)
        {
            activeTween = new Tween(gameObject.transform.position, gameObject.transform.position + Vector3.down, Time.time);
            lastDirection = 1;
            //animatorController.SetTrigger("Down");

        }
        else if (upDistance < downDistance && upDistance < rightDistance && upDistance < leftDistance)
        {
            activeTween = new Tween(gameObject.transform.position, gameObject.transform.position + Vector3.right, Time.time);
            //animatorController.SetTrigger("Right");
            lastDirection = 2;
        }
        else if (rightDistance < upDistance && rightDistance < upDistance && rightDistance < leftDistance)
        {
            activeTween = new Tween(gameObject.transform.position, gameObject.transform.position + Vector3.left, Time.time);
            //animatorController.SetTrigger("Left");
            lastDirection = 3;
        }
        else if (leftDistance < upDistance && leftDistance < rightDistance && leftDistance < downDistance)
        {
            activeTween = new Tween(gameObject.transform.position, gameObject.transform.position + Vector3.up, Time.time);
            //animatorController.SetTrigger("Up");
            lastDirection = 4;
        }

    }
    private void tweener()
    {
        if (Vector3.Distance(gameObject.transform.position, activeTween.EndPos) > 0.1f)
        {
            float percentage = (Time.time - activeTween.StartTime) / (Vector3.Distance(activeTween.StartPos, activeTween.EndPos) / 4);
            gameObject.transform.position = Vector3.Lerp(activeTween.StartPos, activeTween.EndPos, percentage);
        }
        if (Vector3.Distance(gameObject.transform.position, activeTween.EndPos) < 0.1f)
        {
            gameObject.transform.position = activeTween.EndPos;
            activeTween = null;
        }
    }
}
