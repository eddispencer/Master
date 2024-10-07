using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using CommonUsages = UnityEngine.XR.CommonUsages;

public class GrabSkalierung : MonoBehaviour
{
    
    [SerializeField] private Transform proxy;

    [SerializeField] private Sides grab = Sides.RIGHT;

    private Vector3 anchor;
    private Vector3 offset = Vector3.zero;

    private bool isScaleActive = false;

    private ActionBasedController controller;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isScaleActive && proxy)
        {
            Scale();
            bool p;
            if (controller && controller.activateAction.action.WasReleasedThisFrame()) {
                OnDeactivate();
            }
        }
    }

    public void OnActivate(ActivateEventArgs args)
    {
        proxy = args.interactorObject.transform;
        controller = args.interactorObject.transform.gameObject.GetComponent<ActionBasedController>();
        var dir = Vector3.Dot(transform.right, (proxy.position - transform.position));
        grab = dir <= 0 ? Sides.LEFT : Sides.RIGHT;
        SetAnchor();
        isScaleActive = true;

        offset = transform.position + (transform.rotation * Vector3.Scale(getLocalDir(), transform.localScale / 2f)) - proxy.position;
    }
    
    public void OnDeactivate()
    {
        isScaleActive = false;
        controller = null;
        ReleaseAnchor();
    }

    [ContextMenu("Set Anker")]
    public void SetAnchor()
    {
        anchor = transform.position + (transform.rotation * Vector3.Scale(-getLocalDir(), transform.localScale / 2f));
    }
    
    [ContextMenu("Release Anker")]
    public void ReleaseAnchor()
    {
        anchor = Vector3.zero;
    }

    [ContextMenu("Scale")]
    public void Scale()
    {
        ScaleWall(proxy.position + offset);
    }

    private void ScaleWall(Vector3 grabPos)
    {
        Vector3 dir = grabPos - anchor;
        Vector3 projectedDir = Vector3.Project(dir, getDir());
        float length = projectedDir.magnitude;

        Vector3 oldScale = transform.localScale;
        transform.localScale = new Vector3(length, oldScale.y, oldScale.z);
        transform.position = anchor + getDir() * length / 2f;

    }

    private Vector3 getDir()
    {
        return grab == Sides.LEFT ? -transform.right : transform.right;
    }
    
    private Vector3 getLocalDir()
    {
        return grab == Sides.LEFT ? Vector3.left : Vector3.right;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, transform.right * 3f);
    }

    enum Sides
    {
        LEFT,
        RIGHT
    }
}
