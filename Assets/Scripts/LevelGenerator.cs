using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public GameObject level;
    public List<GameObject> levelSprites;
    public GameObject test;
    private GameObject LevelQuad;
    private GameObject corner;
    private Boolean temp = false;
    public int[,] levelMap =
{
{1,2,2,2,2,2,2,2,2,2,2,2,2,7},
{2,5,5,5,5,5,5,5,5,5,5,5,5,4},
{2,5,3,4,4,3,5,3,4,4,4,3,5,4},
{2,6,4,0,0,4,5,4,0,0,0,4,5,4},
{2,5,3,4,4,3,5,3,4,4,4,3,5,3},
{2,5,5,5,5,5,5,5,5,5,5,5,5,5},
{2,5,3,4,4,3,5,3,3,5,3,4,4,4},
{2,5,3,4,4,3,5,4,4,5,3,4,4,3},
{2,5,5,5,5,5,5,4,4,5,5,5,5,4},
{1,2,2,2,2,1,5,4,3,4,4,3,0,4},
{0,0,0,0,0,2,5,4,3,4,4,3,0,3},
{0,0,0,0,0,2,5,4,4,0,0,0,0,0},
{0,0,0,0,0,2,5,4,4,0,3,4,4,0},
{2,2,2,2,2,1,5,3,3,0,4,0,0,0},
{0,0,0,0,0,0,5,0,0,0,4,0,0,0},
};
    // Start is called before the first frame update
    void Start()
    {
        Destroy(level);
        LevelQuad = new GameObject("LevelQuad");
    }

    // Update is called once per frame
    void Update()
    {
        if (temp == false)
        {
            for (int y = 0; y < levelMap.GetLength(1); y++)
            {
                for (int x = 0; x < levelMap.GetLength(0); x++)
                {
                    if (levelMap[x,y] != 0)
                    {
                        corner = Instantiate(levelSprites[levelMap[x, y] - 1], new Vector3(y, -x, 0), Quaternion.identity);
                        corner.transform.parent = LevelQuad.transform;
                    }
                }
            }
            Instantiate(LevelQuad, new Vector3(levelMap.GetLength(0)*2-3, 0, 0), Quaternion.Euler(0, 180, 0));
            Instantiate(LevelQuad, new Vector3(0, -levelMap.GetLength(1)*2, 0), Quaternion.Euler(180, 0, 0));
            Instantiate(LevelQuad, new Vector3(levelMap.GetLength(0) * 2-3, -levelMap.GetLength(1) * 2 , 0), Quaternion.Euler(180, 180, 0));
            temp = true;
        }
        
    }
}
