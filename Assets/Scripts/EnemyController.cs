using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class EnemyController : NetworkBehaviour
{
    // Start is called before the first frame update
    private int health = 1;
    private float speed = 3f;
    private int damage = 1;
    private int score;
    private bool alreadyHit = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //get pos of closest player
        var player = FindObjectOfType<PlayerController>();
        if (player != null)
        {
            Vector3 direction = player.transform.position - transform.position;
            transform.right = direction;
        }
        GetComponent<Rigidbody2D>().velocity = transform.right * speed;

    }
    [Server]
    public void TakeDamage(int damage)
    {
        health -= damage;
        if(health <= 0)
        {
            gameObject.SetActive(false);
        }
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
}
