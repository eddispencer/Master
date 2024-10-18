using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;

public class DualGrab : MonoBehaviour
{
    [SerializeField] private XRGrabInteractable grabInteractable;
    public UnityEvent onDualGrab;
    public UnityEvent onDualGrabExit;

    private bool isGrabbed;
    private ActionBasedController controllerOld;
    private List<ActionBasedController> controllers = new List<ActionBasedController>();

    private Material mat;
    
    // Start is called before the first frame update
    void Start()
    {
        mat = GetComponent<Renderer>().material;
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
            grabInteractable.enabled = false;
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
        grabInteractable.enabled = true;
        foreach (var controller in controllers)
        {
            var interactor = controller.GetComponent<XRDirectInteractor>();
            grabInteractable.interactionManager.SelectEnter(interactor, grabInteractable);
        }
    }
}
