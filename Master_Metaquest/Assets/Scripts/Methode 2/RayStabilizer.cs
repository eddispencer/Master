using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayStabilizer : MonoBehaviour
{
    [SerializeField] private Transform ray;
    [SerializeField] private float moveFar = 10, moveNear = 2, rotFar = 5, rotNear = 0.5f;
    [SerializeField] private float moveBreakpoint = 0.05f, moveBreakpoint2 = 0.1f, rotBreakpoint = 0.1f, rotBreakpoint2 = 0.5f;
    [SerializeField] private float maxInteractionDistance = 10f;
    [SerializeField] private LayerMask interactionLayer;

    private float relativeInteractionDistance = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GetInteractionDist();
        LerpMove();
        LerpRot();
    }

    private void LerpMove()
    {
        var target = ray.position;
        var pos = transform.position;
        var dir = target - pos;


        var modi = Mathf.InverseLerp(moveBreakpoint, moveBreakpoint2, dir.magnitude);
        var lerp = Mathf.Lerp(Mathf.Lerp(moveNear, 0.5f, relativeInteractionDistance), moveFar, modi);

        transform.position += dir * (lerp);

    }
    
    private void LerpRot()
    {
        var target = ray.rotation;
        var rot = transform.rotation;

        var rotDif = target * Vector3.forward - rot * Vector3.forward;
        
        var modi = Mathf.InverseLerp(rotBreakpoint, rotBreakpoint2, rotDif.magnitude);
        var lerp = Mathf.Lerp(Mathf.Lerp(rotNear, 0.1f, relativeInteractionDistance), rotFar, modi);

        transform.rotation = Quaternion.Slerp(rot, target, lerp);

    }

    private void GetInteractionDist()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, maxInteractionDistance, interactionLayer))
        {
            relativeInteractionDistance = hit.distance / maxInteractionDistance;
        }
        else
        {
            relativeInteractionDistance = 1;
        }
    }
    
}
