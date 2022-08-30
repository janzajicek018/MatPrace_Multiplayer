using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class EnemySpawner : NetworkBehaviour
{
    [SerializeField] private GameObject pfEnemy;

    // Start is called before the first frame update
    void Start()
    {
        //SpawnEnemy();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SpawnEnemy()
    {
        var enemy = Instantiate(pfEnemy, gameObject.transform);

        NetworkServer.Spawn(enemy);
    }
}
