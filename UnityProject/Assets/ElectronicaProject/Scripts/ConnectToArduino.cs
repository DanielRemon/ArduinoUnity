using System.Collections;
using System.Collections.Generic;
using System.IO.Ports;
using UnityEngine;

public class ConnectToArduino : MonoBehaviour
{
    //SerialPort stream = new SerialPort("COM3", 9600);
    public wrmhl device = new wrmhl();

    public ReceiveOrdersFromArdiuno m_receiveOrders;

    private void Awake()
    {
        //stream.Open();        
        device.set("COM3", 9600, 100, 1);
        device.connect();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        string value = device.readQueue();
        //print(value);

        if (value != null)
        {
            string[] arrayData = value.Split(',');
            //print(arrayData.Length);
            
            if (arrayData.Length==5) {
                if (arrayData[0] == "1")
                {
                    m_receiveOrders.Shoot();
                }
                m_receiveOrders.SetRotationData(arrayData[1], arrayData[2], arrayData[3]);
                if (arrayData[4] == "0")
                {
                    m_receiveOrders.ResetRotation();
                }
            }        
            
        }
               
    }


    public void SendDataToArduino(string text)
    {
        print(text);
        device.send(text);
    }

    private void OnApplicationQuit()
    {
        //stream.Close();
        device.close();
    }
   
}
