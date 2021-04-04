using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReceiveOrdersFromArdiuno : MonoBehaviour
{
    public ConnectToArduino m_conexionArduino;
    ProjectileShooter m_projectileShooter;
    NaveRotationController m_NaveRotationController;
    float deltaTime = 0.0f;
    private void Awake()
    {
        m_NaveRotationController = GetComponent<NaveRotationController>();
        m_projectileShooter = GetComponent<ProjectileShooter>();
            
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;
    }
    void OnGUI()
    {
        int w = Screen.width, h = Screen.height;

        GUIStyle style = new GUIStyle();

        Rect rect = new Rect(0, 0, w, h * 2 / 100);
        style.alignment = TextAnchor.UpperLeft;
        style.fontSize = h * 2 / 100;
        style.normal.textColor = new Color(0.0f, 0.0f, 0.5f, 1.0f);
        float msec = deltaTime * 1000.0f;
        float fps = 1.0f / deltaTime;
        string text = string.Format("{0:0.0} ms ({1:0.} fps)", msec, fps);
        GUI.Label(rect, text, style);
    }

    public void Shoot()
    {
        m_projectileShooter.ShootBullet();
    }
    public void ResetRotation()
    {
        m_NaveRotationController.ResetRotation();
    }
    public void SetRotationData(string x, string y,string z)
    {
        m_NaveRotationController.SetRotationNave(x,y,z);
    }

    public void SendBulletState(string bulletLengt)
    {
        m_conexionArduino.SendDataToArduino(bulletLengt);
    }
}

