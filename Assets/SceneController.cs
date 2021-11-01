using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneController : MonoBehaviour
{
    public GameObject startingText;
    public GameObject driverText;
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
        writeToCSV("Simulation Program", "Scene Starting.");
    }
    public void SwitchIntersectionNone()
    {
        writeToCSV("Intersection Change", "Intersection changed to none.");
        intersectionNone.SetActive(true);
        intersectionT.SetActive(false);
        intersectionX.SetActive(false);
    }
    public void SwitchIntersectionT()
    {
        writeToCSV("Intersection Change", "Intersection changed to T shape.");
        intersectionT.SetActive(true);
        intersectionNone.SetActive(false);
        intersectionX.SetActive(false);
    }
    public void SwitchIntersectionX()
    {
        writeToCSV("Intersection Change", "Intersection changed to cross (X) shape.");
        intersectionX.SetActive(true);
        intersectionNone.SetActive(false);
        intersectionT.SetActive(false);
    }

    public void ChangeCarType(){
      if (!isDriverless){
        selectedCar = driverlessCar;
        isDriverless = true;
        driverText.GetComponent<Text>().text = "Driverless";
        driverModel.SetActive(false);
        writeToCSV("Vehicle", "Car changed to driverless car.");
        }
      else{

        selectedCar = driverCar;
        isDriverless = false;
        driverText.GetComponent<Text>().text = "Driver";
        driverModel.SetActive(true);
        writeToCSV("Vehicle", "Car changed to normal driver car.");
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

    public void Spawn(GameObject carPrefab, GameObject waypoint, int direction)
    {

        writeToCSV("Vehicle", "Car spawned.");
        GameObject obj = Instantiate(carPrefab);
       switch(direction){
         case 0:
           print("rotate bottom");
           obj.transform.rotation =  Quaternion.Euler(Vector3.down * 270);
         break;
         case 1:
           print("rotate left");
           obj.transform.rotation =  Quaternion.Euler(Vector3.down * 180);
         break;
         case 2:
           print("rotate right");
           obj.transform.rotation =  Quaternion.Euler(Vector3.down * 0);
         break;
         case 3:
           print("rotate top");
           obj.transform.rotation =  Quaternion.Euler(Vector3.down * 90);
         break;
       }
       Transform child = waypoint.transform.GetChild(0);
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
        //case 0: bottom to left
          case 0:
            Spawn(selectedCar,waypoints[2],direction);
            startingText.GetComponent<Text>().text = "None Selected";
            writeToCSV("Vehicle", "Car travelling from bottom road to left.");
            break;
          //case 2: right to left
          case 2:
            Spawn(selectedCar,waypoints[11],direction);
            startingText.GetComponent<Text>().text = "None Selected";
            writeToCSV("Vehicle", "Car travelling from bottom right to left.");
            break;
          //case 3: top to left
          case 3:
            Spawn(selectedCar,waypoints[7],direction);
            startingText.GetComponent<Text>().text = "None Selected";
            writeToCSV("Vehicle", "Car travelling from bottom top to left.");
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
        //case 0: bottom to right
          case 0:
            Spawn(selectedCar,waypoints[1],direction);
            startingText.GetComponent<Text>().text = "None Selected";
            writeToCSV("Vehicle", "Car travelling from bottom to right.");
            break;
            //case 1: left to right
          case 1:
            Spawn(selectedCar,waypoints[3],direction);
            startingText.GetComponent<Text>().text = "None Selected";
            writeToCSV("Vehicle", "Car travelling from left to right.");
            break;
          //case 3: top to right
          case 3:
            Spawn(selectedCar,waypoints[8],direction);
            startingText.GetComponent<Text>().text = "None Selected";
            writeToCSV("Vehicle", "Car travelling from bottom top to right.");
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
          //case 0: bottom to top
            case 0:
              Spawn(selectedCar,waypoints[0],direction);
              startingText.GetComponent<Text>().text = "None Selected";
            writeToCSV("Vehicle", "Car travelling from bottom to top.");
              break;
              //case 1: left to top
            case 1:
              Spawn(selectedCar,waypoints[5],direction);
              startingText.GetComponent<Text>().text = "None Selected";
            writeToCSV("Vehicle", "Car travelling from left to top.");
              //Spawn(selectedCar,waypoints[1]);
              break;
            //case 2: right to top
            case 2:
              Spawn(selectedCar,waypoints[10],direction);
              startingText.GetComponent<Text>().text = "None Selected";
            writeToCSV("Vehicle", "Car travelling from right to top.");
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
            //case 1: left to bottom
          case 1:
            Spawn(selectedCar,waypoints[4],direction);
            startingText.GetComponent<Text>().text = "None Selected";
            writeToCSV("Vehicle", "Car travelling from left to bottom.");
            break;
          //case 2: right to bottom
          case 2:
            Spawn(selectedCar,waypoints[9],direction);
            startingText.GetComponent<Text>().text = "None Selected";
            writeToCSV("Vehicle", "Car travelling from right to bottom.");
            break;
          //case 3: top to bottom
          case 3:
            Spawn(selectedCar,waypoints[6],direction);
            startingText.GetComponent<Text>().text = "None Selected";
            writeToCSV("Vehicle", "Car travelling from top to bottom.");
            break;

        }
      }
    }

    void writeToCSV(string logtype, string message)
    {
        String timestamp = DateTime.Now.ToString(@"MM\/dd\/yyyy h\:mm tt");
        String filepath = System.AppContext.BaseDirectory + "\\logfile.csv";
        try
        {
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(@filepath, true))
            {
                file.WriteLine(timestamp + "," + logtype + "," + message);
            }
        }
        catch (Exception ex)
        {
            throw new ApplicationException("ERROR LOGGING ", ex);
        }
    }


}
