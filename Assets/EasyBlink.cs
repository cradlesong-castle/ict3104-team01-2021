using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EasyBlink : MonoBehaviour
{
    private bool isLit = true;
    
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("BlinkLights", 0f, 0.5f);  //0s delay, repeat every 0.25s
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void BlinkLights()
    {
        if (isLit)
        {
            for (int j = 0; j < transform.childCount; j++)
            {
                transform.GetChild(j).gameObject.SetActive(false);
            }
            isLit = false;
        }
        else if (!isLit)
        {
            for (int j = 0; j < transform.childCount; j++)
            {
                transform.GetChild(j).gameObject.SetActive(true);
            }
            isLit = true;
        }
    }
}
