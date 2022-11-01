using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCol : MonoBehaviour
{
    public Transform CarTransform;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(CarTransform.position.x, transform.position.y, CarTransform.position.z);
    }
}
