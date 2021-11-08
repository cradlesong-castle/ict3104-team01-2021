using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetector : MonoBehaviour
{

    public bool isUsingOrFacingroad;
    public bool isUsingCrossing;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerStay(Collider other)
    {
        if (other.tag == "Road")
        {
            isUsingOrFacingroad = true;
            if (other.gameObject.name.Contains("Crossing"))
            {
                isUsingCrossing = true;
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Road")
        {
            isUsingOrFacingroad = false;
            if (other.gameObject.name.Contains("Crossing"))
            {
                isUsingCrossing = false;
            }
        }
    }
}
