using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSystemScale : MonoBehaviour
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
        Vector3 scale = transform.localScale;
        Vector3 correctedScale = new Vector3(scale.x, scale.y, Mathf.Round(scale.z / stepSize) * stepSize);
        Vector3 correctedPosition = new Vector3(rotation.x, Mathf.Round(rotation.y / stepSize) * stepSize, rotation.z);

        transform.rotation = Quaternion.Euler(correctedRotation);
    }
}
