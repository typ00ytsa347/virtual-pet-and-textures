using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
public class ArduinoControl : MonoBehaviour
{

    public List<int> touchValues;
    // Use this for initialization
    void Start()
    {
        touchValues = new List<int>() { 0, 0, 0, 0 };
    }
    // Update is called once per frame
    void Update()
    {
    }
    // Invoked when a line of data is received from the serial device.
    void OnMessageArrived(string msg)
    {
        //Debug.Log("Arrived:" + msg);

        string[] data = msg.Split(':');
        int number = int.Parse(data[1].Trim());
        print(number + data[0]);

        int index = int.Parse("" + data[0][1]) - 1;
        touchValues[index] = number;
      
    }
    // Invoked when a connect/disconnect event occurs. The parameter 'success'
    // will be 'true' upon connection, and 'false' upon disconnection or
    // failure to connect.
    void OnConnectionEvent(bool success)
    {
        Debug.Log(success ? "Device connected" : "Device disconnected");
    }
}