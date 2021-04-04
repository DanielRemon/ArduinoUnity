using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileShooter : MonoBehaviour
{
    ReceiveOrdersFromArdiuno m_receiveOrderForArduino;

    public Transform TriggerPoint;

    public GameObject BulletPrefab;

    public float forceBullet;

    public int bulletLengt;

    string bulletState;

    bool PressButtonShoot = false;
    bool finishInstanciateBullet = true;


    private void Awake()
    {
        m_receiveOrderForArduino = GetComponent<ReceiveOrdersFromArdiuno>();

    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        /*if (Input.GetKeyDown("space"))
        {
            print("space key was pressed");
            ShootBullet();
        }*/

        if (PressButtonShoot && finishInstanciateBullet)
        {
            finishInstanciateBullet = false;
            StartCoroutine("ShootBulletTime");
        }
    }



    IEnumerator ShootBulletTime()
    {
        if (bulletLengt >= 0)
        {
            InstanciateBullet();
            

            //bulletState
            switch (bulletLengt)
            {
                case 0:
                    bulletState = "Nothing";
                    m_receiveOrderForArduino.SendBulletState(bulletState);
                    break;
                case 2:
                    bulletState = "VeryLow";
                    m_receiveOrderForArduino.SendBulletState(bulletState);
                    break;
                case 4:
                    bulletState = "Low";
                    m_receiveOrderForArduino.SendBulletState(bulletState);
                    break;
                case 6:
                    bulletState = "Medium";
                    m_receiveOrderForArduino.SendBulletState(bulletState);
                    break;
                case 8:
                    bulletState = "High";
                    m_receiveOrderForArduino.SendBulletState(bulletState);
                    break;
                case 10:
                    bulletState = "Complete";
                    m_receiveOrderForArduino.SendBulletState(bulletState);
                    break;
            }
            bulletLengt--;

        }
        if(bulletLengt < 0)
        {
            yield return new WaitForSeconds(0.5f);
            m_receiveOrderForArduino.SendBulletState("Complete");
            bulletLengt = 10;
        }
        yield return new WaitForSeconds(0.5f);
        PressButtonShoot = false;
        finishInstanciateBullet = true;
    }

    public void ShootBullet()
    {
        PressButtonShoot = true;

    }
     void InstanciateBullet()
    {
        GameObject bullet = GameObject.Instantiate(BulletPrefab);
        bullet.transform.position = TriggerPoint.position;
        bullet.transform.rotation = TriggerPoint.rotation;
        bullet.GetComponent<Rigidbody>().AddForce(bullet.transform.forward * forceBullet);
        Destroy(bullet, 2);
    }

}
