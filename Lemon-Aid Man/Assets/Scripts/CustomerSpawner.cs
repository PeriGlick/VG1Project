using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerSpawner : MonoBehaviour
{
    public float spawnRate;
    public float spawnCounter;
    public GameObject[] customerPrefabsList;
    
    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        if (spawnCounter <= 0) {
            int randomIndex = Random.Range(0, customerPrefabsList.Length);
            // GameObject Customer = Instantiate(customerPrefabsList[randomIndex], transform.position, Quaternion.identity);
            GameObject clone = Instantiate(customerPrefabsList[randomIndex], transform.position, Quaternion.identity);
            spawnCounter = spawnRate;
        } else {
            spawnCounter -= Time.deltaTime;
        }
    }
}
