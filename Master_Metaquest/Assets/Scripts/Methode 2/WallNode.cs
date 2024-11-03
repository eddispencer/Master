using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class WallNode : MonoBehaviour
{
    [SerializeField] private WallBuilder builder;
    public bool isGrabbed = false;
    public bool isSelected = false;
    [SerializeField] private Highlighter highlighter;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    [ContextMenu("Activate")]
    public void OnActivate(ActionBasedController c)
    {
        isGrabbed = true;
        builder.OnUpdate();
        OnSelect();
    }
    
    [ContextMenu("Deactivate")]
    public void OnDeactivate()
    {
        isGrabbed = false;
        builder.OnUpdate();
        OnDeselect();
    }

    public void OnSelect()
    {
        isSelected = true;
        highlighter.OnHighlight();
        builder.OnUpdate();
    }
    
    public void OnDeselect()
    {
        isSelected = false;
        highlighter.OnDeactivate();
        builder.OnUpdate();
    }
}
