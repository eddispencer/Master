using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Highlighter : MonoBehaviour
{
    [SerializeField] private new Renderer renderer;
    [SerializeField] private Material defaultMat, highlightMat;
    [SerializeField] private Color defaultColor, highlightColor;
    
    // Start is called before the first frame update
    void Start()
    {
        OnDeactivate();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void OnHighlight()
    {
        renderer.enabled = true;
        renderer.material = highlightMat;
        renderer.material.color = highlightColor;
    }

    public void OnDeactivate()
    {
        renderer.enabled = false;
        renderer.material = defaultMat;
        renderer.material.color = defaultColor;
    }
}
