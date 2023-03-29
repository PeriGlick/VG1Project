using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuckKillScript : MonoBehaviour
{
    // Start is called before the first frame update

    public int duckKills;
   // public Boolean upgradeReady;
    public int upgradeCount;
    public int upgradesAvailable;
    void Start()
    {
        duckKills = 0;
        upgradeCount = 5;
        upgradesAvailable = 0;
    }

    // Update is called once per frame
    void Update()
    {
      if (duckKills == upgradeCount) 
        {
            //upgradeReady = true;
            upgradesAvailable++;
            upgradeCount = Mathf.RoundToInt(upgradeCount*1.5f);
        }
    }

    public void addKill()
    {
        duckKills++;
    }
}
