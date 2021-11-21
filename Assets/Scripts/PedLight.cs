using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PedLight : MonoBehaviour
{
    
    public GameObject redLight;
    public GameObject greenLight;

    public int state = 1; //1 2 = green red


    // Update is called once per frame
    void Update()
    {
        if (state == 1 || state == 2)
        {
            greenLight.SetActive(false);
            redLight.SetActive(true);
        }
        else if (state == 3)
        {
            greenLight.SetActive(true);
            redLight.SetActive(false);
        }
    }
}
