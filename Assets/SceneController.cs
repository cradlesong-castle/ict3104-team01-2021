using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneController : MonoBehaviour
{

    public GameObject startingText;
    [Header("Waypoints")]  /*
       * List of routes
       0 = bottom to top
       1 = bottom to right
       2 = bottom to left

       direction is always from the perspective of the player spawn point
       */
    public GameObject[] waypoints;

    [Header("Car")]
    public GameObject driverCar;
    public GameObject driverlessCar;

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

    private GameObject selectedCar;
    private bool isDriverless = false;

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

    /*
     * This controls the presence of intersections
     */
    void Start(){
      selectedCar = driverCar;
    }
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

    public void ChangeCarType(){
      if (!isDriverless){
        selectedCar = driverlessCar;
        isDriverless = true;
      }
      else{

        selectedCar = driverCar;
        isDriverless = false;
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
        foreach(var tile in crossingTrafficLight)
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

    public void Spawn(GameObject carPrefab, GameObject waypoint)
    {

       GameObject obj = Instantiate(carPrefab);
       Transform child = waypoint.transform.GetChild(0);
       obj.GetComponent<WaypointNavigator>().currentWaypoint = child.GetComponent<Waypoint>();
       obj.transform.position = child.position;
    }

    public void SpawnLeftDriver(){
      print("Click Left");
      if (pressCount==0){
        direction = 1;
        pressCount+=1;
        startingText.GetComponent<Text>().text = "Start from Left";
      }
      else{
        pressCount = 0;
        switch(direction)
        {
          case 0:
            Spawn(selectedCar,waypoints[2]);
            startingText.GetComponent<Text>().text = "None Selected";
            break;
          case 1:
            Spawn(selectedCar,waypoints[0]);
            startingText.GetComponent<Text>().text = "None Selected";
            break;

        }
      }
    }

    public void SpawnRightDriver(){
      print("Click Right");
      if (pressCount==0){
        direction = 2;
        pressCount+=1;
        startingText.GetComponent<Text>().text = "Start from Right";
      }
      else{
        pressCount = 0;
        switch(direction)
        {
          case 0:
            Spawn(selectedCar,waypoints[1]);
            startingText.GetComponent<Text>().text = "None Selected";
            break;
          case 1:
            startingText.GetComponent<Text>().text = "None Selected";
            //Spawn(selectedCar,waypoints[0]);
            break;

        }
      }
    }
    public void SpawnTopDriver(){
      print("Click Top");

        if (pressCount==0){
          direction = 3;
          pressCount+=1;
          startingText.GetComponent<Text>().text = "Start from Top";
        }
        else{
          pressCount = 0;
          switch(direction)
          {
            case 0:
              Spawn(selectedCar,waypoints[0]);
              startingText.GetComponent<Text>().text = "None Selected";
              break;
            case 1:
              startingText.GetComponent<Text>().text = "None Selected";
              //Spawn(selectedCar,waypoints[1]);
              break;

          }
        }
    }

    public void SpawnBottomDriver()
    {
      print("Click Bottom");
      if (pressCount==0){
        direction = 0;
        pressCount+=1;
        startingText.GetComponent<Text>().text = "Start from Bottom";
      }
      else{
        pressCount = 0;
        switch(direction)
        {
          case 0:
            Spawn(selectedCar,waypoints[0]);
            startingText.GetComponent<Text>().text = "None Selected";
            break;
          case 1:
            //Spawn(selectedCar,waypoints[1]);
            startingText.GetComponent<Text>().text = "None Selected";
            break;

        }
      }
    }


}
