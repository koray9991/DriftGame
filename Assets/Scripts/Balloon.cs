using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balloon : MonoBehaviour
{
    public GameObject particle;
   

    private void Start()
    {
        transform.parent.GetComponent<Animator>().SetFloat("multiplier", Random.Range(0.5f,2f));
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            particle.GetComponent<ParticleSystem>().Play();
            ArcadeVehicleController.instance.moneyCount += 1000;
            ArcadeVehicleController.instance.moneyText.text = ArcadeVehicleController.instance.moneyCount.ToString();
            GameManager.instance.explodedBalloons += 1;
            GameManager.instance.ballonText.text = GameManager.instance.explodedBalloons + "/" + GameManager.instance.balloonCount;
            Destroy(gameObject);

        }
    }
}
