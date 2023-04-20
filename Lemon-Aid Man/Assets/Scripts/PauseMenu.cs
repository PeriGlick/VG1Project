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
    public GameObject StandMenuControllerObject;
    public MenuController MC;
    public bool standOn;
    bool pauseOn;
    // Start is called before the first frame update
    void Start()
    {
        pauseOn = false;
        Time.timeScale = 1;
        HidePause();
       MC = StandMenuControllerObject.GetComponent<MenuController>();
       standOn = MC.isOn;
        MainMenu.onClick.AddListener(BackToMM);
        Resume.onClick.AddListener(ResumePlay);
        Pause.onClick.AddListener(PauseButton);
       
    }

    // Update is called once per frame
    public void Update()
    {
        standOn = MC.isOn;
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            
            if(Time.timeScale == 1)
            {
                
                    Time.timeScale = 0;
                    ShowPause();
                    player.GetComponent<playerMovement>().enabled = false;
                

            }
            else if (Time.timeScale == 0)
            {
                if (standOn)
                {
                    if (pauseOn)
                    {
                        HidePause();
                    }
                    else
                    {
                        ShowPause();
                    }

                }
                else 
                {
                    HidePause();

                    Time.timeScale = 1;
                    player.GetComponent<playerMovement>().enabled = true;
                }
               
                

            }
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
        if (standOn)
        {
            HidePause();
        }
        else
        {
            Time.timeScale = 1;
            player.GetComponent<playerMovement>().enabled = true;
            HidePause();

        }

    }

    public void HidePause()
    {
        pauseMenu.SetActive(false);
        pauseOn = false;
    }

    public void ShowPause()
    {
        pauseMenu.SetActive(true);
        pauseOn = true;
    }
}
