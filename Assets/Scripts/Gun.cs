using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public static Gun instance;
    public float Speed;
    public float FireRate;
    float FireTimer;
    public GameObject BulletPrefab;
    public Transform BulletSpawner;
    public GameObject FireParticle;
    //public float Distance;
    public float gunRange;
    public GameObject Target;
    public float bulletDamage;

    private void Awake()
    {
        if (instance == null) instance = this;
    }

    void Update()
    {
        if (ArcadeVehicleController.instance.Health > 0)
        {
            FindClosestEnemy();
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            // Distance +=5;

            //   Debug.Log("Gun Range = " + Distance);
        }

    }
    void FindClosestEnemy()
    {
        float distanceClosestEnemy = Mathf.Infinity;
        ArcadeAiVehicleController closestEnemy = null;
        ArcadeAiVehicleController[] allEnemies = GameObject.FindObjectsOfType<ArcadeAiVehicleController>();
        if (allEnemies.Length != 0)
        {
            foreach (ArcadeAiVehicleController currentEnemy in allEnemies)
            {
                float distanceToEnemy = (currentEnemy.transform.position - this.transform.position).sqrMagnitude;
                if (distanceToEnemy < distanceClosestEnemy)
                {
                    distanceClosestEnemy = distanceToEnemy;
                    closestEnemy = currentEnemy;
                }
            }
            if (Vector3.Distance(transform.position, closestEnemy.transform.position) < gunRange  && closestEnemy.GetComponent<ArcadeAiVehicleController>().health>0) //ArcadeVehicleController.instance.gunRangeStart + ArcadeVehicleController.instance.GunRangeUpgrade)
            {
                FireTimer += Time.deltaTime;
                var targetRotation = Quaternion.LookRotation(closestEnemy.transform.position - transform.position);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Speed * Time.deltaTime);

                if (FireTimer > FireRate)
                {
                    FireParticle.GetComponent<ParticleSystem>().Play();
                    FireTimer = 0;
                    Instantiate(BulletPrefab, BulletSpawner.position, Quaternion.identity);
                    Target = closestEnemy.gameObject;
                }
            }

           
        }



    }

}
