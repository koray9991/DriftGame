using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class FuelImg : MonoBehaviour
{
    float fill=1;
    private void FixedUpdate()
    {
        fill = GetComponent<Image>().fillAmount;
        GetComponent<Image>().color = new Color(1-fill,  fill, 0, 1);
    }
}
