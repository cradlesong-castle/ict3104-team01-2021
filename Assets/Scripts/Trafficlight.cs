using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Trafficlight : MonoBehaviour
{
    /*
     Traffic light array
     Element 0 - Green Light
     Element 1 - Red Light
     Element 2 - Amber Light
     */
    public GameObject[] lights;

    private float time = 0.0f;
    public float greenLight = 0f;
    public float amberLight = 5f;
    public float redLight = 10f;
    public float restart = 15f;
    private string state = "green";

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (time>= greenLight && time < amberLight)
        {
            lights[0].SetActive(true);
            lights[1].SetActive(false);
            lights[2].SetActive(false);

        }
        else if (time >= amberLight && time < redLight)
        {
            lights[0].SetActive(false);
            lights[1].SetActive(false);
            lights[2].SetActive(true);

        }
        else if (time >= redLight && time < restart)
        {
            lights[0].SetActive(false);
            lights[1].SetActive(true);
            lights[2].SetActive(false);

        }
        else if (time >= restart)
        {
            time = 0.0f;
        }
        /*
        if (time >= interpolationPeriod)
        {
            time = 0.0f;
            //change from green to red
            if (state.Contains("green"))
            {
                lights[0].SetActive(false);
                lights[1].SetActive(true);
                state = "red";

            }
            //change from
            else
            {
                lights[0].SetActive(true);
                lights[1].SetActive(false);
                state = "green";
            }
        }*/

    }
}
