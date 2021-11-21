using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneController : MonoBehaviour
{

    public GameObject startingText;
    public Text driverText;
    public Text dayNightText;

    [Header("WeatherControls")]
    public bool isDay;
    public Light environmentLight;
    public GameObject[] streetLights;

    [Header("Waypoints")]  /*
       * List of routes

       0 = bottom to top
       1 = bottom to right
       2 = bottom to left
       3 = left to right
       4 = left to bottom
       5 = left to top
       6 = top to Bottom
       7 = top to left
       8 = top to right

       direction is always from the perspective of the player spawn point
       */
    public GameObject[] waypoints;

    [Header("Car")]
    public GameObject driverCar;
    public GameObject driverlessCar;
    public GameObject driverModel;

    [Header("Intersections")]
    public GameObject intersectionNone;
    public GameObject intersectionT;
    public GameObject intersectionX;
    [Header("Crossings")]
    public GameObject[] crossingTrafficLight;
    public GameObject[] crossingZebra;
    [Header("Direction Arrows")]
    public GameObject[] arrowsUni;
    public GameObject[] arrowsBi;

    [Header("Car Customization")]
    public GameObject selectedCar;
    public Material selectedPaint;
    public GameObject carNormal;
    public GameObject carA;
    public GameObject carB;
    public GameObject carC;
    public Material carNormalMat;
    public Material carAMat;
    public Material carBMat;
    public Material carCMat;
    private bool isDriverless = false;
    // public int avLabel = 0;
    public GameObject previewCar;
    public Transform previewCarLocation;


    [Header("UI References")]
    public GameObject spawnButtons;
    //press count keeps track of whether its the start or end
    private int pressCount = 0;
    /*
     * the integer represents what is the waypoint Start
     0 = bottom
     1 = left
     2 = right
     3 = top

     direction is always from the perspective of the player spawn point
     */
    private int direction = 0;

    void Start()
    {
        selectedCar = carNormal;
        WriteToCSV("Event", "Program", "Scene started");
        WriteToCSV("Event", "Intersection Change", "Intersection None set");
        WriteToCSV("Event", "Vehicle Appearance", "Driver visible set");
        WriteToCSV("Event", "Vehicle Appearance", "AV label non-visible set");
        WriteToCSV("Event", "Road Direction", "Uni-direction roads set");
        WriteToCSV("Event", "Crossing", "Zebra Crossing set");
        WriteToCSV("Event", "Vehicle Appearance", "Normal Car set");
    }

    /*
     * This controls the presence of intersections
     */
    public void SwitchIntersectionNone()
    {
        WriteToCSV("Event", "Intersection Change", "Intersection None set");
        intersectionNone.SetActive(true);
        intersectionT.SetActive(false);
        intersectionX.SetActive(false);
    }
    public void SwitchIntersectionT()
    {
        WriteToCSV("Event", "Intersection Change", "Intersection T (T-Junction) set");
        intersectionT.SetActive(true);
        intersectionNone.SetActive(false);
        intersectionX.SetActive(false);
    }
    public void SwitchIntersectionX()
    {
        WriteToCSV("Event", "Intersection Change", "Intersection X (Cross-Junction) set");
        intersectionX.SetActive(true);
        intersectionNone.SetActive(false);
        intersectionT.SetActive(false);
    }

    /* 
     * These control the visual aspects of the cars */
    public void SwitchCarNormal()
    {
        selectedCar = carNormal;
        WriteToCSV("Event", "Vehicle Appearance", "Normal Car set");
        UpdatePreview();
    }
    public void SwitchCarA()
    {
        selectedCar = carA;
        WriteToCSV("Event", "Vehicle Appearance", "AV Car A set");
        UpdatePreview();
    }
    public void SwitchCarB()
    {
        selectedCar = carB;
        WriteToCSV("Event", "Vehicle Appearance", "AV Car B set");
        UpdatePreview();

    }
    public void SwitchCarC()
    {
        selectedCar = carC;
        WriteToCSV("Event", "Vehicle Appearance", "AV Car C set");
        UpdatePreview();
    }
    public void ChangeDriverVisibility()
    {
        if (!isDriverless)
        {
            isDriverless = true;
            driverText.text = "Not Visible";
            WriteToCSV("Event", "Vehicle Appearance", "Driver not visible set");
            carA.transform.Find("Driver").gameObject.SetActive(false);
            carB.transform.Find("Driver").gameObject.SetActive(false);
            carC.transform.Find("Driver").gameObject.SetActive(false);
        }
        else
        {
            isDriverless = false;
            driverText.text = "Visible";
            WriteToCSV("Event", "Vehicle Appearance", "Driver visible set");
            carA.transform.Find("Driver").gameObject.SetActive(true);
            carB.transform.Find("Driver").gameObject.SetActive(true);
            carC.transform.Find("Driver").gameObject.SetActive(true);
        }
        UpdatePreview();
    }

    public void ChangeDayNight()
    {
        GameObject temp_light;
        if (!isDay)
        {
            isDay = true;
            dayNightText.text = "Day";
            environmentLight.color = new Color(1f, 0.95f, 0.83f);
            foreach (var streetLamp in streetLights)
            {
                foreach (var light in streetLamp.GetComponentsInChildren<Transform>())
                {
                    //Debug.Log("1 " + light);
                    //Debug.Log("2 " + light.GetChild(0));
                    //Debug.Log("3 " + light.GetChild(0).GetChild(0).gameObject);
                    if (light.name == "Street Lamp")
                    {
                        light.GetChild(0).gameObject.SetActive(false);

                    }
                    //temp_light = light.transform.Find("light").gameObject;
                    //if(temp_light.name != null)
                    //{
                    //    temp_light.gameObject.SetActive(false);
                    //}
                }
            }
            carNormal.GetComponent<CarController>().frontLights.SetActive(false);
            carA.GetComponent<CarController>().frontLights.SetActive(false);
            carB.GetComponent<CarController>().frontLights.SetActive(false);
            carC.GetComponent<CarController>().frontLights.SetActive(false);
            WriteToCSV("Event", "Weather", "Day set. Street lights off. ");
        }
        else
        {
            isDay = false;
            dayNightText.text = "Night";
            environmentLight.color = new Color(0.07f, 0, 0.24f);
            foreach (var streetLamp in streetLights)
            {
                foreach (var light in streetLamp.GetComponentsInChildren<Transform>()) {
                    if (light.name == "Street Lamp")
                    {
                        light.GetChild(0).gameObject.SetActive(true);

                    }
                }
            }
            carNormal.GetComponent<CarController>().frontLights.SetActive(true);
            carA.GetComponent<CarController>().frontLights.SetActive(true);
            carB.GetComponent<CarController>().frontLights.SetActive(true);
            carC.GetComponent<CarController>().frontLights.SetActive(true);
            WriteToCSV("Event", "Weather", "Night set. Street lights on. ");
        }
        UpdatePreview();
    }
    //public void ChangeAvVisibility()
    //{
    //    if (avLabel == 0)
    //    {
    //        // Switch to Black (from none)
    //        avText.text = "Black";
    //        WriteToCSV("Event", "Vehicle Appearance", "AV label black set");
    //        carA.transform.Find("avBlack").gameObject.SetActive(true);
    //        carB.transform.Find("avBlack").gameObject.SetActive(true);
    //        carC.transform.Find("avBlack").gameObject.SetActive(true);
    //        avLabel = 1;
    //    }
    //    else if (avLabel == 1)
    //    {
    //        // Switch to White (from Black)
    //        avText.text = "White";
    //        WriteToCSV("Event", "Vehicle Appearance", "AV label white set");
    //        carA.transform.Find("avWhite").gameObject.SetActive(true);
    //        carB.transform.Find("avWhite").gameObject.SetActive(true);
    //        carC.transform.Find("avWhite").gameObject.SetActive(true);
    //        carA.transform.Find("avBlack").gameObject.SetActive(false);
    //        carB.transform.Find("avBlack").gameObject.SetActive(false);
    //        carC.transform.Find("avBlack").gameObject.SetActive(false);
    //        avLabel = 2;
    //    }
    //    else if (avLabel == 2)
    //    {
    //        // Switch to White (from White)
    //        avText.text = "Not Visible";
    //        WriteToCSV("Event", "Vehicle Appearance", "AV label non-visible set");
    //        carA.transform.Find("avWhite").gameObject.SetActive(false);
    //        carB.transform.Find("avWhite").gameObject.SetActive(false);
    //        carC.transform.Find("avWhite").gameObject.SetActive(false);
    //        avLabel = 0;
    //    }
    //    UpdatePreview();
    //}

    private void UpdatePreview()
    {
        if (previewCar != null)
        {
            Destroy(previewCar);
        }
        previewCar = Instantiate(selectedCar, previewCarLocation);
        previewCar.transform.localPosition = new Vector3(0, 0, 0);
        previewCar.name = "PreviewCar";
        previewCar.layer = LayerMask.NameToLayer("UI");
        foreach (Transform child in previewCar.GetComponentsInChildren<Transform>())
        {
            child.gameObject.layer = LayerMask.NameToLayer("UI");
        }
    }


    /*
     * This controls uni-bidirection streets
     */
    public void SwitchDirectionUni()
    {
        foreach (var arrow in arrowsUni)
        {
            arrow.SetActive(true);
        }
        foreach (var arrow in arrowsBi)
        {
            arrow.SetActive(false);
        }
        WriteToCSV("Event", "Road Direction", "Uni-direction roads set");
    }
    public void SwitchDirectionBi()
    {
        foreach (var arrow in arrowsBi)
        {
            arrow.SetActive(true);
        }
        foreach (var arrow in arrowsUni)
        {
            arrow.SetActive(false);
        }
        WriteToCSV("Event", "Road Direction", "Bi-direction roads set");
    }

    /*
     * This controls crossing types
     */
    public void SwitchCrossingTrafficLight()
    {
        foreach (var tile in crossingTrafficLight)
        {
            tile.SetActive(true);
        }
        foreach (var tile in crossingZebra)
        {
            tile.SetActive(false);
        }
        WriteToCSV("Event", "Crossing", "Traffic Light set");
    }
    public void SwitchCrossingZebra()
    {
        foreach (var tile in crossingZebra)
        {
            tile.SetActive(true);
        }
        foreach (var tile in crossingTrafficLight)
        {
            tile.SetActive(false);
        }
        WriteToCSV("Event", "Crossing", "Zebra Crossing set");
    }

    public void DeactivateButtons()
    {
        spawnButtons.SetActive(false);
    }

    public void ReactivateButtons()
    {
        spawnButtons.SetActive(true);
    }

    public void Spawn(GameObject carPrefab, GameObject waypoint, int direction, string turnDir = "")
    {
        WriteToCSV("Event", "Vehicle Spawn", "Car spawned");
        DeactivateButtons();
        GameObject obj = Instantiate(carPrefab);
        switch (direction)
        {
            case 0:
                //print("rotate bottom");
                obj.transform.rotation = Quaternion.Euler(Vector3.down * 0);
                break;
            case 1:
                //print("rotate left");
                obj.transform.rotation = Quaternion.Euler(Vector3.down * 270);
                break;
            case 2:
                //print("rotate right");
                obj.transform.rotation = Quaternion.Euler(Vector3.down * 90);
                break;
            case 3:
                //print("rotate top");
                obj.transform.rotation = Quaternion.Euler(Vector3.down * 180);
                break;
        }
        Transform child = waypoint.transform.GetChild(0);
        obj.GetComponent<WaypointNavigator>().enabled = true;
        obj.GetComponent<CarController>().enabled = true;
        obj.GetComponent<WaypointNavigator>().currentWaypoint = child.GetComponent<Waypoint>();
        if (turnDir != "")
        {
            if (turnDir == "left")
            {
                obj.GetComponent<CarController>().isTurningLeft = true;
                obj.GetComponent<CarController>().OperateIndicatorLights("left", "on");

            }
            else if (turnDir == "right")
            {
                obj.GetComponent<CarController>().isTurningRight = true;
                obj.GetComponent<CarController>().OperateIndicatorLights("right", "on");
            }
        }
        obj.transform.position = child.position;
    }

    /*
     * the integer represents what is the waypoint Start
     0 = bottom
     1 = left
     2 = right
     3 = top

     direction is always from the perspective of the player spawn point
     */
    public void SpawnLeftDriver()
    {
        //print("Click Left");
        if (pressCount == 0)
        {
            direction = 1;
            pressCount += 1;
            startingText.GetComponent<Text>().text = "Start: Left\nEnd: Click on a square on the right";
        }
        else
        {
            pressCount = 0;
            switch (direction)
            {
                //case 0: bottom to left
                case 0:
                    Spawn(selectedCar, waypoints[2], direction, "left");
                    startingText.GetComponent<Text>().text = "Start: Bottom\nEnd: Left";
                    WriteToCSV("Event", "Vehicle Spawn", "Car travelling from bottom to left.");
                    break;
                //case 2: right to left
                case 2:
                    Spawn(selectedCar, waypoints[11], direction);
                    startingText.GetComponent<Text>().text = "Start: Right\nEnd: Left";
                    WriteToCSV("Event", "Vehicle Spawn", "Car travelling from right to left.");
                    break;
                //case 3: top to left
                case 3:
                    Spawn(selectedCar, waypoints[7], direction, "right");
                    startingText.GetComponent<Text>().text = "Start: Top\nEnd: Left";
                    WriteToCSV("Event", "Vehicle Spawn", "Car travelling from top to left.");
                    break;

            }
        }
    }

    public void SpawnRightDriver()
    {
        //print("Click Right");
        if (pressCount == 0)
        {
            direction = 2;
            pressCount += 1;
            startingText.GetComponent<Text>().text = "Start: Right\nEnd: Click on a square on the right";
        }
        else
        {
            pressCount = 0;
            switch (direction)
            {
                //case 0: bottom to right
                case 0:
                    Spawn(selectedCar, waypoints[1], direction, "right");
                    startingText.GetComponent<Text>().text = "Start: Bottom\nEnd: Right";
                    WriteToCSV("Event", "Vehicle Spawn", "Car travelling from bottom to right.");
                    break;
                //case 1: left to right
                case 1:
                    Spawn(selectedCar, waypoints[3], direction);
                    startingText.GetComponent<Text>().text = "Start: Left\nEnd: Right";
                    WriteToCSV("Event", "Vehicle Spawn", "Car travelling from left to right.");
                    break;
                //case 3: top to right
                case 3:
                    Spawn(selectedCar, waypoints[8], direction, "left");
                    startingText.GetComponent<Text>().text = "Start: Top\nEnd: Right";
                    WriteToCSV("Event", "Vehicle Spawn", "Car travelling from top to right.");
                    break;

            }
        }
    }
    public void SpawnTopDriver()
    {
        //print("Click Top");

        if (pressCount == 0)
        {
            direction = 3;
            pressCount += 1;
            startingText.GetComponent<Text>().text = "Start: Top\nEnd: Click on a square on the right";
        }
        else
        {
            pressCount = 0;
            switch (direction)
            {
                //case 0: bottom to top
                case 0:
                    Spawn(selectedCar, waypoints[0], direction);
                    startingText.GetComponent<Text>().text = "Start: Bottom\nEnd: Top";
                    WriteToCSV("Event", "Vehicle Spawn", "Car travelling from bottom to top.");
                    break;
                //case 1: left to top
                case 1:
                    Spawn(selectedCar, waypoints[5], direction, "left");
                    startingText.GetComponent<Text>().text = "Start: Left\nEnd: Top";
                    WriteToCSV("Event", "Vehicle Spawn", "Car travelling from left to top.");
                    //Spawn(selectedCar,waypoints[1]);
                    break;
                //case 2: right to top
                case 2:
                    Spawn(selectedCar, waypoints[10], direction, "right");
                    startingText.GetComponent<Text>().text = "Start: Right\nEnd: Top";
                    WriteToCSV("Event", "Vehicle Spawn", "Car travelling from right to top.");
                    break;

            }
        }
    }

    public void ResetSpawnText()
    {
        startingText.GetComponent<Text>().text = "Start: Click on a square on the right\nEnd: Click on a square on the right";
    }

    public void SpawnBottomDriver()
    {
        //print("Click Bottom");
        //print("Click Bottom");
        if (pressCount == 0)
        {
            direction = 0;
            pressCount += 1;
            startingText.GetComponent<Text>().text = "Start: Bottom\nEnd: Click on a square on the right";
        }
        else
        {
            pressCount = 0;
            switch (direction)
            {
                //case 1: left to bottom
                case 1:
                    Spawn(selectedCar, waypoints[4], direction, "right");
                    startingText.GetComponent<Text>().text = "Start: Left\nEnd: Bottom";
                    WriteToCSV("Event", "Vehicle Spawn", "Car travelling from left to bottom.");
                    break;
                //case 2: right to bottom
                case 2:
                    Spawn(selectedCar, waypoints[9], direction, "left");
                    startingText.GetComponent<Text>().text = "Start: Right\nEnd: Bottom";
                    WriteToCSV("Event", "Vehicle Spawn", "Car travelling from right to bottom.");
                    break;
                //case 3: top to bottom
                case 3:
                    Spawn(selectedCar, waypoints[6], direction);
                    startingText.GetComponent<Text>().text = "Start: Top\nEnd: Bottom";
                    WriteToCSV("Event", "Vehicle Spawn", "Car travelling from top to bottom.");
                    break;

            }
        }
    }

    public void WriteToCSV(string logtype, string logcat, string message)
    {
        String timestamp = DateTime.Now.ToString(@"MM\/dd\/yyyy h\:mm\:ss tt");
        String filepath = System.AppContext.BaseDirectory + "\\logfile.csv";
        try
        {
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(@filepath, true))
            {
                file.WriteLine(timestamp + "," + logtype + "," + logcat + "," + message);
            }
        }
        catch (Exception ex)
        {
            throw new ApplicationException("ERROR LOGGING ", ex);
        }
    }

}
