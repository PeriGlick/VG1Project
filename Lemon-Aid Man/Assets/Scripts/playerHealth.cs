using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerHealth : MonoBehaviour
{


    public float maxHealth = 3f;
    public float currentHealth;
    public Image imageHealthBar;
   
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    
    void takeDamage(int amount)
    {
        currentHealth -= amount;
        if(currentHealth <= 0) 
        {
            Die();
        }
        imageHealthBar.fillAmount = currentHealth / maxHealth;
    }

    void healUp(int amount)
    {
        currentHealth += amount;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
    }
    void Die()
    {
        Debug.Log("You died lol");
        FindObjectOfType<gameManager>().GameOver();
    }

}
