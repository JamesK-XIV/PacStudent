using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using UnityEditor.ShaderKeywordFilter;
using UnityEngine;
using UnityEngine.Windows;

public class GhostController : MonoBehaviour
{
    // Start is called before the first frame update
    public int ghostState { get; set; }

    private Tween activeTween = null;
    private int lastDirection;
    public GameObject player;
    private enum ghostSpawn { spawn, outside };
    private ghostSpawn spawn = ghostSpawn.spawn;
    private Vector3 target;
    private Vector3 spawnPos;
    private void Start()
    {
        spawnPos = transform.position;
    }
    // Update is called once per frame
    void Update()
    {
        if (GameConnector.currentGameState == GameConnector.GameState.Start)
        {
            if (spawn == ghostSpawn.spawn)
            {
                if (activeTween != null)
                {
                    tweener();
                }
                else
                {
                    leaveSpwan();
                }
            }
            else
            {
                if (activeTween != null)
                {
                    tweener();
                }
                else
                {
                    checkMovement(lastDirection);
                }
            }
        }
    }
    private void checkMovement(int back)
    {
        bool[] valid = new bool[4];
        valid[0] = false;
        valid[1] = false;
        valid[2] = false;
        valid[3] = false;
        if (back != 4)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position + Vector3.down, Vector2.down, 0.01f);
            if (hit.collider != null) {
                if (hit.collider.gameObject.tag.Equals("Wall"))
                {
                    valid[0] = false;
                }
                else
                {
                    valid[0] = true;
                }
            }
            else
            {
                valid[0] = true;
            }
        }
        if (back != 3)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position + Vector3.right, Vector2.right, 0.01f);
            if (hit.collider != null) {
                if (hit.collider.gameObject.tag.Equals("Wall"))
                {
                    valid[1] = false;
                }
                else if (hit.collider.gameObject.tag.Equals("TeleporterLeft") || hit.collider.gameObject.tag.Equals("TeleporterRight"))
                {
                    moveBack(back);
                }
                else
                {
                    valid[1] = true;
                }
            }
            else
            {
                valid[1] = true;
            }
        }
        if (back != 2)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position + Vector3.left, Vector2.left, 0.01f);
            if (hit.collider != null) {
                if (hit.collider.gameObject.tag.Equals("Wall"))
                {
                    valid[2] = false;
                }
                else if (hit.collider.gameObject.tag.Equals("TeleporterLeft") || hit.collider.gameObject.tag.Equals("TeleporterRight"))
                {
                    moveBack(back);
                }
                else
                {
                    valid[2] = true;
                }
            }
            else
            {
                valid[2] = true;
            }
        }
        if (back != 1)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position + Vector3.up, Vector2.up, 0.01f);
            if (hit.collider != null) {
                if (hit.collider.gameObject.tag.Equals("Wall"))
                {
                    valid[3] = false;
                }
                else
                {
                    valid[3] = true;
                }
            }
            else
            {
                valid[3] = true;
            }
        }
        if (activeTween == null)
        {
            if (name == "PurpleGhostPhone" || ghostState == 1)
            {
                DecideRun(valid);
            }
            else if (name == "GreenGhostPhone")
            {
                DecideMovement(valid);
            }
            else if (name == "BrownGhostPhone")
            {
                DecideRandom(valid);
            }
            else if (name == "YellowGhostPhone")
            {
                DecideWall(valid);
            }
        }
    }
    private void DecideRandom(bool[] checks)
    {
        bool temp = false;
        int a = UnityEngine.Random.Range(0, 4);
        while (!temp)
        {
            a = UnityEngine.Random.Range(0, 4);
            temp = checks[a];
        }
        if (a == 0)
        {
            activeTween = new Tween(gameObject.transform.position, gameObject.transform.position + Vector3.down, Time.time, 0.3f);
            lastDirection = 1;
        }
        if (a == 1)
        {
            activeTween = new Tween(gameObject.transform.position, gameObject.transform.position + Vector3.right, Time.time, 0.3f);
            lastDirection = 2;
        }
        if (a == 2)
        {
            activeTween = new Tween(gameObject.transform.position, gameObject.transform.position + Vector3.left, Time.time, 0.3f);
            lastDirection = 3;
        }
        if (a == 3)
        {
            activeTween = new Tween(gameObject.transform.position, gameObject.transform.position + Vector3.up, Time.time, 0.3f);
            lastDirection = 4;
        }
    }
    private void DecideMovement(bool[] checks)
    {
        float upDistance = 1000000;
        float rightDistance = 1000000;
        float leftDistance = 1000000;
        float downDistance = 100000;
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
        if (downDistance <= upDistance && downDistance <= rightDistance && downDistance <= leftDistance)
        {
            activeTween = new Tween(gameObject.transform.position, gameObject.transform.position + Vector3.down, Time.time, 0.3f);
            lastDirection = 1;
            //animatorController.SetTrigger("Down");

        }
        else if (upDistance <= downDistance && upDistance <= rightDistance && upDistance <= leftDistance)
        {
            activeTween = new Tween(gameObject.transform.position, gameObject.transform.position + Vector3.up, Time.time, 0.3f);
            //animatorController.SetTrigger("Right");
            lastDirection = 4;
        }
        else if (rightDistance <= upDistance && rightDistance <= downDistance && rightDistance <= leftDistance)
        {
            activeTween = new Tween(gameObject.transform.position, gameObject.transform.position + Vector3.right, Time.time, 0.3f);
            //animatorController.SetTrigger("Left");
            lastDirection = 2;
        }
        else if (leftDistance <= upDistance && leftDistance <= rightDistance && leftDistance <= downDistance)
        {
            activeTween = new Tween(gameObject.transform.position, gameObject.transform.position + Vector3.left, Time.time, 0.3f);
            //animatorController.SetTrigger("Up");
            lastDirection = 3;
        }

    }
    private void DecideRun(bool[] checks)
    {
        float upDistance = 0;
        float rightDistance = 0;
        float leftDistance = 0;
        float downDistance = 0;
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
        if (downDistance >= upDistance && downDistance >= rightDistance && downDistance >= leftDistance)
        {
            activeTween = new Tween(gameObject.transform.position, gameObject.transform.position + Vector3.down, Time.time, 0.3f);
            lastDirection = 1;
            //animatorController.SetTrigger("Down");

        }
        else if (upDistance >= downDistance && upDistance >= rightDistance && upDistance >= leftDistance)
        {
            activeTween = new Tween(gameObject.transform.position, gameObject.transform.position + Vector3.up, Time.time, 0.3f);
            //animatorController.SetTrigger("Right");
            lastDirection = 4;
        }
        else if (rightDistance >= upDistance && rightDistance >= downDistance && rightDistance >= leftDistance)
        {
            activeTween = new Tween(gameObject.transform.position, gameObject.transform.position + Vector3.right, Time.time, 0.3f);
            //animatorController.SetTrigger("Left");
            lastDirection = 2;
        }
        else if (leftDistance >= upDistance && leftDistance >= rightDistance && leftDistance >= downDistance)
        {
            activeTween = new Tween(gameObject.transform.position, gameObject.transform.position + Vector3.left, Time.time, 0.3f);
            //animatorController.SetTrigger("Up");
            lastDirection = 3;
        }
        target = new Vector3(13, -11, 0);

    }
    private void DecideWall(bool[] checks)
    {
        decideTarget();
        float upDistance = 1000000;
        float rightDistance = 1000000;
        float leftDistance = 1000000;
        float downDistance = 100000;
        if (checks[0])
        {
            downDistance = Vector3.Distance(transform.position + Vector3.down, target);
        }
        if (checks[1])
        {
            rightDistance = Vector3.Distance(transform.position + Vector3.right, target);
        }
        if (checks[2])
        {
            leftDistance = Vector3.Distance(transform.position + Vector3.left, target);
        }
        if (checks[3])
        {
            upDistance = Vector3.Distance(transform.position + Vector3.up, target);
        }
        if (rightDistance <= upDistance && rightDistance <= downDistance && rightDistance <= leftDistance)
        {
            activeTween = new Tween(gameObject.transform.position, gameObject.transform.position + Vector3.right, Time.time, 0.3f);
            //animatorController.SetTrigger("Left");
            lastDirection = 2;
        }
        else if (downDistance <= upDistance && downDistance <= rightDistance && downDistance <= leftDistance)
        {
            activeTween = new Tween(gameObject.transform.position, gameObject.transform.position + Vector3.down, Time.time, 0.3f);
            lastDirection = 1;
            //animatorController.SetTrigger("Down");

        }
        else if (leftDistance <= upDistance && leftDistance <= rightDistance && leftDistance <= downDistance)
        {
            activeTween = new Tween(gameObject.transform.position, gameObject.transform.position + Vector3.left, Time.time, 0.3f);
            //animatorController.SetTrigger("Up");
            lastDirection = 3;
        }
        else if (upDistance <= downDistance && upDistance <= rightDistance && upDistance <= leftDistance)
        {
            activeTween = new Tween(gameObject.transform.position, gameObject.transform.position + Vector3.up, Time.time, 0.3f);
            //animatorController.SetTrigger("Right");
            lastDirection = 4;
        }

    }
    private void tweener()
    {
        if (Vector3.Distance(gameObject.transform.position, activeTween.EndPos) > 0.1f)
        {
            float percentage = (Time.time - activeTween.StartTime) /activeTween.Duration;
            gameObject.transform.position = Vector3.Lerp(activeTween.StartPos, activeTween.EndPos, percentage);
        }
        if (Vector3.Distance(gameObject.transform.position, activeTween.EndPos) < 0.1f)
        {
            gameObject.transform.position = activeTween.EndPos;
            activeTween = null;
        }
    }
    private void leaveSpwan()
    {
        if (transform.position.x == 15)
        {
            activeTween = new Tween(gameObject.transform.position, gameObject.transform.position - Vector3.right, Time.time, 0.3f);
        }
        else if (transform.position.x == 12)
        {
            activeTween = new Tween(gameObject.transform.position, gameObject.transform.position + Vector3.right, Time.time, 0.3f);
        }
        else
        {
            if (transform.position.y == -15 || transform.position.y == -14 || transform.position.y == -13 || transform.position.y == -12)
            {
                activeTween = new Tween(gameObject.transform.position, gameObject.transform.position + Vector3.up, Time.time, 0.3f);
            }
            else
            {
                spawn = ghostSpawn.outside;
            }
        }
    }
    private void decideTarget()
    {
        if (transform.position.Equals(new Vector3(13, -11, 0)))
        {
            target = new Vector3(15, -1, 0);
        }
        if (transform.position.Equals(new Vector3(15, -1, 0)))
        {
            target = new Vector3(26, -1, 0);
        }
        if (transform.position.Equals(new Vector3(26, -1, 0)))
        {
            target = new Vector3(26, -20, 0);
        }
        if (transform.position.Equals(new Vector3(26, -20, 0)))
        {
            target = new Vector3(26, -27, 0);
        }
        if (transform.position.Equals(new Vector3(26, -27, 0)))
        {
            target = new Vector3(12, -27, 0);
        }
        if (transform.position.Equals(new Vector3(12, -27, 0)))
        {
            target = new Vector3(1, -27, 0);
        }
        if (transform.position.Equals(new Vector3(1, -27, 0)))
        {
            target = new Vector3(1, -8, 0);
        }
        if (transform.position.Equals(new Vector3(1, -8, 0)))
        {
            target = new Vector3(1, -1, 0);
        }
        if (transform.position.Equals(new Vector3(1, -1, 0)))
        {
            target = new Vector3(15, -1, 0);
        }
    }
    private void moveBack(int direction)
    {
        if (direction == 3)
        {
            activeTween = new Tween(gameObject.transform.position, gameObject.transform.position + Vector3.right, Time.time, 0.3f);
            lastDirection = 2;
        }
        if (direction == 2)
        {
            activeTween = new Tween(gameObject.transform.position, gameObject.transform.position + Vector3.left, Time.time, 0.3f);
            lastDirection = 3;
        }
    }
    public void deadMove()
    {
        activeTween = null;
        activeTween = new Tween(gameObject.transform.position, spawnPos, Time.time, 5);
        spawn = ghostSpawn.spawn;
    }
}
