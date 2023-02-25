using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerController : MonoBehaviour
{
    public float moveSpeed;
    private bool standInRange = false;
    public float coolDown = 1;
    private bool canAttack = true;
    GameObject gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager");
    }

    // Update is called once per frame
    void Update()
    {
        GameObject stand = GameObject.Find("Lemonade Stand");
        var step = moveSpeed * Time.deltaTime;
        var standPosition = stand.transform.position + new Vector3(0, -1f, 0);
        transform.position = Vector3.MoveTowards(transform.position, standPosition, step);

        //buy stand
        if (standInRange && canAttack)
        {
            gameManager.GetComponent<gameManager>().bank ++;
            Debug.Log("Stand buy");
            StartCoroutine(BuyCooldown());
        }
    }
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        // If customer is hit by a player projectile (layer=7), customer disappears
        if (other.gameObject.layer == 7) {
            Destroy(gameObject);
        }
    }

    public void CustomerLeave()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.CompareTag("Stand"))
        {
            standInRange = true;
            Debug.Log("cust to stand In Range");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Stand"))
        {
            standInRange = false;
            Debug.Log(" cust to stand not in range");
        }
    }

    IEnumerator BuyCooldown()
    {
        canAttack = false;
        yield return new WaitForSeconds(coolDown);
        canAttack= true;
    }
}
