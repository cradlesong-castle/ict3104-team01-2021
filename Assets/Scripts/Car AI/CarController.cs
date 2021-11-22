using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    public SceneController sceneController;
    public WaypointNavigator waypointNavigator;

    public float setSpeed = 5;
    private float turningSpeed;
    public float movementSpeed = 5;
    public float rotationSpeed = 120;
    private float invisRotationSpeed = 0;
    public float stopDistance = 2f;
    public Vector3 destination;
    public bool reachedDestination;
    public GameObject[] carWheels;

    [Header("Car Booleans and Logic")]
    public GameObject player;
    public CarDetector carDetector;
    public PlayerDetector playerDetector;
    public float playerDistance;
    public bool isStopping;
    public bool isDay;
    public bool isTurningLeft;
    public bool isTurningRight;
    public bool isDecelerating;

    //public bool isPedNear;
    //public bool isCrossingForward;
    //public string crossingType;

    [Header("Car Lights ")]
    public GameObject frontLights;
    public GameObject leftLights;
    public GameObject rightLights;
    public GameObject backLights;

    [Header("Audio")]
    public AudioSource roadSound;
    public AudioSource idleSound;
    public AudioSource hornSound;


    private Vector3 lastPosition;
    Vector3 velocity;

    // For logging
    void Start()
    {
        InvokeRepeating("LogLocationAndRotation", 0f, 0.5f);  //0s delay, repeat every 0.5s
        roadSound.enabled = true;
        idleSound.enabled = true;
        hornSound.enabled = true;
    }

    void ReactToEnvironment()
    {
        playerDistance = Vector3.Distance(player.transform.position, transform.position);
        // Stop if near player
        if (carDetector.isUp == true && playerDetector.isUsingOrFacingroad == true)
        {
            if (isStopping == false)
            {
                if (!playerDetector.isUsingCrossing)
                {
                    hornSound.Play();
                }
                isStopping = true;
            }
            backLights.SetActive(true);
            if (movementSpeed != 0)
            {
                movementSpeed -= setSpeed*setSpeed/2/(playerDistance/Time.deltaTime);
                roadSound.volume = movementSpeed/setSpeed;
                if (movementSpeed < 0)
                {
                    movementSpeed = 0;
                }
            }
        }
        // Slow down if turning
        else if (isTurningLeft == true || isTurningRight == true)
        {
            if (waypointNavigator.nextWaypoint != null)
            {
                if (waypointNavigator.nextWaypoint.name != "End" && waypointNavigator.currentWaypoint.name != "End")
                {
                    if (movementSpeed > (0.5 * setSpeed))
                    {
                        backLights.SetActive(true);
                        movementSpeed -= setSpeed/5 * Time.deltaTime;
                        roadSound.volume = movementSpeed / setSpeed;
                        isStopping = true;
                    }
                    else if (movementSpeed < (0.5 * setSpeed))
                    {
                        movementSpeed = 0.5f * setSpeed;
                        isStopping = false;
                        backLights.SetActive(false);
                        roadSound.volume = movementSpeed / setSpeed;
                    }
                }
                else
                {
                    if (movementSpeed < setSpeed)
                    {
                        movementSpeed += setSpeed/4 * Time.deltaTime;
                        roadSound.volume = movementSpeed / setSpeed;
                    }
                    else if (movementSpeed > setSpeed)
                    {
                        movementSpeed = setSpeed;
                        isStopping = false;
                        roadSound.volume = movementSpeed / setSpeed;
                    }
                }
            }
        }
        else
        {
            isStopping = false;
            backLights.SetActive(false);
            if (movementSpeed != setSpeed)
            {
                movementSpeed += setSpeed/3 * Time.deltaTime;
                roadSound.volume = movementSpeed / setSpeed;
                if (movementSpeed > setSpeed)
                {
                    movementSpeed = setSpeed;
                }
            }
        }
    }

    void RotateWheels()
    {
        foreach (var wheel in carWheels)
        {
            wheel.transform.Rotate(movementSpeed * movementSpeed * movementSpeed * Time.deltaTime, 0, 0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        ReactToEnvironment();

        if (transform.position!= destination)
        {
            Vector3 destinationDirection = destination - transform.position;
            destinationDirection.y = 0;
            float destinationDistance = destinationDirection.magnitude;
            if (destinationDistance >= stopDistance)
            {
                reachedDestination = false;
                Quaternion targetRotation = Quaternion.LookRotation(destinationDirection);
                if (transform.rotation != targetRotation)
                {
                    if (invisRotationSpeed < rotationSpeed)
                    {
                        invisRotationSpeed += Time.deltaTime * rotationSpeed;
                    }
                    transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, invisRotationSpeed * Time.deltaTime);
                }
                else
                {
                    if (invisRotationSpeed > 0)
                    {
                        invisRotationSpeed -= Time.deltaTime * rotationSpeed;
                    }
                }
                transform.Translate(Vector3.forward * movementSpeed * Time.deltaTime);
            }
            else
            {
                reachedDestination = true;
            }

            velocity = (transform.position - lastPosition) / Time.deltaTime;
            velocity.y = 0;
            var velocityMagnitude = velocity.magnitude;
            velocity = velocity.normalized;
            var fwdDotProduct = Vector3.Dot(transform.forward, velocity);
            var rightDotProduct = Vector3.Dot(transform.right, velocity);

        }
        RotateWheels();
        lastPosition = transform.position;
    }

    public void OperateIndicatorLights(string dir, string command)
    {
        if (command == "on")
        {
            if (dir == "left")
            {
                leftLights.SetActive(true);
            }
            else if (dir == "right")
            {
                rightLights.SetActive(true);
            }
        }
        else if (command == "off")
        {
            if (dir == "left")
            {
                leftLights.SetActive(false);
            }
            else if (dir == "right")
            {
                rightLights.SetActive(false);
            }
        }
    }

    void LogLocationAndRotation()
    {
        sceneController.WriteToCSV("Poll", "Vehicle Location", "\"Location: " + this.gameObject.transform.position + " Rotation: " + this.gameObject.transform.rotation + "\"");
    }

    public void LogDespawn()
    {
        sceneController.WriteToCSV("Event", "Vehicle Spawn", "Vehicle despawned.");
    }

    public void SetDestination(Vector3 destination)
    {
        this.destination = destination;
        reachedDestination = false;
    }
}
