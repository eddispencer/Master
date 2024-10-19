using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;

public class DualGrab : MonoBehaviour
{
    public UnityEvent onDualGrab;
    public UnityEvent onDualGrabExit;

    private List<ActionBasedController> controllers = new List<ActionBasedController>();

    private bool isGrabbing = false;
    private Vector3 offset = Vector3.zero;
    private GameObject anchor;
    
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (controllers.Count > 0)
        {
            var controllerList = new List<ActionBasedController>(controllers);
            foreach (var controller in controllerList)
            {
                if (controller && controller.selectAction.action.WasReleasedThisFrame()) {
                    OnDeactivate(controller);
                }
            }
        }

        if (isGrabbing)
        {
            PositionWall();
        }
    }
    
    public void OnActivate(Transform interactor)
    {
        controllers.Add(interactor.transform.gameObject.GetComponent<ActionBasedController>());
    }
    
    public void OnDeactivate(ActionBasedController deactivatedController)
    {
        controllers.Remove(deactivatedController);
        if (controllers.Count < 2)
        {
            ReleaseAnchor();
            isGrabbing = false;
            onDualGrabExit.Invoke();
        }
    }

    public bool IsDualGrab()
    {
        bool grabbed = controllers.Count == 2;
        if (!grabbed) return false;

        bool isDualGrab = true;
        foreach (var controller in controllers)
        {
            if (controller.TryGetComponent<SingleHandInteractor>( out SingleHandInteractor activeInteractor))
            {
                isDualGrab = isDualGrab && activeInteractor.GetCollider().Any(c => c.transform == transform);
            }
        }

        return isDualGrab;
    }
    
    public void StartDualGrab()
    {
        onDualGrab.Invoke();
        isGrabbing = true;
        SetAnchor();
        
    }
    
    public void SetAnchor()
    {
        Vector3 grabDir = controllers[0].transform.position - controllers[1].transform.position;
        Vector3 grabCenter = controllers[1].transform.position + (grabDir)/2f;

        grabDir.y = 0;
        grabDir = grabDir.normalized;

        offset = transform.position - grabCenter;
        anchor = new GameObject("Anchor");
        anchor.transform.position = transform.position;
        anchor.transform.forward = grabDir;
        transform.parent = anchor.transform;
    }
    
    public void ReleaseAnchor()
    {
        transform.parent = null;
        Destroy(anchor);
        anchor = null;
        offset = Vector3.zero;
    }

    private void PositionWall()
    {
        Vector3 grabDir = controllers[0].transform.position - controllers[1].transform.position;
        Vector3 grabCenter = controllers[1].transform.position + (grabDir)/2f;

        grabDir.y = 0;
        grabDir = grabDir.normalized;

        var newPos = grabCenter + offset;

        anchor.transform.position = new Vector3(newPos.x, anchor.transform.position.y, newPos.z);
        anchor.transform.forward = grabDir;

    }
}
