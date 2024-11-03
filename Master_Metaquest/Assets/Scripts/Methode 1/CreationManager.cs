using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class CreationManager : MonoBehaviour
{
    [SerializeField] private ActionBasedController left, right;
    [SerializeField] private Transform leftProxy, rightProxy;

    [SerializeField] private float activationDistance = 0.1f;

    [SerializeField] private GameObject wallPrefab;
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CheckHand(left, right);
        CheckHand(right, left);
    }

    private void CheckHand(ActionBasedController controller, ActionBasedController other)
    {
        var handDistVec = leftProxy.position - rightProxy.position;
        if (Vector3.Magnitude(handDistVec) <= activationDistance && controller.activateAction.action.WasPressedThisFrame())
        {
            CreateWall(rightProxy.position + handDistVec / 2, controller, -handDistVec);
        }
    }

    private void CreateWall(Vector3 pos, ActionBasedController controller, Vector3 wallRight)
    {
        wallRight.y = 0;
        
        var gScale = Instantiate(wallPrefab, pos, Quaternion.identity).GetComponent<GrabSkalierung>();
        Vector3 wallPos = gScale.transform.position;
        gScale.transform.position = new Vector3(wallPos.x, gScale.transform.localScale.y / 2f, wallPos.z);
        gScale.transform.right = wallRight.normalized;
        gScale.OnActivate(controller.transform);
    }
}
