using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RealRot : MonoBehaviour
{
    public float X, Y, Z;
    private void FixedUpdate()
    {
        transform.Rotate(X, Y, Z);
    }
}
