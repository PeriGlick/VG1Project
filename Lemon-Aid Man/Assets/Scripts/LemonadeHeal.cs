using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class LemonadeHeal : MonoBehaviour
{
    // Start is called before the first frame update

    public Button glass;
    public Button pitcher;
    public GameObject player;
    public Text glassCost;
    public Text pitcherCost;
    void Start()
    {

        glass.onClick.AddListener(glassHeal);
        pitcher.onClick.AddListener(pitcherHeal);

    }

    // Update is called once per frame
    void Update()
    {
        glassCost.text = "$"+gameManager.instance.lemonadeCost.ToString();
        pitcherCost.text = "$"+(gameManager.instance.lemonadeCost*5).ToString();
    }

    public void glassHeal()
    {
        float cost = gameManager.instance.lemonadeCost;
        if (gameManager.instance.bank >= cost)
        {
            gameManager.instance.bank -= cost;
            player.GetComponent<playerHealth>().currentHealth = player.GetComponent<playerHealth>().currentHealth + (player.GetComponent<playerHealth>().maxHealth/10);
        }

    }

    public void pitcherHeal()
    {
        float cost = (gameManager.instance.lemonadeCost * 5);
        if (gameManager.instance.bank >= cost)
        {
            gameManager.instance.bank -= cost;
            player.GetComponent<playerHealth>().currentHealth = player.GetComponent<playerHealth>().maxHealth;

        }
    }
}
