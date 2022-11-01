using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OurParts : MonoBehaviour
{
   
   public bool Grounded;
    public float RandomX, RandomY, RandomZ;
    void Start()
    {
       
       
        transform.parent = null;
        gameObject.AddComponent<Rigidbody>();
        gameObject.AddComponent<BoxCollider>();
        GetComponent<Rigidbody>().AddForce(Random.Range(-300, 300), Random.Range(300, 500), Random.Range(-300, 300));
       

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!Grounded)
        {
            transform.Rotate(RandomX, RandomY, RandomZ);
        }

       
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            Grounded = true;
            RandomX = Random.Range(-10f, 10f);
            RandomY = Random.Range(-10f, 10f);
            RandomZ = Random.Range(-10f, 10f);
        }
    }
}
