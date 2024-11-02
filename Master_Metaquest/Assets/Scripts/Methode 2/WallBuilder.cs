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
            HighlightBeforeDelete();
        }
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
        if (wasUpdate)
        {
            if (isManipulated)
            {
                onActivate.Invoke();
            }
            else
            {
                onDeactivate.Invoke();
                visualizer.OnIdle();
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

    private void HighlightBeforeDelete()
    {
        if (IsDeletionImminent())
        {
            visualizer.OnDeletion();
        }
        else
        {
            visualizer.OnManipulate();
        }
    }

    private bool IsDeletionImminent()
    {
        var dist = end.transform.position - start.transform.position;
        return dist.magnitude <= deletionDistance;
    }
}
