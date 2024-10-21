using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class WallNode : MonoBehaviour
{
    [SerializeField] private WallBuilder builder;
    
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
        builder.OnActivate();
    }
    
    [ContextMenu("Deactivate")]
    public void OnDeactivate()
    {
        builder.OnDeactivate();
    }
}
