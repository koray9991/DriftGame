using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoliceLights : MonoBehaviour
{
    public GameObject Light1, Light2;
    public float timer;
    void Start()
    {
        timer = Random.Range(0f, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer < 0.5f)
        {
            Light1.SetActive(true);
            Light2.SetActive(false);
        }
        if (timer > 0.5f)
        {
            Light1.SetActive(false);
            Light2.SetActive(true);
        }
        if (timer > 1)
        {
            timer = 0;
        }
    }
}
