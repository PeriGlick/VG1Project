using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerController : MonoBehaviour
{
    public float moveSpeed;
    private bool standInRange = false;
    public float coolDown = 1;
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

        // stand range based on distance rather than trigger event
        // if (Vector3.Distance(transform.position, stand.transform.position) <= 1f) 
        // {
        //     standInRange = true;
        // } else
        // {
        //     standInRange = false;
        // }

        //buy from stand if near
        if (standInRange && canBuy)
        {
            gameManager.GetComponent<gameManager>().bank ++;
            Debug.Log("Stand buy");
            FindObjectOfType<gameManager>().increaseBank();
            StartCoroutine(BuyCooldown());
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
    }

    private void CustomerLeave()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Stand"))
        {
            standInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Stand"))
        {
            standInRange = false;
        }
    }

    IEnumerator BuyCooldown()
    {
        canBuy = false;
        yield return new WaitForSeconds(coolDown);
        canBuy= true;
    }
}
