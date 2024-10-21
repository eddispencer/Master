using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class RayInteractor : MonoBehaviour
{
    [SerializeField] private LayerMask interactableLayer;
    [SerializeField] private LayerMask positioningLayer;
    
    [SerializeField] private ActionBasedController controller;

    private WallNode selectedNode;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (controller && controller.selectAction.action.WasPressedThisFrame())
        {
            TriggerWallNode();
        }
        if (selectedNode && controller.selectAction.action.WasReleasedThisFrame())
        {
            selectedNode.OnDeactivate();
            selectedNode = null;
        }
        RaycastHit hit;
        if (selectedNode && Physics.Raycast(transform.position, transform.forward, out hit, 10f, positioningLayer))
        {
            selectedNode.transform.position = hit.point;
        }
    }

    private void TriggerWallNode()
    {
        
        Debug.DrawRay(transform.position, transform.forward * 10f, Color.red);
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, 10f, interactableLayer))
        {
            selectedNode = hit.transform.GetComponent<WallNode>();
            selectedNode.OnActivate(controller);
            Debug.DrawRay(transform.position, transform.forward * hit.distance, Color.yellow);
        }
    }
}
