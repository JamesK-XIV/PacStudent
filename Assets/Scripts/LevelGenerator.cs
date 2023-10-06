using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UIElements;

public class LevelGenerator : MonoBehaviour
{
    public GameObject level;
    public List<GameObject> levelSprites;
    [SerializeField]
    private Camera cam;
    private GameObject LevelQuad;
    private GameObject LevelQuadBottom;
    private GameObject corner;
    private int[,] rotateTypes;

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
        LevelQuadBottom = new GameObject("LevelQuadBottom");
        GenerateLevel();
        setCamera();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void GenerateLevel()
    {
        rotateTypes = new int[levelMap.GetLength(0), levelMap.GetLength(1)];
        Vector3 rot;
        int rightpiece;
        int bellowpiece;
        for (int X = 0; X < levelMap.GetLength(1); X++)
        {
            for (int Y = 0; Y < levelMap.GetLength(0); Y++)
            {
                if (levelMap[Y, X] != 0)
                {
                    Boolean bottom = false;
                   
                    if (Y == levelMap.GetLength(0) - 1)
                    {
                        bottom = true;
                        bellowpiece = -1;
                    }
                    else
                    {
                        bellowpiece = levelMap[Y + 1, X];
                    }
                    if (X == levelMap.GetLength(1) - 1)
                    {
                        rightpiece = -1;
                    }
                    else
                    {
                        rightpiece = levelMap[Y, X + 1];
                    }
                    if (Y == 0 && X == 0)
                    {
                        rot = SetRotation(levelMap[Y, X], -1, rotateTypes[Y, X], -1, rotateTypes[Y, X], bottom, rightpiece, bellowpiece);
                    }
                    else if (X == 0)
                    {
                        rot = SetRotation(levelMap[Y, X], -1, -1, levelMap[Y - 1, X], rotateTypes[Y - 1, X], bottom, rightpiece, bellowpiece);
                    }
                    else if (Y == 0) 
                    {
                        rot = SetRotation(levelMap[Y, X], levelMap[Y, X -1], rotateTypes[Y, X -1], -1, -1, bottom, rightpiece, bellowpiece);
                    }
                    else
                    {
                        rot = SetRotation(levelMap[Y, X], levelMap[Y, X - 1], rotateTypes[Y, X - 1], levelMap[Y - 1, X], rotateTypes[Y - 1, X], bottom, rightpiece, bellowpiece);
                    }
                    rotateTypes[Y, X] = CheckRotate(rot);
                    corner = Instantiate(levelSprites[levelMap[Y, X] - 1], new Vector3(X, -Y, 0), Quaternion.Euler(rot));
                    corner.transform.parent = LevelQuad.transform;
                    if (!bottom)
                    {
                        corner = Instantiate(levelSprites[levelMap[Y, X] - 1], new Vector3(X, -Y, 0), Quaternion.Euler(rot));
                        corner.transform.parent = LevelQuadBottom.transform;
                    }
                    
                }
            }
        }
        Instantiate(LevelQuad, new Vector3(levelMap.GetLength(1) * 2 - 1, 0, 0), Quaternion.Euler(0, 180, 0));
        Instantiate(LevelQuadBottom, new Vector3(0, -levelMap.GetLength(0) * 2 + 2, 0), Quaternion.Euler(180, 0, 0));
        Instantiate(LevelQuadBottom, new Vector3(levelMap.GetLength(1) * 2 - 1, -levelMap.GetLength(0) * 2 + 2, 0), Quaternion.Euler(180, 180, 0));
        Destroy(LevelQuadBottom);
    }

    private Vector3 SetRotation(int piece, int leftpiece, int leftrotate, int uppiece, int uprotate, Boolean bottom, int rightpiece, int bellowpiece)
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
                if (uprotate == 2 || uprotate == 1)
                {
                    up = true;
                }
            }
            if (uppiece == 2 || uppiece == 4)
            {
                if (uprotate == 3 || uprotate == 1)
                {
                    up = true;
                }
            }
            if (uppiece == 7)
            {
                if (uprotate != 0)
                {
                    up = true;
                }
            }

        }
        if (leftpiece != -1)
        {
            if (leftpiece == 1 || leftpiece == 3)
            {
                if (leftrotate == 0 || leftrotate == 1)
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
            if (leftpiece == 7)
            {
                if (leftrotate != 3)
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
        if (piece == 7)
        {
            if (up && left)
            {
                if (bottom)
                {
                    return new Vector3(0, 0, 0);
                }
                else if (bellowpiece == 1 || bellowpiece == 2 || bellowpiece == 3 || bellowpiece == 4 || bellowpiece == 7)
                {
                    if (rightpiece == 1 || rightpiece == 2 || rightpiece == 3 || rightpiece == 4 || rightpiece == 7)
                    {
                        return new Vector3(0, 0, 180);
                    }
                    else
                    {
                        return new Vector3(0, 0, 90);
                    }
                }
                else
                {
                    return new Vector3(0, 0, 0);
                }
            }
            else if (up)
            {
                return new Vector3(0, 0, -90);
            }
            else if (left)
            {
                return new Vector3(0, 0, 180);
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
    private void setCamera()
    {
        cam.transform.position = new Vector3(levelMap.GetLength(1), -levelMap.GetLength(0) + 0.5f, -0.3f);
        if (levelMap.GetLength(1) > levelMap.GetLength(0))
        {
            cam.orthographicSize = levelMap.GetLength(1);
        }
        else
        {
            cam.orthographicSize = levelMap.GetLength(0);
        }

    }
}
