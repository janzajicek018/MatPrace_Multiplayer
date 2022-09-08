using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class EnemySpawner : NetworkBehaviour
{
    public int EnemyCount { get { return enemyCount; } set { enemyCap = value; } } 

    [SerializeField] private GameObject pfEnemy;
    private BoxCollider2D boxCollider;
    private Bounds colliderBounds;
    private Vector3 colliderCenter;
    private int enemyCap = 20;
    private int enemyCount = 0;
    private float spawnTimer = 0;
    // Start is called before the first frame update
    void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        colliderBounds = boxCollider.bounds;
        colliderCenter = colliderBounds.center;
    }

    // Update is called once per frame
    void Update()
    {
        if(spawnTimer <= 0)
        {
            spawnTimer = Random.Range(.1f, 2);
            if(enemyCount < enemyCap)
            {
                SpawnEnemy();
                enemyCount++;
            }
        }
        spawnTimer -= Time.deltaTime;

        /*if (Input.GetKeyDown(KeyCode.X))
        {
            SpawnEnemy();
        }*/
    }
    [Server]
    public void SpawnEnemy()
    {
        float[] floats = { 
            colliderCenter.x - colliderBounds.extents.x,
            colliderCenter.x + colliderBounds.extents.x,
            colliderCenter.y - colliderBounds.extents.y,
            colliderCenter.y + colliderBounds.extents.y 
        };
        float randomX = Random.Range(floats[0], floats[1]);
        float randomY = Random.Range(floats[2], floats[3]);
        Vector3 spawnPos = new Vector3(randomX, randomY, 0);
       
        randomX = randomX - Random.Range(floats[0], floats[1])/2;
        randomY = randomY - Random.Range(floats[2], floats[3])/2;
        Vector3 destination = new Vector3(-randomX, -randomY);

        var enemy = Instantiate(pfEnemy, spawnPos, Quaternion.identity);

        enemy.GetComponent<EnemyController>().SetDestination(destination);

        NetworkServer.Spawn(enemy);
    }
}
