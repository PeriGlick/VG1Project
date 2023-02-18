using System.Collections;
using System.Collections.Generic;
using UnityEngine;





public class DuckController : MonoBehaviour
{
    private bool playerInRange = false;
    public float coolDown = 1;
    public float damage = 1;
    private bool canAttack = true;
    

    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(playerInRange && canAttack)
        {
            GameObject.Find("Player").GetComponent<playerHealth>().currentHealth -= damage;
            Debug.Log("Attack");
            StartCoroutine(AttackCooldown());
        }
    }


    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.GetComponent<LemonGrenadeController>())
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerInRange= true;
            Debug.Log("In Range");
        }
    }

    private void OnTriggerExit2D (Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerInRange = false ;
            Debug.Log("Out of Range");
        }
    }


    IEnumerator AttackCooldown()
    {
        canAttack = false;
        yield return new WaitForSeconds(coolDown);
        canAttack= true;
    }


}
