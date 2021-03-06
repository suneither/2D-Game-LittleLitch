﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{

    public float speed;
    public float distanceFromPlayer;
 
    private Transform target;
    public float distanceBeetweenPlayer;

    // Use this for initialization
    void Start () {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        distanceBeetweenPlayer = Vector2.Distance(transform.position, target.position);
        if (distanceBeetweenPlayer > distanceFromPlayer)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        }
    }
}
