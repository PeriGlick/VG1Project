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

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        slider.value = standHealth;
        healthText.text = "Stand Health: " + standHealth; //check caps 
        if (standHealth <= 0f)
        {
            endGame();
        }
    }

    void endGame()
    {
        FindObjectOfType<gameManager>().GameOver();
    }
}
