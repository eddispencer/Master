using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NearWallVisualizer : MonoBehaviour
{
    [SerializeField] private DualGrab dualGrab;
    [SerializeField] private GrabSkalierung grabSkalierung;
    [SerializeField] private WallVisualizer wallVisualizer;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Render();
    }

    public void Render()
    {
        float length = transform.localScale.x;
        bool toSmall = length <= grabSkalierung.sizeToDestroy;
        if (toSmall)
        {
            wallVisualizer.OnDeletion();
        } else if (dualGrab.isGrabbing)
        {
            wallVisualizer.OnManipulate();
        }
        else
        {
            wallVisualizer.OnIdle();
        }
    }
}
