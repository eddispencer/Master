using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallVisualizer : MonoBehaviour
{
    [SerializeField] private new Renderer renderer;
    [SerializeField] private Material transparentMat, opaqueMat, deletionMat;
    [SerializeField] private Color transparent, opaque, delete;
    
    public void Start()
    {
        if (!renderer)
        {
            renderer = GetComponent<Renderer>();
        }

        OnIdle();
    }

    public void OnManipulate()
    {
        renderer.material = transparentMat;
        renderer.material.color = transparent;
    }
    
    public void OnIdle()
    {
        renderer.material = opaqueMat;
        renderer.material.color = opaque;
    }
    
    public void OnDeletion()
    {
        renderer.material = deletionMat;
        renderer.material.color = delete;
    }
}