using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tween
{
    public Transform Target { get; private set; }
    public Vector3 StartPos { get; private set; }
    public Vector3 EndPos { get; private set; }

    public Tween(Transform target, Vector3 startPos, Vector3 endPos)
    {
        Target = target;
        StartPos = startPos;
        EndPos = endPos;
    }

}
