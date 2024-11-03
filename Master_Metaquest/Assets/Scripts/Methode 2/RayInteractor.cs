using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class RayInteractor : MonoBehaviour
{
    [SerializeField] private FarCreationManager creationManager;
    [SerializeField] private LayerMask interactableLayer;
    [SerializeField] private LayerMask positioningLayer;
    
    [SerializeField] private ActionBasedController controller;
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private float length = 10f;
    [SerializeField] private float radius = 0.5f;
    
    [SerializeField] private Transform rayOrigin;
    
    private WallNode selectedNode;
    private WallNode hightlightedNode;
    
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
        if (controller && controller.uiPressAction.action.WasPressedThisFrame() && Physics.Raycast(GetRay(), out hit, length, positioningLayer))
        {
            creationManager.CreateNode(hit.point);
        }
        //Highlight
        if (!selectedNode)
        {
            if (hightlightedNode && hightlightedNode?.gameObject)
            {
                hightlightedNode?.OnDeselect();
            }
            var ray = GetRay();
            var hightlightHits = Physics.SphereCastAll(ray, radius, length, interactableLayer);
            WallNode bestNode = null;
            float optimalHit = 0;
            foreach (var interactable in hightlightHits)
            {
                var node = interactable.transform.GetComponent<WallNode>();
                if (!node.isGrabbed)
                {
                    var nodeDir = (node.transform.position - ray.origin).normalized;
                    var dist = Vector3.Dot(ray.direction, nodeDir);
                    if (dist > optimalHit)
                    {
                        optimalHit = dist;
                        bestNode = node;
                    }
                }
            }

            if (bestNode)
            {
                bestNode.OnSelect();
                hightlightedNode = bestNode;
            }
        }
    }

    private void TriggerWallNode()
    {
        var ray = GetRay();
        var hits = Physics.SphereCastAll(ray, radius, length, interactableLayer);
        WallNode bestNode = null;
        float optimalHit = 0;
        foreach (var hit in hits)
        {
            var node = hit.transform.GetComponent<WallNode>();
            if (!node.isGrabbed)
            {
                var nodeDir = (node.transform.position - ray.origin).normalized;
                var dist = Vector3.Dot(ray.direction, nodeDir);
                if (dist > optimalHit)
                {
                    optimalHit = dist;
                    bestNode = node;
                }
            }
        }
        if (bestNode)
        {
            selectedNode = bestNode;
            selectedNode.OnActivate(controller);
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
