using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemySpawner : MonoBehaviour
{
    public float spawnRate;
    public float spawnCounter;
    public GameObject prefab;


    void Start()
    {
        
    }

    void Update()
    {
        if (spawnCounter <= 0)
        {
            GameObject DuckEnemy = Instantiate(prefab, transform.position, Quaternion.identity);

            spawnCounter = spawnRate;
        }

        else
        {
            spawnCounter -= Time.deltaTime;
        }



    }
}
