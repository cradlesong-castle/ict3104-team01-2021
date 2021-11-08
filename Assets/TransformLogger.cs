using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformLogger : MonoBehaviour
{
    public SceneController sceneController;
    public string type;
    public string category;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("LogLocationAndRotation", 0f, 0.5f);  //0s delay, repeat every 0.5s
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void LogLocationAndRotation()
    {
        sceneController.WriteToCSV(type, category, "\"Location: " + this.gameObject.transform.position + " Rotation: " + this.gameObject.transform.rotation + "\"");
    }
}
