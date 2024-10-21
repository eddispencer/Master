using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WallBuilder : MonoBehaviour
{
    [SerializeField] private Transform wallModel;
    [SerializeField] private Transform start, end;
    
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
        }
    }

    [ContextMenu("Build Wall")]
    public void BuildWall()
    {
        var dist = end.position - start.position;
        var pos = start.position + dist / 2;
        var orientation = dist.normalized;
        pos.y = wallModel.position.y;
        
        wallModel.position = pos;
        wallModel.localScale = new Vector3(dist.magnitude, wallModel.localScale.y, wallModel.localScale.z);
        wallModel.right = orientation;
    }
    
    public void OnActivate()
    {
        isManipulated = true;
        onActivate.Invoke();
    }
    
    public void OnDeactivate()
    {
        isManipulated = false;
        onDeactivate.Invoke();
    }
}
