using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Tutorial : MonoBehaviour
{
    float timer;
    public float firstTouchTime, secondTouchTime;
    bool firstStop, secondStop;
    public GameObject turnRightHand, turnRightImage,turnLeftHand,turnLeftImage;
    public GameObject rightButton,leftButton;
  
    private void FixedUpdate()
    {
        timer += Time.deltaTime;
        if (timer > firstTouchTime && !firstStop)
        {
            Time.timeScale = 0;
            firstStop = true;
        }
        if(timer>secondTouchTime && !secondStop)
        {
            Time.timeScale = 0;
            secondStop = true;
        }
        if(firstStop && !secondStop)
        {
            ArcadeVehicleController.instance.horizontalInput = 1;
            
        }
       else if(firstStop && secondStop)
        {
           
            ArcadeVehicleController.instance.horizontalInput = -1;
           

        }
        
        if(firstStop && !secondStop && Time.timeScale == 0)
        {
            turnRightHand.SetActive(true);
            turnRightImage.SetActive(true);
            rightButton.GetComponent<Animator>().enabled = true;
        }
        if(firstStop && secondStop && Time.timeScale == 0)
        {
            turnLeftHand.SetActive(true);
            turnLeftImage.SetActive(true);
            leftButton.GetComponent<Animator>().enabled = true;
        }
        if (Time.timeScale == 1)
        {
            turnLeftHand.SetActive(false);
            turnLeftImage.SetActive(false);
            turnRightHand.SetActive(false);
            turnRightImage.SetActive(false);
            rightButton.GetComponent<Animator>().enabled = false;
            leftButton.GetComponent<Animator>().enabled = false;
            if (firstStop)
            {
                rightButton.SetActive(false);
            }
            if (secondStop)
            {
                
                leftButton.SetActive(false);
            }
        }
    }
    public void Buttons(int butonNo)
    {
        if (butonNo == 1)
        {
            
            Time.timeScale = 1;
        }
        if (butonNo == 2)
        {
            
            Time.timeScale = 1;
        }
    }
}
