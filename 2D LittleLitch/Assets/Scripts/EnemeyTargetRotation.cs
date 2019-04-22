using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemeyTargetRotation : MonoBehaviour {

    private Transform target;

    // Use this for initialization
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if(target == true)
        {
            Vector3 difference = target.position - transform.position;
            difference.Normalize();

            float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, rotZ);
        }
       
    }
}
