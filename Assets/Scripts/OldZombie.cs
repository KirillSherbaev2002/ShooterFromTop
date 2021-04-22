using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using Pathfinding;

public class OldZombie : MonoBehaviour
{
    Animator anim;
    PlayerMove player;
    Rigidbody2D rb;

    public float[] speedScale = new float[3];
    public float speed;

    float[] DistanceScale = new float[2] { 2f, 15f };
    float distance;

    public Color[] LightColor = new Color[2];

    public float ZombieHP = 100;

    float ZombieStartHP;

    public Vector3 Start;
    public Transform StartTransform;

    public GameObject FireItself;

    public Image HPRateZombie;

    AIPath aiPath;
    AIDestinationSetter destinationSetter;

    void Awake()
    {
        aiPath = GetComponent<AIPath>();
        destinationSetter = GetComponent<AIDestinationSetter>();
        gameObject.GetComponent<SpriteRenderer>().color = LightColor[0];
        player = FindObjectOfType<PlayerMove>();
        rb = gameObject.GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        Start = transform.position;
        StartTransform = transform;
        ZombieStartHP = ZombieHP;
    }

    void FixedUpdate()
    {
        destinationSetter.target = player.transform;
        distance = Vector3.Distance(transform.position, player.transform.position);
        if (ZombieHP >= 0)
        {
            anim.SetTrigger("Stand");
        }
        if (distance <= DistanceScale[0])
        {
            print("Атака");
            Attack();
        }
        if (distance <= DistanceScale[1] && distance >= DistanceScale[0])
        {
            Follow();
        }
        if (distance >= DistanceScale[1])
        {
            if (Mathf.Round(transform.position.x) == Mathf.Round(Start.x) && (Mathf.Round(transform.position.y) == Mathf.Round(Start.y)))
            {
                Stand();
            }
            else
            {
                MoveBack();
            }
        }
    }
    public void Attack()
    {
        anim.SetTrigger("Attack");

        Vector3 zombiePoition = transform.position;
        Vector3 playerPosition = player.transform.position;

        Vector3 direction = destinationSetter.target.position - zombiePoition;

        Move(direction);
        speed = speedScale[2];
        Rotate(direction);
        AttackPlayer();
    }

    public void Follow()
    {
        aiPath.enabled = false;
        anim.SetTrigger("Follow");

        Vector3 zombiePoition = transform.position;
        Vector3 playerPosition = player.transform.position;

        Vector3 direction = destinationSetter.target.position - zombiePoition;
        Move(direction);
        speed = speedScale[1];
        Rotate(direction);
    }

    public void Stand()
    {
        aiPath.enabled = false;
        anim.SetTrigger("Stand");
        Vector3 playerPosition = player.transform.position;
        Vector3 zombiePoition = transform.position;

        Vector3 direction = destinationSetter.target.position - zombiePoition;
        Rotate(direction);

        speed = speedScale[0];
    }

    public void MoveBack()
    {
        anim.SetTrigger("Follow");
        Vector3 playerPosition = Start;
        aiPath.enabled = false;
        Vector3 zombiePoition = transform.position;

        Vector3 direction = StartTransform.position;
        Move(direction);
        speed = speedScale[1];
        Rotate(direction);
    }

    void Move(Vector3 direction)
    {
        aiPath.enabled = true;
    }
    void Rotate(Vector3 direction)
    {
        direction.z = 0;
        transform.up = -direction;
    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Light")
        {
            gameObject.GetComponent<SpriteRenderer>().color = LightColor[1];
        }
        if (collision.gameObject.tag == "Bullet")
        {
            ZombieHP -= 30;
            collision.gameObject.SetActive(false);
            if (collision.gameObject.GetComponent<BulletBeh>().typeOfBullet == 2)
            {
                ZombieHP -= 50;
                FireItself.SetActive(true);
            }
            if (ZombieHP <= 0)
            {
                anim.SetTrigger("Death");
            }
            HealthCheck();
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Light")
        {
            gameObject.GetComponent<SpriteRenderer>().color = LightColor[0];
        }
    }

    void HealthCheck()
    {
        HPRateZombie.fillAmount = ZombieHP / ZombieStartHP;
    }

    void DestroyItself()
    {
        player.PistolUpgradeCheck();
        player.ZombieKilled++;
        player.ZombieKilledText.text = player.ZombieKilled.ToString();
        Destroy(gameObject);
    }
    void AttackPlayer()
    {
        PlayerMove player;
        player = FindObjectOfType<PlayerMove>();

        if (distance <= 2f)
        {
            player.Damage();
        }
    }
}