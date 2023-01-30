using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : NetworkBehaviour
{
    private int bulletDamage = 1;
    private float bulletSpeed = 12f;


    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody2D>().velocity = transform.right * bulletSpeed;

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Enemy"))
        {
            collision.transform.GetComponent<EnemyController>().TakeDamage(bulletDamage);
            Destroy(gameObject);
        }
        
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Spawner"))
        {
            Destroy(gameObject);
        }
    }
}
