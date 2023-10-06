using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

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
            GenerateLevel();
        }
        
    }

    private void GenerateLevel()
    {
        Vector3 rot;
        for (int X = 0; X < levelMap.GetLength(1); X++)
        {
            for (int Y = 0; Y < levelMap.GetLength(0); Y++)
            {
                if (levelMap[Y, X] != 0)
                {
                    if (Y == 0 && X == 0)
                    {
                        rot = Rotation(Y, X, levelMap[Y, X], -1, -1);
                    }
                    else if (Y == 0)
                    {
                        rot = Rotation(Y, X, levelMap[Y, X], -1, levelMap[Y, X -1]);
                    }
                    else if (X == 0)
                    {
                        rot = Rotation(Y, X, levelMap[Y, X], levelMap[Y - 1 , X], -1);
                    }
                    else
                    {
                        rot = Rotation(Y, X, levelMap[Y, X], levelMap[Y, X - 1], levelMap[Y - 1, X]);
                    }
                    corner = Instantiate(levelSprites[levelMap[Y, X] - 1], new Vector3(X, -Y, 0), Quaternion.Euler(rot));
                    corner.transform.parent = LevelQuad.transform;
                }
            }
        }
        Instantiate(LevelQuad, new Vector3(levelMap.GetLength(0) * 2 - 3, 0, 0), Quaternion.Euler(0, 180, 0));
        Instantiate(LevelQuad, new Vector3(0, -levelMap.GetLength(1) * 2, 0), Quaternion.Euler(180, 0, 0));
        Instantiate(LevelQuad, new Vector3(levelMap.GetLength(0) * 2 - 3, -levelMap.GetLength(1) * 2, 0), Quaternion.Euler(180, 180, 0));
        temp = true;
    }

    private Vector3 Rotation(int x, int y, int piece, int left, int up)
    {
        if (up == -1 && left == -1)
        {
            return new Vector3(0, 0, -90);
        }
        return new Vector3(0,0,0);
    }
}
