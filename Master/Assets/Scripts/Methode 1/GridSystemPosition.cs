using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSystemPosition : MonoBehaviour
{
    [SerializeField]
    private float stepSize = 1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = transform.position;
        Vector3 correctedPos = new Vector3(Mathf.Round(pos.x / stepSize) * stepSize, pos.y, Mathf.Round(pos.z / stepSize) * stepSize);

        transform.position = correctedPos;
    }
}
