using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Upgrades : MonoBehaviour
{
    // Start is called before the first frame

    public Button speedOne, attackOne, healthOne, standOne, moneyOne;

    void Start()
    {
        speedOne.onClick.AddListener(speedUpgrade);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void speedUpgrade()
    {
        Debug.Log("speedy");
    }
}
