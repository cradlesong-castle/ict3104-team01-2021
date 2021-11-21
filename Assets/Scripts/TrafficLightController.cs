using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class TrafficLightController : MonoBehaviour
{
    
    public GameObject[] controlledCrossings;
    public GameObject[] trafficLights;
    public GameObject[] pedLights;

    public float time = 0.0f;
    public float greenLight = 0f;
    public float amberLight = 3f;

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

    public void UpdateChildren()
    {
        foreach (var trafficLight in trafficLights)
        {
            trafficLight.GetComponent<Trafficlight>().state = state;
        }
        foreach (var pedLight in pedLights)
        {
            pedLight.GetComponent<PedLight>().state = state;
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (time > 0f)
        {
            time = time - Time.deltaTime;
        }

        if (state == 1) // Green
        {
            GetComponent<Image>().color = new Color(0f, 0.95f, 0f);
        }
        else if (state == 2) // Amber
        {
            GetComponent<Image>().color = new Color(0.93f, 0.77f, 0f);

            if (time <= 0f)
            {
                state = 3;
            }
        }
        else if (state == 3) // Red
        {
            GetComponent<Image>().color = new Color(0.93f, 0f, 0f);
        }
        UpdateChildren();
    }
}
