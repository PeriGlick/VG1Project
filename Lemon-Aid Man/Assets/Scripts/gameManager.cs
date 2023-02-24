using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class gameManager : MonoBehaviour
{
    public GameObject GameOverScreen;
    public GameObject playerObject;
    bool gameOver = false;
    // Start is called before the first frame update
    void Start()
    {
        
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
    }

    public void GameOver()
    {
        GameOverScreen.SetActive(true);
        playerObject.SetActive(false);
        gameOver = true;
    }
}
