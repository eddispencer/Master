using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class GrabRotation : MonoBehaviour
{
    [SerializeField] private Transform proxy;

    [SerializeField] private Sides grab = Sides.RIGHT;

    private GameObject anchor;

    private bool isRotationActive = false;
    private Vector3 offset = Vector3.zero;

    private ActionBasedController controller;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isRotationActive && proxy)
        {
            Rotate();
            if (controller && controller.selectAction.action.WasReleasedThisFrame()) {
                OnDeactivate();
            }
        }
    }

    public void OnDestroy()
    {
        OnDeactivate();
    }

    public void OnActivate(Transform interactor)
    {
        proxy = interactor.transform;
        controller = interactor.transform.gameObject.GetComponent<ActionBasedController>();
        var dir = Vector3.Dot(transform.right, (proxy.position - transform.position));
        grab = dir <= 0 ? Sides.LEFT : Sides.RIGHT;
        SetAnchor();
        isRotationActive = true;
        
        offset = transform.position + (transform.rotation * Vector3.Scale(getLocalDir(), transform.localScale / 2f)) - proxy.position;
    }
    
    public void OnDeactivate()
    {
        isRotationActive = false;
        controller = null;
        ReleaseAnchor();
    }

    [ContextMenu("Set Anker")]
    public void SetAnchor()
    {
        anchor = new GameObject("Anchor");
        anchor.transform.position = transform.position + (transform.rotation * Vector3.Scale(-getLocalDir(), transform.localScale / 2f));
        anchor.transform.forward = getDir();
        transform.parent = anchor.transform;
    }
    
    [ContextMenu("Release Anker")]
    public void ReleaseAnchor()
    {
        transform.parent = null;
        Destroy(anchor);
        anchor = null;
    }

    [ContextMenu("Rotate")]
    public void Rotate()
    {
        RotateWall(proxy.position + offset);
        
    }

    private void RotateWall(Vector3 grabPos)
    {
        var rotPos = new Vector3(grabPos.x, anchor.transform.position.y, grabPos.z);
        anchor.transform.LookAt(rotPos, Vector3.up);

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
