using System.Collections;
using System.Collections.Generic;
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
    bool win = false;
    float bank;

    void Start()
    {
        // initialize bank display
        increaseBank(0);
    }

    void Update()
    {

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
        currentBank.text = "Bank: $" + bank.ToString();
    }

    // when game is lost
    public void GameOver()
    {
        GameOverScreen.SetActive(true);
        // playerObject.SetActive(false);
        gameOver = true;
    }

    // when game is won
    public void Win()
    {
        WinScreen.SetActive(true);
        // playerObject.SetActive(false);
        win = true;
    }

}
