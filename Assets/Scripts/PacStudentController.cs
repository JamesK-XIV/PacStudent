using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class PacStudentController : MonoBehaviour
{
    public GameObject player;
    private bool playerAlive;
    private Tween activeTween = null;
    public Animator animatorController;
    public AudioSource aud;
    public AudioClip[] audioclips;
    private float temptime;
    private string lastInput;
    private string currentInput;
    private float lifeCount;
    private GameObject hitObject;
    public ParticleSystem[] particles;
    private bool playerWasMoving;
    private float playerScore;
    public GameConnector gameManager;
    public GameObject startPos;
    // Start is called before the first frame update
    void Start()
    {
        playerAlive = true;
        playerScore = 0;
        lifeCount = 3;
        currentInput = "";
    }

    // Update is called once per frame
    void Update()
    {
        if (playerAlive)
        {
            getInput();
            if (activeTween != null)
            {
                playerWasMoving = true;
                if (Vector3.Distance(gameObject.transform.position, activeTween.EndPos) > 0.1f)
                {
                    float percentage = (Time.time - activeTween.StartTime) / (Vector3.Distance(activeTween.StartPos, activeTween.EndPos) / 4);
                    gameObject.transform.position = Vector3.Lerp(activeTween.StartPos, activeTween.EndPos, percentage);
                }
                if (Vector3.Distance(gameObject.transform.position, activeTween.EndPos) < 0.1f)
                {
                    gameObject.transform.position = activeTween.EndPos;
                    activeTween = null;
                    particles[0].Stop();
                    animatorController.SetTrigger("Neutral");
                    if (hitObject != null)
                    {
                        if (hitObject.tag.Equals("TeleporterLeft"))
                        {
                            gameObject.transform.position = GameObject.FindGameObjectWithTag("TeleporterRight").transform.position;
                            AddTween();
                        }
                        else if (hitObject.tag.Equals("TeleporterRight"))
                        {
                            gameObject.transform.position = GameObject.FindGameObjectWithTag("TeleporterLeft").transform.position;
                            AddTween();
                        }
                    }
                    hitObject = null;
                }
                if (Time.time > (temptime + 0.5) && !aud.isPlaying)
                {
                    aud.Play();
                    temptime = Time.time;
                }
            }
            else
            {
                if (lastInput != null)
                {
                    if (movementCheck(lastInput))
                    {
                        currentInput = lastInput;
                        particles[0].Play();
                        AddTween();
                    }
                    else if (movementCheck(currentInput))
                    {
                        particles[0].Play();
                        AddTween();
                    }
                    else
                    {
                        aud.clip = audioclips[2];
                        if (playerWasMoving)
                        {
                            particles[1].transform.position = hitObject.transform.position;
                            particles[1].Play();
                            aud.Play();
                            playerWasMoving = false;
                        }
                    }
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
                RaycastHit2D hit = Physics2D.Raycast(transform.position + Vector3.down, Vector2.down, 0.01f);
                return (hitCheck(hit));
            }
            if (input.Equals("right"))
            {
                RaycastHit2D hit = Physics2D.Raycast(transform.position + Vector3.right, Vector2.right, 0.01f);
                return (hitCheck(hit));
            }
            if (input.Equals("left"))
            {
                RaycastHit2D hit = Physics2D.Raycast(transform.position + Vector3.left, Vector2.left, 0.01f);
                return (hitCheck(hit));
            }
            if (input.Equals("up"))
            {
                RaycastHit2D hit = Physics2D.Raycast(transform.position + Vector3.up, Vector2.up, 0.01f);
                return (hitCheck(hit));
            }
            return false;
        }
    public Boolean hitCheck(RaycastHit2D hit)
    {
        if (hit.collider != null)
        {
            hitObject = hit.transform.gameObject;
            if (hit.transform.gameObject.tag.Equals("Wall"))
            {
                aud.clip = audioclips[2];
                if (playerWasMoving && lastInput.Equals(currentInput))
                {
                    particles[1].transform.position = hit.transform.position;
                    particles[1].Play();
                    aud.Play();
                    playerWasMoving = false;
                }
                return false;
            }
        }
        aud.clip = audioclips[0];
        return true;
    }
    public float getScore()
    {
        return playerScore;
    }
    IEnumerator OnTriggerEnter2D(Collider2D collider)
    {
        yield return null;
        if (collider.gameObject.tag.Equals("Pellet"))
        {
            aud.clip = audioclips[1];
            aud.Play();
            playerScore += 10;
            Destroy(collider.gameObject);
        }
        else if (collider.gameObject.tag.Equals("Cherry"))
        {
            playerScore += 100;
            Destroy(collider.gameObject);
        }
        else if (collider.gameObject.tag.Equals("PowerUp"))
        {
            gameManager.PowerUp();
            Debug.Log("SUPER TIME");
        }
        else if (collider.gameObject.tag.Equals("Enemy"))
        {
            if (gameManager.GhostManager.getStatus(ghostTranslater(collider.gameObject.name)) == 1)
            {
                Debug.Log(collider.gameObject.name);
                playerScore += 300;
                gameManager.deadGhost(ghostTranslater(collider.gameObject.name));
            }
            else if (gameManager.GhostManager.getStatus(ghostTranslater(collider.gameObject.name)) == 0)
            {
                if (playerAlive)
                {
                    StartCoroutine(playerDeath());
                }
            }
        }
    }
    IEnumerator playerDeath()
    {
        playerAlive = false;
        gameManager.UIManager.loseLife();
        lifeCount -= 1;
        lastInput = null;
        currentInput = null;
        activeTween = null;
        while (!playerAlive)
        {
            gameObject.GetComponent<Animator>().SetTrigger("Neutral");
            gameObject.GetComponent<Animator>().SetTrigger("Death");
            particles[2].Play();
            yield return new WaitForSeconds(1);
            player.transform.position = startPos.transform.position;
            gameObject.GetComponent<Animator>().ResetTrigger("Death");
            gameObject.GetComponent<Animator>().ResetTrigger("Neutral");
            Debug.Log(lifeCount);
            playerAlive = true;
        }
        yield return null;
    }
    public int ghostTranslater(string name)
    {
        if (name == "GreenGhostPhone")
        {
            return 0;
        }
        else if (name == "PurpleGhostPhone")
        {
            return 1;
        }
        else if (name == "BrownGhostPhone")
        {
            return 2;
        }
        else if (name == "YellowGhostPhone")
        {
            return 3;
        }
        return 0;
    }
}
