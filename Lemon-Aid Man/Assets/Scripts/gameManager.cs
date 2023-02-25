using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class gameManager : MonoBehaviour
{
    public GameObject GameOverScreen;
    public GameObject playerObject;
    bool gameOver = false;
    public Text currentBank;
    public int bank;
    public GameObject WinScreen;
    bool win = false;
    // Start is called before the first frame update
    void Start()
    {
        bank = 0;
        currentBank.text = "Bank: " + bank.ToString(); 
    }

    // Update is called once per frame
    void Update()
    {
        //IF THE GAME IS OVER AND THE R KEY IS PRESSED THEN...
        if (gameOver == true && Input.GetKeyDown(KeyCode.R))
        {


            //LOAD THE SCENE CALLED SampleScene
            SceneManager.LoadScene("Game");

            //SET THE gameOver VARIABLE TO FALSE
            gameOver = false;

        }

        if(bank >= 10) {
            win = true;
            Win();
        }

        if (win == true && Input.GetKeyDown(KeyCode.R))
        {


            //LOAD THE SCENE CALLED SampleScene
            SceneManager.LoadScene("Game");

            //SET THE gameOver VARIABLE TO FALSE
            win = false;

        }


    }

    public void increaseBank()
    {
        bank++;
        currentBank.text = bank.ToString();
        Debug.Log(bank);
    }

    
    public void GameOver()
    {
        GameOverScreen.SetActive(true);
        playerObject.SetActive(false);
        gameOver = true;
    }

    public void Win()
    {
        WinScreen.SetActive(true);
        playerObject.SetActive(false);
        win = true;
    }

}
