using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerHealth : MonoBehaviour
{
    public float maxHealth = 3f;
    public float currentHealth;
    // public Image imageHealthBar;
    public PlayerHealthbarBehavior healthBar;
    
    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetHealth(currentHealth, maxHealth);
    }

    void Update(){
        if (currentHealth <= 0)
        {
            Die();
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
