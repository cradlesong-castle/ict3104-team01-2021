using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NameTriggerDetector : MonoBehaviour
{
    public CarDetector carDetector;
    public bool isLeft;
    public bool isRight;
    public bool isUp;
    public bool isDown;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.name == "XR Rig")
        {
            if (isLeft)
            {
                carDetector.isLeft = true;
            }
            else if (isRight)
            {
                carDetector.isRight = true;
            }
            else if (isUp)
            {
                carDetector.isUp = true;
            }
            else if (isDown)
            {
                carDetector.isDown = true;
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.name == "XR Rig")
        {
            if (isLeft)
            {
                carDetector.isLeft = false;
            }
            else if (isRight)
            {
                carDetector.isRight = false;
            }
            else if (isUp)
            {
                carDetector.isUp = false;
            }
            else if (isDown)
            {
                carDetector.isDown = false;
            }
        }
    }
}
