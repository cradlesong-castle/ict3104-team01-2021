using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;

public class HandButton : XRBaseInteractable
{

    public GameObject[] lights;

    private float time = 0.0f;
    private float pedTime = 0.0f;

    public float greenLight = 0f;
    public float amberLight = 5f;
    public float redLight = 7f;
    public float restart = 15f;
    public float pedLimit = 10f;
    public bool isPed = false;
    private string state = "green";

    public UnityEvent OnPress = null;
    private float xMin = 0.0f;
    private float xMax = 0.0f;
    private bool previousPress = false;
    private bool isOn = false;

    /**

    lights
    0 - ped side green
    1 - ped side amber
    2 - ped side red
    3 - car side green
    4 - car side amber
    5 - car side red

    */
    //right side facing car
    private float previousHandHeight = 0.0f;

    private XRBaseInteractor hoverInteractor = null;
    protected override void Awake()
    {
        base.Awake();
        onHoverEntered.AddListener(StartPress);
        onHoverExited.AddListener(EndPress);
    }

    // Update is called once per frame
    void Update()
    {
      if (!isPed){
          print("normal");
        time += Time.deltaTime;
        if (time>= greenLight && time < amberLight)
        {

            lights[0].SetActive(true);
            lights[1].SetActive(false);
            lights[2].SetActive(false);
            lights[3].SetActive(false);
            lights[4].SetActive(false);
            lights[5].SetActive(false);

        }
        else if (time >= amberLight && time < redLight)
        {
            lights[0].SetActive(false);
            lights[1].SetActive(false);
            lights[2].SetActive(true);

        }
        else if (time >= redLight && time < restart)
        {
            lights[0].SetActive(false);
            lights[1].SetActive(true);
            lights[2].SetActive(false);

        }
        else if (time >= restart)
        {
            time = 0.0f;
        }

      }
      else{
          print(pedTime);
          pedTime += Time.deltaTime;

          if (pedTime>= greenLight && pedTime < amberLight)
          {

              lights[0].SetActive(false);
              lights[1].SetActive(true);
              lights[2].SetActive(false);
              lights[3].SetActive(false);
              lights[4].SetActive(false);
              lights[5].SetActive(true);

          }
          else if (pedTime > (amberLight/2) && pedTime < pedLimit)
          {
              lights[1].SetActive(false);
              lights[2].SetActive(true);
              lights[3].SetActive(true);


          }
          else{

                print("ped end");
            lights[0].SetActive(true);
            lights[3].SetActive(false);
            lights[4].SetActive(false);
            lights[5].SetActive(true);
            isPed = false;
            time = 0.0f;
          }
      }


    }
    private void OnDestroy()
    {
        onHoverEntered.AddListener(StartPress);
        onHoverExited.AddListener(EndPress);

    }

    private void StartPress(XRBaseInteractor interactor)
    {
        pedTime = 0.0f;
        isPed = true;

        hoverInteractor = interactor;
        previousHandHeight = GetLocalXPosition(hoverInteractor.transform.position);
    }
    private void EndPress(XRBaseInteractor interactor)
    {
        hoverInteractor = null;
        previousHandHeight = 0.0f;

        previousPress = false;
        SetXPosition(xMax);
    }


    private void Start()
    {
        SetMinMax();
    }

    private void SetMinMax()
    {
        Collider collider = GetComponent<Collider>();
        xMin = transform.position.x - (collider.bounds.size.x * 0.5f) - 9.5f;
        xMax = transform.position.x - 9.3f;
    }

    public override void ProcessInteractable(XRInteractionUpdateOrder.UpdatePhase updatePhase)
    {
        if (hoverInteractor)
        {
            float newHandHeight = GetLocalXPosition(hoverInteractor.transform.position);
            float handDifference = previousHandHeight - newHandHeight;
            previousHandHeight = newHandHeight;
            float newPosition = transform.localPosition.x - handDifference;
            SetXPosition(newPosition);
            CheckPress();

        }
    }

    private float GetLocalXPosition(Vector3 position)
    {
        Vector3 localposition = transform.root.InverseTransformPoint(position);
        return localposition.x;
    }

    private void SetXPosition(float position)
    {
        Vector3 newPosition = transform.localPosition;
        newPosition.x = Mathf.Clamp(position, xMin, xMax);
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
        float inRange = Mathf.Clamp(transform.localPosition.x, xMin, xMin + 0.01f);
        return transform.localPosition.x == inRange;
    }

}
