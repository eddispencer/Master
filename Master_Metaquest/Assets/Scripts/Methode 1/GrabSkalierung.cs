using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using CommonUsages = UnityEngine.XR.CommonUsages;

public class GrabSkalierung : MonoBehaviour
{
    
    [SerializeField] private Transform proxy;

    [SerializeField] private Sides grab = Sides.RIGHT;
    [SerializeField] private float sizeToDestroy = 0.1f;

    private Vector3 anchor;
    private Vector3 offset = Vector3.zero;

    private bool isScaleActive = false;

    private ActionBasedController controller;
    
    private bool isDisabled = false;
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
            if (controller && controller.activateAction.action.WasReleasedThisFrame()) {
                OnDeactivate();
                DestroyWhenSmall();
            }
        }
    }
    
    public void OnActivate(Transform interactor)
    {
        if (isDisabled) return;
        
        proxy = interactor.transform;
        controller = interactor.transform.gameObject.GetComponent<ActionBasedController>();
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

    public void OnDisable()
    {
        OnDeactivate();
        isDisabled = true;
    }
    
    public void OnEnable()
    {
        isDisabled = false;
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

    private void DestroyWhenSmall()
    {
        float length = transform.localScale.x;
        if (length <= sizeToDestroy)
        {
            Destroy(gameObject);
        }
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
