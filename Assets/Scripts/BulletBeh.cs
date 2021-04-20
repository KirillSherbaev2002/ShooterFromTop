using UnityEngine;
using UnityEngine.UI;

public class BulletBeh : MonoBehaviour
{
    public float speed;

    public int bulletDamage;

    public GameObject[] Blood = new GameObject[8];

    public Sprite[] Bullets = new Sprite[3];

    Rigidbody2D rb;

    public int typeOfBullet;
    void OnEnable()
    {
        rb = GetComponent<Rigidbody2D>();
        typeOfBullet = Random.Range(0,3);
        gameObject.GetComponent<SpriteRenderer>().sprite = Bullets[typeOfBullet];
    }
    void Update()
    {
        rb.velocity = -transform.up * speed;
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Zombie")
        {
            Instantiate(Blood[Random.Range(0,7)], new Vector3(transform.position.x, transform.position.y, transform.position.z+100), transform.rotation);
        }
    }
}
