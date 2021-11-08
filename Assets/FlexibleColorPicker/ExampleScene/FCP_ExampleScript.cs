using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FCP_ExampleScript : MonoBehaviour
{
    public SceneController sceneController;
    public FlexibleColorPicker fcp;
    public Material material;

    public Color externalColor;
    private Color internalColor;

    private void Start() {
        internalColor = externalColor;
    }

    private void Update() {
        //apply color of this script to the FCP whenever it is changed by the user
        if(internalColor != externalColor) {
            fcp.color = externalColor;
            internalColor = externalColor;
            //sceneController.WriteToCSV("Event", "Vehicle Appearance", "\"Car color set as " + externalColor + "\"");
        }

        //extract color from the FCP and apply it to the object material
        material.color = fcp.color;
    }
}
