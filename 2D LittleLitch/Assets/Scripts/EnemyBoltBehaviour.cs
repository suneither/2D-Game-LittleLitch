﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBoltBehaviour : MonoBehaviour
{

    public float speed;
    public float damage;

    private Rigidbody2D rb;

    Vector2 moveDirection;
    private Transform target;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        Vector3 difference = target.position - transform.position;
        float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotZ + 90);
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(difference.x, difference.y).normalized * speed;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name.Equals("Player"))
        {
            other.gameObject.GetComponent<PlayerMovement>().health -= damage;
        }
        else
        {

        }
    }
}
