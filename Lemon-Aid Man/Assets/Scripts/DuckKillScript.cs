using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuckKillScript : MonoBehaviour
{
    // Start is called before the first frame update

    public int duckKills;
    void Start()
    {
        duckKills = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void addKill()
    {
        duckKills++;
    }
}
