using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointNavigator : MonoBehaviour
{
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
            currentWaypoint = currentWaypoint.nextWaypoint;
            controller.SetDestination(currentWaypoint.GetPosition());

        }
    }
}
