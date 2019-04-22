using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBoltController : MonoBehaviour {

    public float speed;
    private Rigidbody2D rb;
    public float damage;

	void Start ()
    {
        Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotZ+90);
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(difference.x, difference.y).normalized * speed;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Enemy")
        {
            col.GetComponent<EnemyMovement>().health -= damage;
            
            Destroy(gameObject);
        }
        if(col.gameObject.tag == "OuterWalls")
        {
            Destroy(gameObject);
        }
    }
}
