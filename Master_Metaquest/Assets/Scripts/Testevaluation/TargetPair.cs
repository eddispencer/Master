using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetPair : MonoBehaviour
{
    [SerializeField] private Transform start, end;
    [SerializeField] private new LineRenderer renderer;
    [SerializeField] private float wallThickness = 0.15f;
    
    [SerializeField] private Color defaultColor, optimalColor;

    private Evaluator evaluator;

    public void Start()
    {
        evaluator = FindObjectOfType<Evaluator>();
        evaluator.RegisterTarget(this);
        Default();
    }

    public (Vector3, Vector3) GetTargets()
    {
        return (start.position, end.position);
    }
    
    public (Vector2, Vector2) GetTargets2D()
    {
        Vector2 start2D = new(start.position.x, start.position.z);
        Vector2 end2D = new(end.position.x, end.position.z);
        return (start2D, end2D);
    }

    public void Optimal()
    {
        renderer.startColor = optimalColor;
        renderer.endColor = optimalColor;
        DisplayTarget();
    }
    
    public void Default()
    {
        renderer.startColor = defaultColor;
        renderer.endColor = defaultColor;
        DisplayTarget();
    }

    private void DisplayTarget()
    {
        var lineThickness = renderer.startWidth;
        var lineThicknessAdjustment = lineThickness / 2;
        var wallThicknessAdjustment = wallThickness / 2;
        var thicknessAdjustment = lineThicknessAdjustment + wallThicknessAdjustment;
        
        var (s, e) = GetTargets();
        var dir = (e - s).normalized;
        var oDir = Vector3.Cross(dir, Vector3.up) * thicknessAdjustment;
        var puffer = dir * lineThicknessAdjustment;
        
        renderer.positionCount = 6;
        renderer.SetPosition(0, start.position + oDir - puffer);
        renderer.SetPosition(1, end.position + oDir + puffer);
        renderer.SetPosition(2, end.position - oDir + puffer);
        renderer.SetPosition(3, start.position - oDir - puffer);
        renderer.SetPosition(4, start.position + oDir - puffer);
        renderer.SetPosition(5, end.position + oDir + puffer);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawLine(start.position, end.position);
        var lineThickness = renderer.startWidth;
        var lineThicknessAdjustment = lineThickness / 2;
        var wallThicknessAdjustment = wallThickness / 2;
        var thicknessAdjustment = lineThicknessAdjustment + wallThicknessAdjustment;
        
        var (s, e) = GetTargets();
        var dir = (e - s).normalized;
        var oDir = Vector3.Cross(dir, Vector3.up) * thicknessAdjustment;
        var puffer = dir * lineThicknessAdjustment;
        
        renderer.positionCount = 6;
        renderer.SetPosition(0, start.position + oDir - puffer);
        renderer.SetPosition(1, end.position + oDir + puffer);
        renderer.SetPosition(2, end.position - oDir + puffer);
        renderer.SetPosition(3, start.position - oDir - puffer);
        renderer.SetPosition(4, start.position + oDir - puffer);
        renderer.SetPosition(5, end.position + oDir + puffer);
    }
}
