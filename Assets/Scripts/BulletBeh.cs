using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBeh : MonoBehaviour
{
    public float speed;

    public int bulletDamage;

    public GameObject[] Blood = new GameObject[8];

    Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        rb.velocity = -transform.up * speed;
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Zombie")
        {
            Instantiate(Blood[Random.Range(0,7)], new Vector3(transform.position.x, transform.position.y, transform.position.z+100), transform.rotation);
        }
    }
}
