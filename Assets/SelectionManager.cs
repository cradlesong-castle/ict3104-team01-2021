using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionManager : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        Vector3 centerScreen; 
        centerScreen = new Vector3(0.5f*Screen.width, 0.5f * Screen.height, 0f);
        var ray = Camera.main.ScreenPointToRay(centerScreen);
        RaycastHit hit;
        Debug.DrawRay(Camera.main.transform.position, transform.forward * 20, Color.green);
        if (Physics.Raycast(ray, out hit))
        {
            var selection = hit.transform;
            Debug.Log(selection.name);
        }

        //if (Input.GetMouseButtonDown(0))
        //{
        //    PointerEventData pointerData = new PointerEventData(EventSystem.current);

        //    pointerData.position = Input.mousePosition;

        //    List<RaycastResult> results = new List<RaycastResult>();
        //    EventSystem.current.RaycastAll(pointerData, results);

        //    if (results.Count > 0)
        //    {
        //        WorldUI is my layer name
        //        if (results[0].gameObject.layer == LayerMask.NameToLayer("UI"))
        //        {
        //            string dbg = "Root Element: {0} \n GrandChild Element: {1}";
        //            Debug.Log(string.Format(dbg, results[results.Count - 1].gameObject.name, results[0].gameObject.name));
        //            Debug.Log("Root Element: "+results[results.Count-1].gameObject.name);
        //            Debug.Log("GrandChild Element: "+results[0].gameObject.name);
        //            results.Clear();
        //        }
        //    }
        //}
    }
}
