using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class LemonadeHeal : MonoBehaviour
{
    // Start is called before the first frame update

    public Button glass;
    public Button pitcher;
    playerHealth ph;
    void Start()
    {

        glass.onClick.AddListener(glassHeal);
        pitcher.onClick.AddListener(pitcherHeal);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void glassHeal()
    {
        float cost = 0.05f;
        if (gameManager.instance.bank >= cost)
        {
            gameManager.instance.bank -= cost;

        }

    }

    public void pitcherHeal()
    {

    }
}
