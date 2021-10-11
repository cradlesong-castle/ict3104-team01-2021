using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneController : MonoBehaviour
{
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

    /*
     * This controls the presence of intersections
     */
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
}
