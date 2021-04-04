using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NaveRotationController : MonoBehaviour
{
    public float Xsensitivity;
    public float Ysensitivity;
    public float Zsensitivity;

    float g_x;
    float g_y;
    float g_z;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        /*if (Input.GetKeyDown("space"))
        {
            ResetRotation();
        }*/
    }

    public void ResetRotation()
    {
        g_x = 0;
        g_y = 0;
        g_z = 0;
    }
    
    public void SetRotationNave(string x, string y ,string z)
    {
        float joy = (float.Parse(x) - 500);
        if (joy > 50 || joy < -50)
        {
            g_x += joy;
        }
        if (float.Parse(y) > 500 || float.Parse(y) < -500)
        {
            g_y += (float.Parse(y) / 10000);
        }
        if (float.Parse(z) > 500 || float.Parse(z) < -500)
        {
            g_z += (float.Parse(z) / 10000);
        }

        
        //Debug.Log(float.Parse(x) - 500);

        //this.transform.rotation = Quaternion.Euler(-g_x * Xsensitivity, -g_y * Ysensitivity, -g_z * Zsensitivity);
        this.transform.rotation = Quaternion.Euler( -g_y * Ysensitivity, -g_z * Zsensitivity, -g_x * Xsensitivity);
        //this.transform.rotation = Quaternion.Lerp(this.transform.rotation, new Quaternion(float.Parse(x), float.Parse(y), float.Parse(z), 1.0f), Time.deltaTime * Xsensitivity);
    }

 

}
