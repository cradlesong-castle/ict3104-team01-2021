using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointNavigator : MonoBehaviour
{

    public GameObject car;
    CarController controller;
    public Waypoint currentWaypoint;
    private void Awake()
    {
        controller = GetComponent<CarController>();

    }

    void Start()
    {
        controller.SetDestination(currentWaypoint.GetPosition());

    }
    void Update()
    {
        if (controller.reachedDestination)
        {
            if (currentWaypoint.nextWaypoint != null){
              currentWaypoint = currentWaypoint.nextWaypoint;
              controller.SetDestination(currentWaypoint.GetPosition());

            }
            else{
              Destroy(car);
            }

        }
    }
}
