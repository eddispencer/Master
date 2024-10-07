using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(1000)]
public class GridSystemRotation : MonoBehaviour
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
        Vector3 rotation = transform.rotation.eulerAngles ;
        Vector3 correctedRotation = new Vector3(rotation.x, Mathf.Round(rotation.y / stepSize) * stepSize, rotation.z);

        transform.rotation = Quaternion.Euler(correctedRotation);
    }
}
