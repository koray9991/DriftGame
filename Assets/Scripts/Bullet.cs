using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Gun GunScript;
    public GameObject TargetObject;
    public float Speed;
    
    void Start()
    {
        GunScript = GameObject.FindGameObjectWithTag("Gun").GetComponent<Gun>();
        TargetObject = GunScript.Target;
        Destroy(gameObject, 0.4f);

    }

    // Update is called once per frame
    void Update()
    {
        if (TargetObject != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, TargetObject.transform.position, Speed);
        }

    }
}
