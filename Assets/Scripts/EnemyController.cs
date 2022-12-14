using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class EnemyController : NetworkBehaviour
{
    [SerializeField] private GameObject enemySpawner;
    [SyncVar] private int health = 1;
    private float speed = 3f;
    private int damage = 1;
    private bool alreadyHit = false;
    private Vector2 destination;
    void Start()
    {

    }

    void Update()
    {
        if(destination != null)
        {
            GetComponent<Rigidbody2D>().velocity = transform.right * speed;
        }
    }
    [Server]
    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            enemySpawner.GetComponent<EnemySpawner>().EnemyCount -= 1;
            Destroy(gameObject);
        }
    }
    public void SetDestination(Vector2 dest)
    {
        destination = dest;
        transform.right = destination;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            //without this player takes 2 damage instead of 1
            //possibly runs twice?
            if (alreadyHit) return;
            alreadyHit = true;
            gameObject.SetActive(false);
            collision.GetComponent<PlayerController>().TakeDamage(damage);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Spawner"))
        {
            //Destroy(gameObject);
            transform.right = -destination;
            GetComponent<Rigidbody2D>().velocity = transform.right * speed;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Enemy"))
        {
            Destroy(gameObject);
        }
    }
}
