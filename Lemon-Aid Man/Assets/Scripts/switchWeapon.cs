using LemonAidMan;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class switchWeapon : MonoBehaviour
{
    // Start is called before the first frame update

    public Button seedSpitterButton;
    public GameObject player;
    public GameObject seedSpitterPrefab;
    public DuckKillScript dks;
    public GameObject DuckKillMonitor;
    public int firstUnlock = 12;
    void Start()
    {
        seedSpitterButton.onClick.AddListener(changeWeaponSS);
        seedSpitterButton.interactable = false;
        DuckKillMonitor = GameObject.Find("DuckKillMonitor");
        dks = DuckKillMonitor.GetComponent<DuckKillScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (dks.duckKills >= firstUnlock)
        {
            seedSpitterButton.interactable = true;
        }
    }

    public void changeWeaponSS()
    {
        player.GetComponent<playerMovement>().projectilePrefab = seedSpitterPrefab;
        seedSpitterButton.interactable = false;

    }
}
