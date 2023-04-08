using LemonAidMan;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Windows.Speech;

public class Upgrades : MonoBehaviour
{
    // Start is called before the first frame

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
        
        
    }

    // Update is called once per frame
    void Update()
    {
        /*kuu = dks.upgradeCount;
        if (dks.upgradesAvailable >= 1)
        {

        }*/

    }
    //In here I'd also like to add a general healing feature with cost based on how low your health is - something the player can use at any time with any cost.

    //ideally I'd like to make all of these usable for all levels of upgrades, but I have to figure out how to organize these guys into an array
    public void speedOneUpgrade()
    {
        float upgradeCost = 0.5f;
        if(gameManager.instance.bank >= upgradeCost)
        {
            gameManager.instance.bank -= upgradeCost;
            player.GetComponent<playerMovement>().moveSpeed = player.GetComponent<playerMovement>().moveSpeed * 1.5f;
            speedOne.interactable = false;
            speedTwo.interactable = true;
            Debug.Log("Speed Upgrade Working");
        }
        
    }

    public void moneyOneUpgrade()
    {
        float upgradeCost = 1f;
        if (gameManager.instance.bank >= upgradeCost)
        {
            gameManager.instance.bank -= upgradeCost;
            gameManager.instance.lemonadeCost = gameManager.instance.lemonadeCost * 2;
            moneyOne.interactable = false;
            moneyTwo.interactable = true;
        }
    }

    public void healthOneUpgrade()
    {
        float upgradeCost = 1f;
        if (gameManager.instance.bank >= upgradeCost)
        {
            gameManager.instance.bank -= upgradeCost;
            player.GetComponent<playerHealth>().maxHealth = player.GetComponent<playerHealth>().maxHealth* 2;
            player.GetComponent<playerHealth>().currentHealth = player.GetComponent<playerHealth>().maxHealth;
            healthOne.interactable = false;
            healthTwo.interactable = true;
        }
    }

    public void standOneUpgrade()
    {
        float upgradeCost = 1.5f;
        if (gameManager.instance.bank >= upgradeCost)
        {
            gameManager.instance.bank -= upgradeCost;
            stand.GetComponent<StandController>().maxStandHealth = stand.GetComponent<StandController>().maxStandHealth* 2;
            stand.GetComponent<StandController>().standHealth = stand.GetComponent<StandController>().maxStandHealth;
            standOne.interactable = false;
            standTwo.interactable = true;
        }
    }

    public void customerMoveOneUpgrade()
    {
        float upgradeCost = 0.5f;
        if (gameManager.instance.bank >= upgradeCost)
        {
            gameManager.instance.bank -= upgradeCost;
            gameManager.instance.customerMoveSpeed = gameManager.instance.customerMoveSpeed * 1.5f;
            customerMoveOne.interactable = false;
            customerMoveTwo.interactable = true;
        }
    }

    //another upgrade Idea

    /*void advertisementUpgrade()
    {

    }*/
}
