using LemonAidMan;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class gameManager : MonoBehaviour
{
    public GameObject GameOverScreen;
    public GameObject WinScreen;
    public GameObject playerObject;
    bool gameOver = false;
    public Text currentBank;
    public float winningProfits;
    bool win = false;
    public float bank;
    public int duckKills;
    public Text FinalKills1;
    public Text FinalKills2;
    public Text FinalKillsGO;
    public GameObject DuckKillMonitor;
    DuckKillScript dks;
    public GameObject player;
    public static gameManager instance;
    public float lemonadeCost;
    public float customerMoveSpeed;


    public void Awake()
    {
        instance = this; 
    }
    void Start()
    {
        lemonadeCost = 0.05f;
        customerMoveSpeed = 1f;
        // initialize bank display
        increaseBank(0);
        DuckKillMonitor = GameObject.Find("DuckKillMonitor");
        dks = DuckKillMonitor.GetComponent<DuckKillScript>();
        duckKills = 0;
        Time.timeScale = 1;
    }

    void Update()
    {
        duckKills = dks.duckKills;

        //if game has been lost, reset when r key is pressed
        if (gameOver == true && Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene("Game");
            gameOver = false;
        }

        //if game has been won, reset when r key is pressed
        if (win == true && Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene("Game");
            win = false;
        }
    }

    // increase the amount in bank and update its display
    public void increaseBank(float cost)
    {
        bank += cost;
        currentBank.text = "$" + bank.ToString();
        if(bank >= winningProfits) {
            Win();
        }
    }

    // when game is lost
    public void GameOver()
    {
        // TODO: if we properly pause game upon lose, can remove this if statement
        if(!win) {
            GameOverScreen.SetActive(true);
            gameOver = true;
            FinalKillsGO.text = "Final Kills: " + duckKills.ToString();
            Time.timeScale = 0;
            playerObject.GetComponent<playerMovement>().enabled = false;

        }
    }

    

    // when game is won
    public void Win()
    {
        // TODO: if we properly pause game upon win, can remove this if statement
        if(!gameOver) {
            WinScreen.SetActive(true);
            win = true;
           FinalKills1.text = "Final Kills: " + duckKills.ToString();
           FinalKills2.text = "Final Kills: " + duckKills.ToString();
            Time.timeScale = 0;
            playerObject.GetComponent<playerMovement>().enabled = false;
        }
    }

}
