using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SampleMovement : MonoBehaviour
{
    public GameObject[] waypoints;
    int current = 0;
    float rotSpeed;
    public float speed;
    public Transform[] target;
    float WPradius = 1;

    void Update()
    {
        if(Vector3.Distance(waypoints[current].transform.position, transform.position) < WPradius)
        {
            current++;
            if (current >= waypoints.Length)
            {
                current = 0;
            }
        }

        Vector3 direction = target[current].position - transform.position;
        Quaternion rotation = Quaternion.LookRotation(direction);

        transform.position = Vector3.MoveTowards(transform.position, waypoints[current].transform.position, Time.deltaTime * speed);
        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, speed * Time.deltaTime);
    }
}
