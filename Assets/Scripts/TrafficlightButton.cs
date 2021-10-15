using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrafficlightButton : MonoBehaviour
{


    /*
     Traffic light array
     Element 0 - Green Light
     Element 1 - Red Light
     Element 2 - Amber Light
     */
    public GameObject[] lightsleft;
    public GameObject[] lightsright;
    public GameObject displayText;
    private bool isPlayerInZone;

    private bool isLeft = false;

   

    private float time = 0.0f;
    public float greenLight = 0f;
    public float amberLight = 5f;
    public float redLight = 10f;
    public float restart = 15f;
    private string state = "green";
    // Start is called before the first frame update
    void Start()
    {
        isPlayerInZone = false;
        displayText.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //Input.GetKeyDown(KeyCode.F)
    }
}
