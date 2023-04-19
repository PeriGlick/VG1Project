using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerHealth : MonoBehaviour
{
    public float maxHealth;
    public float currentHealth;
    // public Image imageHealthBar;
    // public PlayerHealthbarBehavior healthBar;
    public Image lemonSliceHealth;
    public Text playerHealthText;
    
    void Start()
    {
        maxHealth = 150;
        currentHealth = maxHealth;
        lemonSliceHealth.fillAmount = 1;
        // healthBar.SetHealth(currentHealth, maxHealth);
    }

    void Update(){
        // healthBar.SetHealth(currentHealth, maxHealth);
        playerHealthText.text = currentHealth.ToString();
        lemonSliceHealth.fillAmount = currentHealth / maxHealth;
        if (currentHealth <= 0)
        {
            Die();
        }
        if (currentHealth > maxHealth)
        {
            currentHealth= maxHealth;
        }
       
    }


    // void takeDamage(int amount)
    // {
    //     currentHealth -= amount;
    //     if (currentHealth <= 0)
    //     {
    //         Die();
    //     }
    //     imageHealthBar.fillAmount = currentHealth / maxHealth;
    // }

    // void healUp(int amount)
    // {
    //     currentHealth += amount;
    //     if (currentHealth > maxHealth)
    //     {
    //         currentHealth = maxHealth;
    //     }
    // }

    private void Die()
    {
        FindObjectOfType<gameManager>().GameOver();
    }
}
