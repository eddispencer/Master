using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class GridSystemScale : MonoBehaviour
{
    [SerializeField]
    private float stepSize = 1;
    [SerializeField]
    private Transform parent;

    public enum Direction
    {
        Left,
        Right
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void scaleWall(Vector3 interactorPos, Direction direction)
    {
        switch (direction)
        {
            case Direction.Left : scaleLeftWall(interactorPos);
                break;
            case Direction.Right : scaleRightWall(interactorPos);
                break;
        }

        
    }

    public void scaleLeftWall(Vector3 handPos)
    {
        Vector3 pos = transform.position;
        float distance = pos.x - handPos.x;
        
        float correctedScale = Mathf.Round(distance / stepSize) * stepSize;
        Vector3 localScale = transform.localScale;

        transform.localScale = new Vector3(correctedScale + localScale.x, localScale.y, localScale.z);
        parent.position += Vector3.left * correctedScale;
        
        
    }
    
    public void scaleRightWall(Vector3 handPos)
    {
        Vector3 pos = transform.position;
        float distance = Mathf.Abs(handPos.x-pos.x);
        
        float correctedScale = Mathf.Round(distance / stepSize) * stepSize;
        Vector3 localScale = transform.localScale;

        transform.localScale = new Vector3(correctedScale, localScale.y, localScale.z);
    }
}
