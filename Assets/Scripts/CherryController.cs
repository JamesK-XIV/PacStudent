using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class CherryController : MonoBehaviour
{
    private float timer;
    public GameObject cherry;
    private GameObject cherryClone;
    private Vector3 startPos = new Vector3(0, 0, 0);
    private float moveTime;
    private Vector3 endPos;
    private float xPos;
    private float yPos;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (GameConnector.currentGameState == GameConnector.GameState.Start)
        {

            timer += Time.deltaTime;
            if (timer >= 10)
            {
                spawnCherry();
            }
            if (cherryClone != null)
            {
                cherryClone.transform.position = Vector3.Lerp(startPos, endPos, (Time.time - moveTime) / 10);
                if (Vector3.Distance(cherryClone.transform.position, endPos) < 0.1f)
                {

                    Destroy(cherryClone);
                    startPos = new Vector3(0, 0, 0);
                }

            }
        }
    }
    private void spawnCherry()
    {
        int whereIsRandom = Random.Range(0, 2);
        int height = Random.Range(0, 2);
        int side = Random.Range(0, 2);
        float randomNess = (Random.Range(0, 101));
        randomNess = randomNess / 100;
        if (whereIsRandom == 0)
        {
            if (height == 0)
            {
                cherryClone = Instantiate(cherry, Camera.main.ViewportToWorldPoint(new Vector3(1, randomNess, 0.5f)), Quaternion.identity);

            }
            if (height == 1)
            {
                cherryClone = Instantiate(cherry, Camera.main.ViewportToWorldPoint(new Vector3(0, randomNess, 0.5f)), Quaternion.identity);
            }
        }
        else if (whereIsRandom == 1)
        {
            if (side == 0)
            {
                cherryClone =  Instantiate(cherry, Camera.main.ViewportToWorldPoint(new Vector3(randomNess, 1, 0.5f)), Quaternion.identity);
            }
            if (side == 1)
            {
                cherryClone = Instantiate(cherry, Camera.main.ViewportToWorldPoint(new Vector3(randomNess, 0, 0.5f)), Quaternion.identity);
            }
        }
        startPos = cherryClone.transform.position;
        moveTime = Time.time;
        endPos = new Vector3(startPos.x - (startPos.x - 13.5f)*2, startPos.y - (startPos.y + 14)*2, 0);
    }
 }
