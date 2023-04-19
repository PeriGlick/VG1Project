using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class LemonadeHeal : MonoBehaviour
{
    public Button glass;
    public Button pitcher;
    public GameObject player;
    public Text glassCost;
    public Text pitcherCost;
    public GameObject stand;
    
    void Start()
    {
        glass.onClick.AddListener(glassHeal);
        pitcher.onClick.AddListener(pitcherHeal);
    }

    void Update()
    {
        glassCost.text = "$"+gameManager.instance.lemonadeCost.ToString("F2");
        pitcherCost.text = "$"+(gameManager.instance.lemonadeCost*5).ToString("F2");
        if (player.GetComponent<playerHealth>().currentHealth == player.GetComponent<playerHealth>().maxHealth)
        {
            glass.interactable = false;
        }
        else
        {
            glass.interactable = true;
        }
        if (stand.GetComponent<StandController>().standHealth == stand.GetComponent<StandController>().maxStandHealth)
        {
            pitcher.interactable = false;
        }
        else
        {
            pitcher.interactable= true;
        }
    }

    public void glassHeal()
    {
        float cost = gameManager.instance.lemonadeCost/2;
        if (gameManager.instance.bank >= cost)
        {
            gameManager.instance.bank -= cost;
            player.GetComponent<playerHealth>().currentHealth = player.GetComponent<playerHealth>().currentHealth + (player.GetComponent<playerHealth>().maxHealth/10);
            SoundFxManager.instance.PlayHealSound();
        }
    }

    public void pitcherHeal()
    {
        float cost = ((gameManager.instance.lemonadeCost * 5)/2);
        if (gameManager.instance.bank >= cost)
        {
            gameManager.instance.bank -= cost;
            stand.GetComponent<StandController>().standHealth = stand.GetComponent<StandController>().standHealth + (stand.GetComponent<StandController>().maxStandHealth/ 5);
            SoundFxManager.instance.PlayHealSound();
        }
    }
}
