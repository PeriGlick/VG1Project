using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;


public class StandController : MonoBehaviour
{
    // Start is called before the first frame update

    //public float standHealth = 20f; commented out to try healthbar code
    public int standHealth;
    public Slider slider;
    public Text healthText;
    public int maxStandHealth;
    public Image lemonSliceHealth;

    void Start()
    {
        maxStandHealth = 100;
        standHealth = maxStandHealth;
    }

    // Update is called once per frame
    void Update()
    {
        slider.value = standHealth;
        //lemonSliceHealth.fillAmount = standHealth / maxStandHealth;
        healthText.text = standHealth.ToString(); //check caps 
        if (standHealth <= 0f) {
            endGame();
        }
    }

    void endGame()
    {
        FindObjectOfType<gameManager>().GameOver();
    }

    public void TakeDamage(int damageAmount)
    {
        standHealth -= damageAmount;
        float sh = standHealth;
        float msh = maxStandHealth;
        lemonSliceHealth.fillAmount = sh / msh;
        if (standHealth < 0f)
        {
            endGame();
        }

    }
}
