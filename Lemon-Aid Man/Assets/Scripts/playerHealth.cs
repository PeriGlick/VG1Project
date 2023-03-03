using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerHealth : MonoBehaviour
{


    public float maxHealth = 3f;
    public float currentHealth;
    public Image imageHealthBar;

    void Start()
    {
        currentHealth = maxHealth;
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

    // // scene reloads when player dies
    // private void reload()
    // {
    //     health = gameObject.GetComponent<playerHealth>().currentHealth;
    //     if (health <= 0)
    //     {
    //         SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    //     }
    // }
}
