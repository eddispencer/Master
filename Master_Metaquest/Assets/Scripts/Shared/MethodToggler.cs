using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MethodToggler : MonoBehaviour
{
    [SerializeField] private MethodManager methodManagerRef;
    [SerializeField] private UnityEvent onUseNear, onUseFar;
    
    // Start is called before the first frame update
    void Start()
    {
        if (methodManagerRef.useMethodNear)
        {
            onUseNear.Invoke();
        }
        if (methodManagerRef.useMethodFar)
        {
            onUseFar.Invoke();
        }
    }
}
