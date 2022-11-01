using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasFollow : MonoBehaviour
{
    public Transform Car;
    public float Xindex, Yindex, Zindex;
    void Start()
    {

    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = new Vector3(Car.transform.position.x + Xindex, Car.transform.position.y + Yindex, Car.transform.position.z + Zindex);
    }
}
