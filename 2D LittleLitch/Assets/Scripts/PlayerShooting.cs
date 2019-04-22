using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public float fireRate = 0f;

    float timeToFire = 0f;
    Transform firePoint;
    public GameObject fireBolt;

    void Awake ()
    {
        firePoint = transform.Find("FirePoint");
	}
	
	void Update ()
    {
	    if(fireRate == 0)
        {
            if (Input.GetMouseButton(0))
            {
                Shoot();
            }
        }
        else
        {
            if(Input.GetMouseButton(0) && Time.time > timeToFire)
            {
                timeToFire = Time.time + 1 / fireRate;
                Shoot();
            }
        }
	}

    void Shoot()
    {
        Instantiate(fireBolt, firePoint.transform.position, Quaternion.identity);
    }
}
