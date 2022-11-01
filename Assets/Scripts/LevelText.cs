using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;
using TMPro;
public class LevelText : MonoBehaviour
{
    float alpha;
    float timer;


    private void Start()
    {
        GetComponent<TextMeshProUGUI>().text = "LEVEL " + (SceneManager.GetActiveScene().buildIndex + 1).ToString();
    }
    void FixedUpdate()
    {
        GetComponent<TextMeshProUGUI>().color = new Color(1, 1, 1, alpha);
        timer += Time.deltaTime;
        if (timer < 3)
        {
            alpha += 0.01f;
        }
        if(timer>3 && timer < 6)
        {
            alpha -= 0.01f;
        }
        if (timer > 6)
        {
            GetComponent<LevelText>().enabled = false;
        }
    }
}
