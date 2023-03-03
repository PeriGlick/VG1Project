using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerController : MonoBehaviour
{
    public float moveSpeed;
    private bool standInRange = false;
    public float coolDown = 1;
    public float lemonadePrice = .05f;
    private bool canBuy = true;
    GameObject gameManager;
    GameObject stand;

    // Start is called before the first frame update
    void Start()
    {
        stand = GameObject.Find("Lemonade Stand");
        gameManager = GameObject.Find("GameManager");
    }

    // Update is called once per frame
    void Update()
    {
        GameObject stand = GameObject.Find("Lemonade Stand");
        var step = moveSpeed * Time.deltaTime;
        var standPosition = stand.transform.position + new Vector3(0, -1f, 0);
        transform.position = Vector3.MoveTowards(transform.position, standPosition, step);

        //buy from stand if near
        if (standInRange && canBuy)
        {
            FindObjectOfType<gameManager>().increaseBank(lemonadePrice);
            canBuy = false;
            CustomerLeave();
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        // disappear if shot
        if (other.gameObject.GetComponent<LemonGrenadeController>())
        {
            Destroy(gameObject);
        }

        if (other.gameObject.CompareTag("Stand"))
        {
            standInRange = true;
        }
    }

    private void OnCollisionExit2D(Collision2D other) {
        if (other.gameObject.CompareTag("Stand"))
        {
            standInRange = false;
        }
    }

    private void CustomerLeave()
    {
        Destroy(gameObject);
    }
}
