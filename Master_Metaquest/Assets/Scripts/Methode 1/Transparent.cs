using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transparent : MonoBehaviour
{
    [SerializeField] private Material transparentMat, opaqueMat;
    [SerializeField] private new Renderer renderer;
    [SerializeField] private Color transparent, opaque;

    public void Start()
    {
        if (!renderer)
        {
            renderer = GetComponent<Renderer>();
        }

        renderer.material = opaqueMat;
        renderer.material.color = opaque;
    }

    public void OnHide()
    {
        renderer.material = transparentMat;
        renderer.material.color = transparent;
    }
    
    public void OnShow()
    {
        renderer.material = opaqueMat;
        renderer.material.color = opaque;
    }
}
