using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SourcePair : MonoBehaviour
{
    [SerializeField] private Transform wall;

    private Evaluator evaluator;
    
    public void Start()
    {
        evaluator = FindObjectOfType<Evaluator>();
        evaluator.RegisterSource(this);
    }

    public void OnDestroy()
    {
        evaluator.UnregisterSource(this);
    }

    public (Vector3, Vector3) GetSources()
    {
        var start = wall.position + (wall.rotation * Vector3.Scale(Vector3.left, wall.localScale / 2f));
        var end = wall.position + (wall.rotation * Vector3.Scale(Vector3.right, wall.localScale / 2f));
        return (start, end);
    } 
    
    public (Vector2, Vector2) GetSources2D()
    {
        var (start, end) = GetSources();
        Vector2 start2D = new(start.x, start.z);
        Vector2 end2D = new(end.x, end.z);
        return (start2D, end2D);
    }
}
