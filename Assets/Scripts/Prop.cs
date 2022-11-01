using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prop : MonoBehaviour
{
    public ParticleSystem particle;
    private void Start()
    {
        particle = transform.GetChild(0).GetComponent<ParticleSystem>();
    }

}
