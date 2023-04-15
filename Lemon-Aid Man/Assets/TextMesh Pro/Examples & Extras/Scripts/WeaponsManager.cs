using LemonAidMan;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
public class WeaponsManager : MonoBehaviour
{
    public Button LG;
    public Button SS;
    public Button LS;
    public GameObject DuckKillMonitor;
    DuckKillScript dks;
    bool unlockSS;
    bool unlockLS;
    public int currentKills;
    public int SScap = 15;
    public int LScap = 30;
    public GameObject player;
    public GameObject LemonGrenadePrefab;
    public GameObject SeedSpitterPrefab;
    public GameObject LemonadeSplashPrefab;
    public Text untilSS;
    public Text untilLS;
    public int tilSS;
    public int tilLS;

   
    // Start is called before the first frame update
    void Start()
    {
        LG.onClick.AddListener(LemonGrenade);
        SS.onClick.AddListener(SeedSpitter);
        LS.onClick.AddListener(LemonadeSplash);
        SS.interactable = false;
        LS.interactable = false;
        DuckKillMonitor = GameObject.Find("DuckKillMonitor");
        dks = DuckKillMonitor.GetComponent<DuckKillScript>();
        currentKills = dks.duckKills;
        tilSS = SScap - currentKills;
        tilLS = LScap - currentKills;
        
    }

    // Update is called once per frame
    void Update()
    {
        tilSS = SScap - currentKills;
        tilLS = LScap - currentKills;
        untilSS.text = "Kills Until Unlock: " + tilSS;
        untilLS.text = "Kills Until Unlock: " + tilLS;
        if (currentKills >= SScap)
        {
            unlockSS = true;
        }
        if (currentKills >= LScap)
        {
            unlockLS = true;
        }
        if (unlockSS)
        {
            SS.interactable = true;
            untilSS.gameObject.SetActive(false);
        }

        if (unlockLS)
        {
            LS.interactable = true;
            untilLS.gameObject.SetActive(false);
        }
        
    }

    public void LemonGrenade()
    {
        player.GetComponent<playerMovement>().projectilePrefab = LemonGrenadePrefab;
        LG.interactable = false;
        if (unlockSS)
        {
            SS.interactable = true;
        }
        if (unlockLS)
        {
            LS.interactable = true;
        }
    }

    public void SeedSpitter()
    {
        if (unlockSS)
        {
            player.GetComponent<playerMovement>().projectilePrefab = SeedSpitterPrefab;
            SS.interactable = false;
            LG.interactable = true;
            if (unlockLS)
            {
                LS.interactable = true;
            }
        }
    }

    public void LemonadeSplash()
    {
        if (unlockLS)
        {
            player.GetComponent<playerMovement>().projectilePrefab = LemonadeSplashPrefab;
            LS.interactable = false;
            LG.interactable = true;
            SS.interactable = true;
        }
    }
}
