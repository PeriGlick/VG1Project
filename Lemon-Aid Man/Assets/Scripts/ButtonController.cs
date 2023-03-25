using System.Buffers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour
{

    public Button[] speedUpgrades;
    public Button[] attackUpgrades;
    public Button[] healthUpgrades;
    public Button[] standUpgrades;
    public Button[] moneyUpgrades;
    public int numOfUpgrades = 3;

   
    void Awake()
    {
        speedUpgrades= new Button[numOfUpgrades];
        attackUpgrades= new Button[numOfUpgrades];
        healthUpgrades= new Button[numOfUpgrades];
        standUpgrades= new Button[numOfUpgrades];
        moneyUpgrades= new Button[numOfUpgrades];

        /*for (int a = 1; a <= numOfUpgrades; a++)
        {
            Button speed = GameObject.Find("Speed" + a).GetComponent<Button>();
            Button attack = GameObject.Find("Attack" + a).GetComponent<Button>();
            Button health = GameObject.Find("Health" + a).GetComponent<Button>();
            Button stand = GameObject.Find("Stand" + a).GetComponent<Button>();
            Button money = GameObject.Find("Money" + a).GetComponent<Button>();
            speedUpgrades[a] = speed;
            attackUpgrades[a] = attack;
            healthUpgrades[a] = health;
            standUpgrades[a] = stand;
            moneyUpgrades[a] = money;
            if (a != 1)
            {
                speedUpgrades[a].interactable = false;
                attackUpgrades[a].interactable = false;
                healthUpgrades[a].interactable = false;
                standUpgrades[a].interactable = false;
                moneyUpgrades[a].interactable = false;
            }
        }*/
    }

    void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
