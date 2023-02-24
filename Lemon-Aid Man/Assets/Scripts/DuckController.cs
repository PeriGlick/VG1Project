using System.Collections;
using System.Collections.Generic;
using UnityEngine;





public class DuckController : MonoBehaviour
{
    private bool playerInRange = false;
    public float coolDown = 1;
    public float damage = 1;
    private bool canAttack = true;
    private Rigidbody2D _rb;
    public float moveSpeed;
    private bool standInRange = false;
    
    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        GameObject stand = GameObject.Find("Lemonade Stand");
        GameObject player = GameObject.Find("Player");
        
        // attack player
        if(playerInRange && canAttack)
        {
            player.GetComponent<playerHealth>().currentHealth -= damage;
            Debug.Log("Attack");
            StartCoroutine(AttackCooldown());
        }

      

        var step =  moveSpeed * Time.deltaTime; 

        // move towards stand unless near player
        if (Vector3.Distance(transform.position, stand.transform.position) > 1f && !playerInRange)
        {
            transform.position = Vector3.MoveTowards(transform.position, stand.transform.position, step);
        } 
        // move towards player
        else if(Vector3.Distance(transform.position, player.transform.position) > 1.2f)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, step);
        }

        //attack stand
        if (standInRange && canAttack)
        {
            stand.GetComponent<StandController>().standHealth -= damage;
            Debug.Log("Stand Attack");
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
            playerInRange = true;
            Debug.Log("In Range");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
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
