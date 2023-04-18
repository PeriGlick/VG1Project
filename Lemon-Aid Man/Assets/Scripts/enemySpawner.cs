using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemySpawner : MonoBehaviour
{
    public GameObject[] ducks;
    public GameObject leftCorner;
    public GameObject rightCorner;

    public float maxSpawnRate;
    public float[] minSpawnRates;
    float currentMinSpawnRate;

    public int[] meleesInAWave;
    public int[] rangersInAWave;
    int currMeleesInAWave;
    int currRangersInAWave;
    int currDucksInAWave;
    int meleesSoFar = 0;
    int rangersSoFar = 0;
    int ducksSoFar = 0;
    public float timeBetweenWave;
    int currentWave = 0;
    public AnimationCurve spawnCurve;

    bool canSpawn = true;
    bool startWave = true;


    void Start()
    {
        // spawnRate = initialSpawnRate;
        currentMinSpawnRate = minSpawnRates[currentWave];
        currMeleesInAWave = meleesInAWave[currentWave];
        currRangersInAWave = rangersInAWave[currentWave];
        currDucksInAWave = currMeleesInAWave + currRangersInAWave;
    }

    void Update()
    {
        if (startWave && canSpawn && ducksSoFar < currDucksInAWave)
        {
            var spawnPointX = UnityEngine.Random.Range(leftCorner.transform.position.x, rightCorner.transform.position.x);
            var spawnPoint = new Vector3(spawnPointX, leftCorner.transform.position.y, leftCorner.transform.position.z);
            GameObject DuckEnemy = Instantiate(GetRandomDuck(), spawnPoint, Quaternion.identity);
            Debug.Log(ducksSoFar + " melee: " + meleesSoFar + " ranger: " + rangersSoFar);
            Debug.Log("spawn: " + currentMinSpawnRate + " ducks in this wave:" + currDucksInAWave);
            // Debug.Log(spawnCurve.Evaluate(.3f));
            StartCoroutine(spawnWait()); 
            canSpawn = false;
        }

        if(ducksSoFar >= currDucksInAWave) {
            ducksSoFar = 0;
            meleesSoFar = 0;
            rangersSoFar = 0;
            StartCoroutine(waveWait()); 
            startWave = false;
        } 
    }

    GameObject GetRandomDuck() {
        int index = UnityEngine.Random.Range(0, 2);
        ducksSoFar ++;
        if(index == 0) {
            if(rangersSoFar < currRangersInAWave) {
                rangersSoFar ++;
                return ducks[0];
            }
            meleesSoFar ++;
            return ducks[1];
        } else if(meleesSoFar < currMeleesInAWave) {
            meleesSoFar ++;
            return ducks[1];
        }
        rangersSoFar ++;
        return ducks[0];
    }

    IEnumerator waveWait() {
        yield return new WaitForSeconds(timeBetweenWave);
        if(currentWave < minSpawnRates.Length - 1) {
            currentWave ++;
        }
        currentMinSpawnRate = minSpawnRates[currentWave];
        currMeleesInAWave = meleesInAWave[currentWave];
        currRangersInAWave = rangersInAWave[currentWave];
        currDucksInAWave = currMeleesInAWave + currRangersInAWave;
        startWave = true;
    }
    IEnumerator spawnWait() {
        yield return new WaitForSeconds((spawnCurve.Evaluate((float)ducksSoFar/(float)(currDucksInAWave - 1)) * (maxSpawnRate - currentMinSpawnRate)) + currentMinSpawnRate);
        canSpawn = true;
    }
}
