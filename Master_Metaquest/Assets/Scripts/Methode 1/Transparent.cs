using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transparent : MonoBehaviour
{
    [SerializeField] private Material mat;
    [SerializeField] private new Renderer renderer;
    [SerializeField] private Color transparent, opaque;

    public void Start()
    {
        if (!renderer)
        {
            renderer = GetComponent<Renderer>();
        }

        renderer.material = mat;
    }

    public void OnHide()
    {
        renderer.material.color = transparent;
    }
    
    public void OnShow()
    {
        renderer.material.color = opaque;
    }
}
