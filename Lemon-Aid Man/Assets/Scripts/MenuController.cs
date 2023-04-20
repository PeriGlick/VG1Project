using LemonAidMan;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    //public GameObject[] standMenuItems;
    public GameObject StandMenu;
    
    public GameObject playerObject;
    bool InRange = false;
    SpriteRenderer sprite;
    public bool isOn;
    
    //Color highlight = Color.yellow;
    

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale= 1;
        isOn = false;
        
        hidePaused();
        sprite = GetComponent<SpriteRenderer>();
        //sprite.color = Color.white;

       
        
        
    }


    private void Update()
    {
        if(InRange == true)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (Time.timeScale == 1)
                {
                    Time.timeScale = 0;

                    showPaused();
                    isOn = true;
                    
                        playerObject.GetComponent<playerMovement>().enabled = false;
                }
                else if (Time.timeScale == 0)
                {
                    Time.timeScale = 1;
                    
                    hidePaused();
                    isOn = false;

                    playerObject.GetComponent<playerMovement>().enabled = true;
                }
            }
            
        }

    // Update is called once per frame
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            InRange= true;
            sprite.color = Color.yellow;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            InRange = false;
            sprite.color = Color.white;
           
        }
    }


    public void showPaused()
    {
      

        StandMenu.SetActive(true);

    }

    public void hidePaused()
    {


        StandMenu.SetActive(false);
    }

}
