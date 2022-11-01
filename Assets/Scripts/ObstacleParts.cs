using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleParts : MonoBehaviour
{
    public float randomX, randomY, randomZ;
    public bool grounded;
    public float timer;
    void Start()
    {
        transform.parent = null;
        randomX = Random.Range(-4f, 4f);
        randomY = Random.Range(-4f, 4f);
        randomZ = Random.Range(-4f, 4f);
        Destroy(gameObject, 5f);
    }

    
    void FixedUpdate()
    {
        //timer += Time.deltaTime;
        ////if (timer > 3)
        ////{
        ////    grounded = true;
        ////}
        if (!grounded)
        {
            transform.Rotate(randomX, randomY, randomZ);
        }
        
      
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            grounded = true;

        }
    }
   
}
