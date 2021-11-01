using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class LoggingWriter : MonoBehaviour
{


    void writeToCSV(string timestamp, string logtype, string message)
    {
        String filepath = System.AppContext.BaseDirectory + "Logs\\logfile.csv";
        try
        {
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(@filepath, true))
            {
                file.WriteLine(timestamp + "," + logtype + "," + message);
            }
        }
        catch (Exception ex)
        {
            throw new ApplicationException("ERROR LOGGING ", ex);
        }
    }
}
