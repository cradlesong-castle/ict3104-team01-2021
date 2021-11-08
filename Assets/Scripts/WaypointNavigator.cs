using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointNavigator : MonoBehaviour
{

    public GameObject car;
    CarController controller;
    public SceneController sceneController;
    public Waypoint currentWaypoint;
    public Waypoint nextWaypoint;

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
            if (currentWaypoint.nextWaypoint != null)
            {
                currentWaypoint = currentWaypoint.nextWaypoint;
                controller.SetDestination(currentWaypoint.GetPosition());
                if (currentWaypoint.nextWaypoint != null)
                {
                    nextWaypoint = currentWaypoint.nextWaypoint;
                }
                if (currentWaypoint.name == "End")
                {
                    controller.OperateIndicatorLights("left", "off");
                    controller.OperateIndicatorLights("right", "off");
                }
            }
            else
            {
                controller.LogDespawn();
                sceneController.ReactivateButtons();
                sceneController.ResetSpawnText();
                Destroy(car);
            }
        }
    }
}
