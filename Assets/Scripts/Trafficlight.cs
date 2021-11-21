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

    public float time = 0.0f;
    public float greenLight = 0f;
    public float amberLight = 3f;
    public float redLight = 10f;
    public float restart = 15f;
    public int state = 3; //1 2 3 = green amber red
    //private string state = "green";

    public void ToggleColor()
    {
        if (state == 1)
        {
            SwitchToRed();
        }
        else if (state == 3)
        {
            SwitchToGreen();
        }
    }


    public void SwitchToRed()
    {
        time = amberLight;
        state = 2;
    }

    public void SwitchToGreen()
    {
        state = 1;
}

    // Update is called once per frame
    void Update()
    {

        if (time > 0f)
        {
            time = time - Time.deltaTime;
        }

        if (state == 1)
        {
            lights[0].SetActive(true);
            lights[1].SetActive(false);
            lights[2].SetActive(false);
        }
        else if (state == 2)
        {
            lights[0].SetActive(false);
            lights[1].SetActive(false);
            lights[2].SetActive(true);
            //if (time <= 0f)
            //{
            //    state = 3;
            //}
        }
        else if (state == 3)
        {
            lights[0].SetActive(false);
            lights[1].SetActive(true);
            lights[2].SetActive(false);
        }
        //time += Time.deltaTime;
        //if (time>= greenLight && time < amberLight)
        //{
        //    lights[0].SetActive(true);
        //    lights[1].SetActive(false);
        //    lights[2].SetActive(false);

        //}
        //else if (time >= amberLight && time < redLight)
        //{
        //    lights[0].SetActive(false);
        //    lights[1].SetActive(false);
        //    lights[2].SetActive(true);

        //}
        //else if (time >= redLight && time < restart)
        //{
        //    lights[0].SetActive(false);
        //    lights[1].SetActive(true);
        //    lights[2].SetActive(false);

        //}
        //else if (time >= restart)
        //{
        //    time = 0.0f;
        //}

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
