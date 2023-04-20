using LemonAidMan;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
//using UnityEngine.Windows.Speech;

public class Upgrades : MonoBehaviour
{
    public Button speedOne, customerMoveOne, healthOne, standOne, moneyOne;
    public Button speedTwo, customerMoveTwo, healthTwo, standTwo, moneyTwo;
    Button[] ones;
    Button[] twos;
    //public int[] x = new int[5];
    public GameObject player;
    public GameObject stand;
    DuckKillScript dks;
    int kuu;

    void Start()
    {
        //kuu = dks.upgradeCount;
        ones = new Button[] {speedOne, customerMoveOne, healthOne, standOne, moneyOne};
        twos = new Button[] {speedTwo,customerMoveTwo, healthTwo, standTwo,moneyTwo};
        speedOne.onClick.AddListener(speedOneUpgrade);
        moneyOne.onClick.AddListener(moneyOneUpgrade);
        healthOne.onClick.AddListener(healthOneUpgrade);
        standOne.onClick.AddListener(standOneUpgrade);
        customerMoveOne.onClick.AddListener(customerMoveOneUpgrade);
        speedTwo.onClick.AddListener(speedTwoUpgrade);
        moneyTwo.onClick.AddListener(moneyTwoUpgrade);
        healthTwo.onClick.AddListener(healthTwoUpgrade);
        standTwo.onClick.AddListener(standTwoUpgrade);
        customerMoveTwo.onClick.AddListener(customerMoveTwoUpgrade);
    }

    // Update is called once per frame
    void Update()
    {
        /*kuu = dks.upgradeCount;
        if (dks.upgradesAvailable >= 1)
        {

        }*/
    }
    
    public void speedOneUpgrade()
    {
        float upgradeCost = 0.125f;
        if(gameManager.instance.bank >= upgradeCost)
        {
            gameManager.instance.bank -= upgradeCost;
            player.GetComponent<playerMovement>().moveSpeed = player.GetComponent<playerMovement>().moveSpeed * 1.2f;
            speedOne.interactable = false;
            speedTwo.interactable = true;
            SoundFxManager.instance.PlayPowerupSound();
            Debug.Log("Speed Upgrade Working");
        }
    }

    public void moneyOneUpgrade()
    {
        float upgradeCost = 0.25f;
        if (gameManager.instance.bank >= upgradeCost)
        {
            gameManager.instance.bank -= upgradeCost;
            gameManager.instance.lemonadeCost = 0.10f;
            moneyOne.interactable = false;
            moneyTwo.interactable = true;
            SoundFxManager.instance.PlayPowerupSound();
        }
    }

    public void healthOneUpgrade()
    {
        float upgradeCost = 0.125f;
        if (gameManager.instance.bank >= upgradeCost)
        {
            gameManager.instance.bank -= upgradeCost;
            player.GetComponent<playerHealth>().maxHealth = player.GetComponent<playerHealth>().maxHealth + 75f;
            player.GetComponent<playerHealth>().currentHealth = player.GetComponent<playerHealth>().maxHealth;
            healthOne.interactable = false;
            healthTwo.interactable = true;
            SoundFxManager.instance.PlayPowerupSound();
        }
    }

    public void standOneUpgrade()
    {
        float upgradeCost = 0.25f;
        if (gameManager.instance.bank >= upgradeCost)
        {
            gameManager.instance.bank -= upgradeCost;
            stand.GetComponent<StandController>().maxStandHealth += 50;
            stand.GetComponent<StandController>().standHealth = stand.GetComponent<StandController>().maxStandHealth;
            standOne.interactable = false;
            standTwo.interactable = true;
            SoundFxManager.instance.PlayPowerupSound();
        }
    }

    public void customerMoveOneUpgrade()
    {
        float upgradeCost = 0.125f;
        if (gameManager.instance.bank >= upgradeCost)
        {
            gameManager.instance.bank -= upgradeCost;
            CustomerSpawner.instance.spawnRate = CustomerSpawner.instance.spawnRate * .75f;
            customerMoveOne.interactable = false;
            customerMoveTwo.interactable = true;
            SoundFxManager.instance.PlayPowerupSound();
        }
    }

    public void speedTwoUpgrade()
    {
        float upgradeCost = 1f;
        if (gameManager.instance.bank >= upgradeCost)
        {
            gameManager.instance.bank -= upgradeCost;
            CustomerSpawner.instance.spawnRate = CustomerSpawner.instance.spawnRate * .75f;
            speedOne.interactable = false;
            speedTwo.interactable = true;
            SoundFxManager.instance.PlayPowerupSound();
            Debug.Log("Speed Upgrade Working");
            speedTwo.interactable = false;
        }

    }

    public void moneyTwoUpgrade()
    {
        float upgradeCost = 1.50f;
        if (gameManager.instance.bank >= upgradeCost)
        {
            gameManager.instance.bank -= upgradeCost;
            gameManager.instance.lemonadeCost = 0.20f;
            moneyOne.interactable = false;
            moneyTwo.interactable = true;
            SoundFxManager.instance.PlayPowerupSound();
            moneyTwo.interactable = false;
        }
    }

    public void healthTwoUpgrade()
    {
        float upgradeCost = 1.5f;
        if (gameManager.instance.bank >= upgradeCost)
        {
            gameManager.instance.bank -= upgradeCost;
            player.GetComponent<playerHealth>().maxHealth = player.GetComponent<playerHealth>().maxHealth + 150f;
            player.GetComponent<playerHealth>().currentHealth = player.GetComponent<playerHealth>().maxHealth;
            healthOne.interactable = false;
            healthTwo.interactable = true;
            SoundFxManager.instance.PlayPowerupSound();
            healthTwo.interactable = false;
        }
    }

    public void standTwoUpgrade()
    {
        float upgradeCost = 2f;
        if (gameManager.instance.bank >= upgradeCost)
        {
            gameManager.instance.bank -= upgradeCost;
            stand.GetComponent<StandController>().maxStandHealth += 100;
            stand.GetComponent<StandController>().standHealth = stand.GetComponent<StandController>().maxStandHealth;
            standOne.interactable = false;
            standTwo.interactable = true;
            SoundFxManager.instance.PlayPowerupSound();
            standTwo.interactable = false;
        }
    }

    public void customerMoveTwoUpgrade()
    {
        float upgradeCost = 1.5f;
        if (gameManager.instance.bank >= upgradeCost)
        {
            gameManager.instance.bank -= upgradeCost;
            gameManager.instance.customerMoveSpeed = gameManager.instance.customerMoveSpeed * 1.2f;
            customerMoveOne.interactable = false;
            customerMoveTwo.interactable = true;
            SoundFxManager.instance.PlayPowerupSound();
            customerMoveTwo.interactable = false;
        }
    }


}
