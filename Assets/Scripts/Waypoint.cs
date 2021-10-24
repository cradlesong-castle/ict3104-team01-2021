using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{

    public Waypoint previousWaypoint;
    public Waypoint nextWaypoint;

    [Range(0f, 4f)]
    public float width = 1.6f;

    public Vector3 GetPosition()
    {


        //min bound and max defines how much degree of freedom object can move between following a waypoint
        Vector3 minBound = transform.position + transform.right * width / 2f;
        Vector3 maxBound = transform.position - transform.right * width / 2f;
        return Vector3.Lerp(minBound, maxBound, Random.Range(0f, 1f));
    }
}
