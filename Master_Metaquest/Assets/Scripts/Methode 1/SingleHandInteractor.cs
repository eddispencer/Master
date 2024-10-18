using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SingleHandInteractor : MonoBehaviour
{
    
    [SerializeField]
    private ActionBasedController controller;

    [SerializeField] private float grabRadius = 0.1f;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (controller && controller.activateAction.action.WasPressedThisFrame())
        {
            CheckInteractablesScale();
        }
        
        if (controller && controller.selectAction.action.WasPressedThisFrame())
        {
            if (!CheckInteractablesDualGrab())
            {
                CheckInteractablesRotate();
            }
        }
    }

    private void CheckInteractablesScale()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, grabRadius);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.TryGetComponent<GrabSkalierung>(out GrabSkalierung scaler)) {
                scaler.OnActivate(controller.transform);
                return;
            }
        }
    }
    
    private void CheckInteractablesRotate()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, grabRadius);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.TryGetComponent<GrabRotation>(out GrabRotation rotator)) {
                rotator.OnActivate(controller.transform);
                return;
            }
        }
    }
    
    private bool CheckInteractablesDualGrab()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, grabRadius);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.TryGetComponent<DualGrab>(out DualGrab manager))
            {
                manager.OnActivate(controller.transform);
                if (manager.IsDualGrab())
                {
                    print("Switch to Two Hands Mode!");
                    manager.StartDualGrab();
                    return true;
                }
            }
        }
        return false;
    }

    public Collider[] GetCollider()
    {
        return Physics.OverlapSphere(transform.position, grabRadius);
    }
}
