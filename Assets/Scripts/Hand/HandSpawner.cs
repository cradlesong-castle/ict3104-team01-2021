using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;

public class HandSpawner : XRBaseInteractable
{

    public GameObject carPrefab;
    public int spawnAmt;
    public GameObject waypoint;

    public UnityEvent OnPress = null;
    private float yMin = 0.0f;
    private float yMax = 0.0f;
    private bool previousPress = false;

    private float previousHandHeight = 0.0f;

    private XRBaseInteractor hoverInteractor = null;
    protected override void Awake()
    {
        base.Awake();
        onHoverEntered.AddListener(StartPress);
        onHoverExited.AddListener(EndPress);
    }

    private void OnDestroy()
    {
        onHoverEntered.AddListener(StartPress);
        onHoverExited.AddListener(EndPress);

    }

    private void StartPress(XRBaseInteractor interactor)
    {
        hoverInteractor = interactor;
        previousHandHeight = GetLocalYPosition(hoverInteractor.transform.position);
        Spawn();
    }


    private void Spawn()
    {
       int count = 0;
       GameObject obj = Instantiate(carPrefab);
       Transform child = waypoint.transform.GetChild(1);
       obj.GetComponent<WaypointNavigator>().currentWaypoint = child.GetComponent<Waypoint>();
       obj.transform.position = child.position;
    }
    private void EndPress(XRBaseInteractor interactor)
    {
        hoverInteractor = null;
        previousHandHeight = 0.0f;

        previousPress = false;
        SetYPosition(yMax);
    }


    private void Start()
    {
        SetMinMax();
    }

    private void SetMinMax()
    {
        Collider collider = GetComponent<Collider>();
        yMin = transform.position.y - (collider.bounds.size.y * 0.5f);
        yMax = transform.position.y;
    }

    public override void ProcessInteractable(XRInteractionUpdateOrder.UpdatePhase updatePhase)
    {
        if (hoverInteractor)
        {
            float newHandHeight = GetLocalYPosition(hoverInteractor.transform.position);
            float handDifference = previousHandHeight - newHandHeight;
            previousHandHeight = newHandHeight;
            float newPosition = transform.localPosition.y - handDifference;
            SetYPosition(newPosition);

            CheckPress();

        }
    }

    private float GetLocalYPosition(Vector3 position)
    {
        Vector3 localposition = transform.root.InverseTransformPoint(position);
        return localposition.y;
    }

    private void SetYPosition(float position)
    {
        Vector3 newPosition = transform.localPosition;
        newPosition.y = Mathf.Clamp(position, yMin, yMax);
        transform.localPosition = newPosition;

    }
    private void CheckPress()
    {
        bool inPosition = InPosition();

        if (inPosition && inPosition != previousPress)
        {
             OnPress.Invoke();
        }
        previousPress = inPosition;
    }
    private bool InPosition()
    {
        float inRange = Mathf.Clamp(transform.localPosition.y, yMin, yMin + 0.01f);
        return transform.localPosition.y == inRange;
    }

}
