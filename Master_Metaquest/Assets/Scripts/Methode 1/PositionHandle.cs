using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class PositionHandle : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void positionHandle(SelectEnterEventArgs args)
    {
        Vector3 handPos = args.interactorObject.transform.position;

        transform.position = handPos;
    }
}
