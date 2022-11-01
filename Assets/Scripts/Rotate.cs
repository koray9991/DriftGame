using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    ArcadeVehicleController OurCar;
    public bool Grounded;
    public float RandomX,RandomY,RandomZ;
    void Start()
    {
        // Destroy(gameObject, 3f);
        OurCar = GameObject.FindGameObjectWithTag("Player").GetComponent<ArcadeVehicleController>();
        gameObject.layer = 6;
        transform.parent = null;
        gameObject.AddComponent<Rigidbody>();
        gameObject.AddComponent<BoxCollider>();
        GetComponent<Rigidbody>().AddForce(Random.Range(-300, 300), Random.Range(300, 500), Random.Range(-300, 300));
        if (transform.tag == "PoliceLight")
        {
            transform.GetChild(0).GetComponent<Light>().enabled = false;
            transform.GetChild(1).GetComponent<Light>().enabled = false;
        }
       // Destroy(GetComponent<Rigidbody>());
       
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!Grounded)
        {
            transform.Rotate(RandomX,RandomY,RandomZ);
        }
      
        if (Vector3.Distance(transform.position, OurCar.transform.position) > 100)
        {
            Destroy(gameObject);
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
