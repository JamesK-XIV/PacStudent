using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class LevelGenerator : MonoBehaviour
{
    public GameObject level;
    public List<GameObject> levelSprites;
    public GameObject test;
    private GameObject LevelQuad;
    private GameObject corner;
    private int[,] rotateTypes;
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
        rotateTypes = new int[levelMap.GetLength(0), levelMap.GetLength(1)];
        Vector3 rot;
        for (int X = 0; X < levelMap.GetLength(1); X++)
        {
            for (int Y = 0; Y < levelMap.GetLength(0); Y++)
            {
                if (levelMap[Y, X] != 0)
                {
                    if (Y == 0 && X == 0)
                    {
                        rot = SetRotation(levelMap[Y, X], -1, rotateTypes[Y, X], -1, rotateTypes[Y, X]);
                    }
                    else if (Y == 0)
                    {
                        rot = SetRotation(levelMap[Y, X], -1, rotateTypes[Y, X], levelMap[Y, X -1], rotateTypes[Y, X]);
                    }
                    else if (X == 0) 
                    {
                        rot = SetRotation(levelMap[Y, X], levelMap[Y - 1 , X], rotateTypes[Y - 1, X], -1, rotateTypes[Y, X]);
                    }
                    else
                    {
                        rot = SetRotation(levelMap[Y, X], levelMap[Y, X - 1], rotateTypes[Y, X - 1], levelMap[Y - 1, X], rotateTypes[Y - 1, X]);
                    }
                    rotateTypes[Y, X] = CheckRotate(rot);
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

    private Vector3 SetRotation(int piece, int uppiece, int uprotate, int leftpiece, int leftrotate)
    {
        Boolean up = false;
        Boolean left = false;
        if (leftpiece == -1 && uppiece == -1)
        {
            return new Vector3(0, 0, -90);
        }

        if (uppiece != -1)
        {
            if (uppiece == 1 || uppiece == 3)
            {
                if (uprotate == 1 || uprotate == 2)
                {
                    up = true;
                }
            }
            if (uppiece == 2 || uppiece == 4)
            {
                if (uprotate == 1 || uprotate == 3)
                {
                    up = true;
                }
            }

        }
        if (leftpiece != -1)
        {
            if (leftpiece == 1 || leftpiece == 3)
            {
                if (leftrotate == 0 || leftrotate == 3)
                {
                    left = true;
                }
            }
            if (leftpiece == 2 || leftpiece == 4)
            {
                if (leftrotate == 0 || leftrotate == 2)
                {
                    left = true;
                }
            }
        }
        if (piece == 2 || piece == 4)
        {
            if (up)
            {
                return new Vector3(0, 0, 90);
            }
            else
            {
                return new Vector3(0, 0, 0);
            }
        }
        if (piece == 1 || piece == 3)
        {
            if (left && up)
            {
                return new Vector3(0, 0, 90);
            }
            else if (left)
            {
                return new Vector3(0, 0, 180);
            }
            else if (up)
            {
                return new Vector3(0, 0, 0);
            }
            else
            {
                return new Vector3(0, 0, -90);
            }
        }
        return new Vector3(0, 0, 0);

    }
    private int CheckRotate(Vector3 rot)
    {
        if (Vector3.Distance(rot, new Vector3(0, 0, -90)) == 0)
        {
            return 1;
        }
        else if (Vector3.Distance(rot, new Vector3(0, 0, 0)) == 0)
        {
            return 0;
        }
        else if (Vector3.Distance(rot, new Vector3(0, 0, 180)) == 0)
        {
            return 2;
        }
        else if (Vector3.Distance(rot, new Vector3(0, 0, 90)) == 0)
        {
            return 3;
        }
        return 4;
    }
}
