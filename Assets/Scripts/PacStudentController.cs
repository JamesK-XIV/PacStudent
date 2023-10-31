using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacStudentController : MonoBehaviour
{
    private Tween activeTween = null;
    public Animator animatorController;
    public AudioSource aud;
    private float temptime;
    private string lastInput;
    private string currentInput;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        getInput();
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
                animatorController.ResetTrigger("Right");
                animatorController.ResetTrigger("Down");
                animatorController.ResetTrigger("Left");
                animatorController.ResetTrigger("Up");
            }
            if (Time.time > (temptime + 0.5))
            {
                aud.Play();
                temptime = Time.time;
            }
        }
        else
        {
            if (lastInput != null)
            {
                if(movementCheck(lastInput))
                {
                    currentInput = lastInput;
                    AddTween();
                }
                else if(movementCheck(currentInput))
                {
                    AddTween();
                }
            }
        }
    }
    public void AddTween()
    {
        if (currentInput.Equals("down"))
        {
            activeTween = new Tween(gameObject.transform.position, gameObject.transform.position + Vector3.down, Time.time);
            animatorController.SetTrigger("Down");

        }
        else if (currentInput.Equals("right"))
        {
            activeTween = new Tween(gameObject.transform.position, gameObject.transform.position + Vector3.right, Time.time);
            animatorController.SetTrigger("Right");
        }
        else if (currentInput.Equals("left"))
        {
            activeTween = new Tween(gameObject.transform.position, gameObject.transform.position + Vector3.left, Time.time);
            animatorController.SetTrigger("Left");
        }
        else if (currentInput.Equals("up"))
        {
            activeTween = new Tween(gameObject.transform.position, gameObject.transform.position + Vector3.up, Time.time);
            animatorController.SetTrigger("Up");
        }
    }
    public void getInput()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            lastInput = "down";
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            lastInput = "right";
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            lastInput = "left";
        }
        else if (Input.GetKeyDown(KeyCode.W))
        {
            lastInput = "up";
        }
    }
    public Boolean movementCheck(String input)
    {
        if (input.Equals("down"))
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 1f);
            return (hitCheck(hit));
        }
        if (input.Equals("right"))
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right, 1f);
            return (hitCheck(hit));
        }
        if (input.Equals("left"))
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.left, 1f);
            return (hitCheck(hit));
        }
        if (input.Equals("up"))
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.up, 1f);
            return (hitCheck(hit));
        }
        return false;
    }
    public Boolean hitCheck(RaycastHit2D hit)
    {
        if (hit.collider != null)
        {
            if (hit.transform.gameObject.tag.Equals("Wall"))
            {
                return false;
            }
        }
        return true;
        
    }
}
