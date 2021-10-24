using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneController : MonoBehaviour
{


    [Header("Waypoints")]
    public GameObject leftwaypoint;
    public GameObject rightwaypoint;
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
    public void SpawnBottomDriver()
    {
      Spawn(selectedCar,leftwaypoint);
    }

    public void SpawnRightDriver()
    {
      Spawn(selectedCar,rightwaypoint);
    }

    public void SpawnBottomDriverless()
    {
      Spawn(selectedCar,leftwaypoint);
    }

    public void SpawnRightDriverless()
    {

      Spawn(selectedCar,rightwaypoint);
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
}
