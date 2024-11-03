using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WallBuilder : MonoBehaviour
{
    
    [SerializeField] private WallVisualizer visualizer;
    [SerializeField] private Transform wallModel;
    [SerializeField] public WallNode start, end;
    
    [SerializeField] private float deletionDistance = 0.2f;
    
    public UnityEvent onActivate;
    public UnityEvent onDeactivate;

    private bool isManipulated = false;
    private bool isSelected = false;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isManipulated)
        {
            BuildWall();
        }
        HighlightBeforeDelete();
    }

    [ContextMenu("Build Wall")]
    public void BuildWall()
    {
        var dist = end.transform.position - start.transform.position;
        var pos = start.transform.position + dist / 2;
        var orientation = dist.normalized;
        pos.y = wallModel.position.y;
        
        wallModel.position = pos;
        wallModel.localScale = new Vector3(dist.magnitude, wallModel.localScale.y, wallModel.localScale.z);
        wallModel.right = orientation;
    }

    public void OnUpdate()
    {
        var wasUpdate = isManipulated != IsManipulated();
        isManipulated = IsManipulated();
        isSelected = IsSelected();
        if (wasUpdate)
        {
            if (!isManipulated)
            {
                if (IsDeletionImminent())
                {
                    Destroy(gameObject);
                }
            }
        }
    }

    private bool IsManipulated()
    {
        return start.isGrabbed || end.isGrabbed;
    }
    
    private bool IsSelected()
    {
        return start.isSelected || end.isSelected;
    }

    private void HighlightBeforeDelete()
    {
        if (IsDeletionImminent())
        {
            visualizer.OnDeletion();
        }
        else if (isManipulated || isSelected)
        {
            visualizer.OnManipulate();
        }
        else
        {
            visualizer.OnIdle();
        }
    }

    private bool IsDeletionImminent()
    {
        var dist = end.transform.position - start.transform.position;
        return dist.magnitude <= deletionDistance;
    }
}
