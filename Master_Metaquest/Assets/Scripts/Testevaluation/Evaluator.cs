using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class Evaluator : MonoBehaviour
{
    [SerializeField] private TMP_Text wallCountText;
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text timeText;
    
    [SerializeField] private List<SourcePair> sources = new();
    [SerializeField] private List<TargetPair> targets = new();

    [SerializeField] private UnityEvent onScoreComplete = new();
    
    
    [SerializeField] private float threshold = 0.1f;
    [SerializeField] private float multiThreshold = 0.1f;

    [SerializeField] private bool applyThreshold = false;

    private void Update()
    {
        wallCountText.text = sources.Count + "/" + targets.Count + " Walls";
        if (applyThreshold)
        {
            OnCalcSquareDist();
        }
    }

    public void RegisterSource(SourcePair source)
    {
        sources.Add(source);
    }
    
    public void RegisterTarget(TargetPair target)
    {
        targets.Add(target);
    }
    
    public void UnregisterSource(SourcePair source)
    {
        sources.Remove(source);
    }
    
    public void UnregisterTarget(TargetPair target)
    {
        targets.Remove(target);
    }

    private float CalcSquareDist(Vector3 source, Vector3 target)
    {
        return Vector3.SqrMagnitude(target - source) * 1000;
    }
    
    private float CalcSquareDist(SourcePair sourcePair, TargetPair targetPair)
    {
        var (sStart, sEnd) = sourcePair.GetSources2D();
        var (tStart, tEnd) = targetPair.GetTargets2D();

        var distSquared = CalcSquareDist(sStart, tStart) + CalcSquareDist(sEnd, tEnd);
        var distSquaredSwapped = CalcSquareDist(sStart, tEnd) + CalcSquareDist(sEnd, tStart);

        return distSquared > distSquaredSwapped ? distSquaredSwapped : distSquared;
    }

    private (float,TargetPair) CalcSquareDist(SourcePair sourcePair, List<TargetPair> targetPairs)
    {
        float bestDist = float.MaxValue;
        TargetPair bestPair = null;
        foreach (var targetPair in targetPairs)
        {
            var dist = CalcSquareDist(sourcePair, targetPair);
            if (dist < bestDist)
            {
                bestDist = dist;
                bestPair = targetPair;
            }
        }

        return (bestDist, bestPair);
    }
    
    private float CalcSquareDist()
    {
        float distScore = 0;
        List<TargetPair> targetsCopy = new List<TargetPair>(targets);
        
        foreach (var sourcePair in sources)
        {
            var (dist, usedTarget) = CalcSquareDist(sourcePair, targetsCopy);
            targetsCopy.Remove(usedTarget);
            distScore += dist;
            if (applyThreshold)
            {
                if (dist <= threshold)
                {
                    usedTarget.Optimal();
                }
                else
                {
                    usedTarget.Default();
                }
            }
        }

        return distScore;
    }
    
    public void OnCalcSquareDist()
    {
        if (sources.Count != targets.Count)
        {
            return; // Do something else
        }
        CalcSquareDist();
    }
    
    [ContextMenu("Calculate Score")]
    public void OnCalcScore()
    {
        if (sources.Count != targets.Count)
        {
            return; // Do something else
        }

        var score = CalcSquareDist();
        if (applyThreshold && score > multiThreshold)
        {
            return;
        }
        scoreText.text = $"Alpha: {score:f}";
        timeText.text = $"Beta: {Time.timeSinceLevelLoad:f}";
        onScoreComplete.Invoke();
    }

    public bool IsReadyForEvaluation()
    {
        return sources.Count == targets.Count;
    }
}
