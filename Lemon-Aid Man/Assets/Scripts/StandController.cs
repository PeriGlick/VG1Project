using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;


public class StandController : MonoBehaviour
{
    // Start is called before the first frame update

    public int currentBank;
    //public float standHealth = 20f; commented out to try healthbar code
    public float standHealth;
    public Slider slider;
    public Text text;




    void Start()
    {
        currentBank = 0;
    }

    // Update is called once per frame
    void Update()
    {
        slider.value = standHealth;
        text.text = "Health : " + standHealth; //check caps 
        if (standHealth == 0f)
        {
            endGame();
        }

        if(currentBank== 20)
        {
            winGame();
        }
    }
//    void OnCollisionEnter2D(Collision2D obj) //check if need 2d
//     {
//         if (obj.gameObject.tag == "Enemy")
//             standHealth = standHealth - 10f;

//         if (obj.gameObject.tag == "Customer")
//         {
//             Debug.Log("customer reached stand");
//             currentBank++;
//             FindObjectOfType<gameManager>().increaseBank();
//             FindObjectOfType<CustomerController>().CustomerLeave();
//         }
//     }

    void endGame()
    {
        FindObjectOfType<gameManager>().GameOver();
    }

    void winGame()
    {
        FindObjectOfType<gameManager>().Win();
    }
}
