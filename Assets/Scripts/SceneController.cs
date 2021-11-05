using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneController : MonoBehaviour
{

    public GameObject startingText;
    public Text driverText;
    public Text avText;

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
    public GameObject carA;
    public GameObject carB;
    public GameObject carC;
    public Material carAMat;
    public Material carBMat;
    public Material carCMat;
    private bool isDriverless = false;
    public int avLabel = 0;
    public GameObject previewCar;
    public Transform previewCarLocation;

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
        selectedCar = carA;
    }

    /*
     * This controls the presence of intersections
     */
    public void SwitchIntersectionNone()
    {
        intersectionNone.SetActive(true);
        intersectionT.SetActive(false);
        intersectionX.SetActive(false);
    }
    public void SwitchIntersectionT()
    {
        intersectionT.SetActive(true);
        intersectionNone.SetActive(false);
        intersectionX.SetActive(false);
    }
    public void SwitchIntersectionX()
    {
        intersectionX.SetActive(true);
        intersectionNone.SetActive(false);
        intersectionT.SetActive(false);
    }

    /* 
     * These control the visual aspects of the cars */
    public void SwitchCarA()
    {
        selectedCar = carA;
        UpdatePreview();
    }
    public void SwitchCarB()
    {
        selectedCar = carB;
        UpdatePreview();

    }
    public void SwitchCarC()
    {
        selectedCar = carC;
        UpdatePreview();
    }
    public void ChangeDriverVisibility()
    {
        if (!isDriverless)
        {
            isDriverless = true;
            driverText.text = "Not Visible";
            carA.transform.Find("Driver").gameObject.SetActive(false);
            carB.transform.Find("Driver").gameObject.SetActive(false);
            carC.transform.Find("Driver").gameObject.SetActive(false);
        }
        else
        {
            isDriverless = false;
            driverText.text = "Visible";
            carA.transform.Find("Driver").gameObject.SetActive(true);
            carB.transform.Find("Driver").gameObject.SetActive(true);
            carC.transform.Find("Driver").gameObject.SetActive(true);
        }
        UpdatePreview();
    }
    public void ChangeAvVisibility()
    {
        if (avLabel == 0)
        {
            // Switch to Black (from none)
            avText.text = "Black";
            carA.transform.Find("avBlack").gameObject.SetActive(true);
            carB.transform.Find("avBlack").gameObject.SetActive(true);
            carC.transform.Find("avBlack").gameObject.SetActive(true);
            avLabel = 1;
        }
        else if (avLabel == 1)
        {
            // Switch to White (from Black)
            avText.text = "White";
            carA.transform.Find("avWhite").gameObject.SetActive(true);
            carB.transform.Find("avWhite").gameObject.SetActive(true);
            carC.transform.Find("avWhite").gameObject.SetActive(true);
            carA.transform.Find("avBlack").gameObject.SetActive(false);
            carB.transform.Find("avBlack").gameObject.SetActive(false);
            carC.transform.Find("avBlack").gameObject.SetActive(false);
            avLabel = 2;
        }
        else if (avLabel == 2)
        {
            // Switch to White (from White)
            avText.text = "Not Visible";
            carA.transform.Find("avWhite").gameObject.SetActive(false);
            carB.transform.Find("avWhite").gameObject.SetActive(false);
            carC.transform.Find("avWhite").gameObject.SetActive(false);
            avLabel = 0;
        }
        UpdatePreview();
    }

    private void UpdatePreview()
    {
        if (previewCar != null)
        {
            Destroy(previewCar);
        }
        previewCar = Instantiate(selectedCar, previewCarLocation);
        previewCar.transform.localPosition = new Vector3(0, 0, 0);
        previewCar.name = "PreviewCar";
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
    }

    public void Spawn(GameObject carPrefab, GameObject waypoint, int direction)
    {

        GameObject obj = Instantiate(carPrefab);
        switch (direction)
        {
            case 0:
                print("rotate bottom");
                obj.transform.rotation = Quaternion.Euler(Vector3.down * 0);
                break;
            case 1:
                print("rotate left");
                obj.transform.rotation = Quaternion.Euler(Vector3.down * 270);
                break;
            case 2:
                print("rotate right");
                obj.transform.rotation = Quaternion.Euler(Vector3.down * 90);
                break;
            case 3:
                print("rotate top");
                obj.transform.rotation = Quaternion.Euler(Vector3.down * 180);
                break;
        }
        Transform child = waypoint.transform.GetChild(0);
        obj.GetComponent<WaypointNavigator>().enabled = true;
        obj.GetComponent<CarController>().enabled = true;
        obj.GetComponent<WaypointNavigator>().currentWaypoint = child.GetComponent<Waypoint>();
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
        print("Click Left");
        if (pressCount == 0)
        {
            direction = 1;
            pressCount += 1;
            startingText.GetComponent<Text>().text = "Start from Left";
        }
        else
        {
            pressCount = 0;
            switch (direction)
            {
                //case 0: bottom to left
                case 0:
                    Spawn(selectedCar, waypoints[2], direction);
                    startingText.GetComponent<Text>().text = "None Selected";
                    break;
                //case 2: right to left
                case 2:
                    Spawn(selectedCar, waypoints[11], direction);
                    startingText.GetComponent<Text>().text = "None Selected";
                    break;
                //case 3: top to left
                case 3:
                    Spawn(selectedCar, waypoints[7], direction);
                    startingText.GetComponent<Text>().text = "None Selected";
                    break;

            }
        }
    }

    public void SpawnRightDriver()
    {
        print("Click Right");
        if (pressCount == 0)
        {
            direction = 2;
            pressCount += 1;
            startingText.GetComponent<Text>().text = "Start from Right";
        }
        else
        {
            pressCount = 0;
            switch (direction)
            {
                //case 0: bottom to right
                case 0:
                    Spawn(selectedCar, waypoints[1], direction);
                    startingText.GetComponent<Text>().text = "None Selected";
                    break;
                //case 1: left to right
                case 1:
                    Spawn(selectedCar, waypoints[3], direction);
                    startingText.GetComponent<Text>().text = "None Selected";
                    break;
                //case 3: top to right
                case 3:
                    Spawn(selectedCar, waypoints[8], direction);
                    startingText.GetComponent<Text>().text = "None Selected";
                    break;

            }
        }
    }
    public void SpawnTopDriver()
    {
        print("Click Top");

        if (pressCount == 0)
        {
            direction = 3;
            pressCount += 1;
            startingText.GetComponent<Text>().text = "Start from Top";
        }
        else
        {
            pressCount = 0;
            switch (direction)
            {
                //case 0: bottom to top
                case 0:
                    Spawn(selectedCar, waypoints[0], direction);
                    startingText.GetComponent<Text>().text = "None Selected";
                    break;
                //case 1: left to top
                case 1:
                    Spawn(selectedCar, waypoints[5], direction);
                    startingText.GetComponent<Text>().text = "None Selected";
                    //Spawn(selectedCar,waypoints[1]);
                    break;
                //case 2: right to top
                case 2:
                    Spawn(selectedCar, waypoints[10], direction);
                    startingText.GetComponent<Text>().text = "None Selected";
                    break;

            }
        }
    }

    public void SpawnBottomDriver()
    {
        print("Click Bottom");
        if (pressCount == 0)
        {
            direction = 0;
            pressCount += 1;
            startingText.GetComponent<Text>().text = "Start from Bottom";
        }
        else
        {
            pressCount = 0;
            switch (direction)
            {
                //case 1: left to bottom
                case 1:
                    Spawn(selectedCar, waypoints[4], direction);
                    startingText.GetComponent<Text>().text = "None Selected";
                    break;
                //case 2: right to bottom
                case 2:
                    Spawn(selectedCar, waypoints[9], direction);
                    startingText.GetComponent<Text>().text = "None Selected";
                    break;
                //case 3: top to bottom
                case 3:
                    Spawn(selectedCar, waypoints[6], direction);
                    startingText.GetComponent<Text>().text = "None Selected";
                    break;

            }
        }
    }


}
