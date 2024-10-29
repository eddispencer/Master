using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class RayInteractor : MonoBehaviour
{
    [SerializeField] private LayerMask interactableLayer;
    [SerializeField] private LayerMask positioningLayer;
    
    [SerializeField] private ActionBasedController controller;
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private float length = 10f;
    [SerializeField] private float radius = 0.5f;
    
    [SerializeField] private Transform rayOrigin;
    
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
        if (selectedNode && Physics.Raycast(GetRay(), out hit, length, positioningLayer))
        {
            selectedNode.transform.position = hit.point;
        }
    }

    private void TriggerWallNode()
    {
        var hits = Physics.SphereCastAll(GetRay(), radius, length, interactableLayer);
        foreach (var hit in hits)
        {
            selectedNode = hit.transform.GetComponent<WallNode>();
            selectedNode.OnActivate(controller);
            return;
        }
    }

    private Ray GetRay()
    {
        var start = rayOrigin.position;
        var dir = rayOrigin.forward;
        return new Ray(start, dir);
    }
    
    private Ray GetRayForRenderer()
    {
        var start = Vector3.zero;
        var dir = Vector3.forward;
        return new Ray(start, dir);
    }
}
