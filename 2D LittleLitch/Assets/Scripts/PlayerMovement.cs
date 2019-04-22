using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float movementSpeed;

    private Vector2 direction;
    private Rigidbody2D rb;

    private float nextDashTime = 0f;
    public float dashCooldown;

    public float dashSpeed;
    private float dashTime;
    public float startDashTime;
    public float health;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        dashTime = startDashTime;
    }

    void Update ()
    {
        GetInput();
        Move();
        Roll();
	}

    private void GetInput()
    {
        direction = Vector2.zero;
        if(Input.GetKey(KeyCode.W))
        {
            direction += Vector2.up;
        }
        if (Input.GetKey(KeyCode.A))
        {
            direction += Vector2.left;
        }
        if (Input.GetKey(KeyCode.S))
        {
            direction += Vector2.down;
        }
        if (Input.GetKey(KeyCode.D))
        {
            direction += Vector2.right;
        }
    }
    private void Move()
    {
        rb.velocity = new Vector2(direction.x, direction.y) * movementSpeed;
    }
    private void Roll()
    {
        if (dashTime <= 0f)
        {
            dashTime = startDashTime;
            rb.velocity = Vector2.zero;
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (Time.time > nextDashTime)
                {
                    nextDashTime = Time.time + dashCooldown;
                    rb.velocity = new Vector2(direction.x, direction.y) * dashSpeed;
                }
            }
            dashTime -= Time.deltaTime;
        }
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.gameObject.layer == LayerMask.NameToLayer("InnerWall"))
        {
            movementSpeed = movementSpeed - 3;
            print("touched the wall");
        }
    }
}
