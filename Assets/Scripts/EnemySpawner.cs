using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using System;

public class EnemySpawner : NetworkBehaviour
{   
    [SerializeField] private GameObject pfEnemy;
    private BoxCollider2D boxCollider;
    private Bounds colliderBounds;
    private Vector3 colliderCenter;
    private int enemyCap = 20;
    [SerializeField]
    public int enemyCount = 0;
    public int totalCount = 0;
    public float timerMax = 2;
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
            
            spawnTimer = UnityEngine.Random.Range(.1f, timerMax);
            if (enemyCount < enemyCap)
            {
                Debug.Log(timerMax);
                SpawnEnemy();
                enemyCount++;
                totalCount++;
                if(totalCount % 20 == 0)
                {
                    timerMax = (float)Math.Pow(totalCount, -0.5) + 1;

                }
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
        float randomX = UnityEngine.Random.Range(floats[0], floats[1]);
        float randomY = UnityEngine.Random.Range(floats[2], floats[3]);
        Vector3 spawnPos = new Vector3(randomX, randomY, 0);
       
        randomX = randomX - UnityEngine.Random.Range(floats[0], floats[1])/2;
        randomY = randomY - UnityEngine.Random.Range(floats[2], floats[3])/2;
        Vector3 destination = new Vector3(-randomX, -randomY);

        var enemy = Instantiate(pfEnemy, spawnPos, Quaternion.identity);

        enemy.GetComponent<EnemyController>().SetDestination(destination);
        if(NetworkServer.activeHost)
            NetworkServer.Spawn(enemy);
    }
}
