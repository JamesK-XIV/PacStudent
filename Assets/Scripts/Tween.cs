using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tween
{
    public Vector3 StartPos { get; private set; }
    public Vector3 EndPos { get; private set; }
    public float StartTime { get; private set; }

    public Tween(Vector3 startPos, Vector3 endPos, float time)
    {
        StartPos = startPos;
        EndPos = endPos;
        StartTime = time;
    }

}
