using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MethodManager", menuName = "Manager/MethodManager", order = 1)]
public class MethodManager : ScriptableObject
{
    [SerializeField] public bool useMethodNear;
    [SerializeField] public bool useMethodFar;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
