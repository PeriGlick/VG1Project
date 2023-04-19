using LemonAidMan;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    public Button Resume;
    public Button MainMenu;
    public GameObject player;
    public Button Pause;
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        HidePause();
        MainMenu.onClick.AddListener(BackToMM);
        Resume.onClick.AddListener(ResumePlay);
        Pause.onClick.AddListener(PauseButton);
       
    }

    // Update is called once per frame
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            
            if(Time.timeScale == 1)
            {
                Time.timeScale = 0;
                ShowPause();
                player.GetComponent<playerMovement>().enabled = false;

            }
            /*else if (Time.timeScale == 0)
            {
                HidePause();
                Time.timeScale = 1;
                player.GetComponent<playerMovement>().enabled = true;
            }*/
        }
    }

    public void BackToMM()
    {
        SceneManager.LoadScene("StartGameMenu");

        
    }

    public void PauseButton()
    {
        Time.timeScale = 0;
        ShowPause();
        player.GetComponent<playerMovement>().enabled = false;
    }

    public void ResumePlay()
    {
        Time.timeScale = 1;
        player.GetComponent<playerMovement>().enabled = true;
        HidePause();
        
    }

    public void HidePause()
    {
        pauseMenu.SetActive(false);
    }

    public void ShowPause()
    {
        pauseMenu.SetActive(true);
    }
}
