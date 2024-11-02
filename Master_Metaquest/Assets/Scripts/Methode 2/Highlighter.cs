using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Highlighter : MonoBehaviour
{
    [SerializeField] private new Renderer renderer;
    [SerializeField] private Material defaultMat, highlightMat;
    [SerializeField] private Color defaultColor, highlightColor;

    [SerializeField] private float duration = 0.2f;
    private float deactivationTimestamp;
    private bool isActive;
    
    // Start is called before the first frame update
    void Start()
    {
        Deactivate();
    }

    // Update is called once per frame
    void Update()
    {
        if (isActive && Time.time > deactivationTimestamp)
        {
            Deactivate();
        }
    }

    public void OnHighlight()
    {
        deactivationTimestamp = Time.time + duration;
        renderer.material = highlightMat;
        renderer.material.color = highlightColor;
        isActive = true;
    }
    
    private void Deactivate()
    {
        renderer.material = defaultMat;
        renderer.material.color = defaultColor;
        isActive = false;
    }
}
