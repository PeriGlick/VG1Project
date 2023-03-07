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
    public float moveToPlayerDistance;
    private bool standInRange = false;
    GameObject stand;
    GameObject player;
    Animator animator;
    
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        stand = GameObject.Find("Lemonade Stand");
        player = GameObject.Find("Player");
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        var step =  moveSpeed * Time.deltaTime; 

        // move towards player
        if(Vector3.Distance(transform.position, player.transform.position) < moveToPlayerDistance)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, step);
        }

        // move towards stand unless near player
        else if (Vector3.Distance(transform.position, stand.transform.position) > 1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, stand.transform.position, step);
        }

        //attack stand
        if (standInRange && canAttack)
        {
            stand.GetComponent<StandController>().standHealth -= damage;
            StartCoroutine(AttackCooldown());
        }

        // attack player
        if(playerInRange && canAttack)
        {   
            player.GetComponent<playerHealth>().currentHealth -= damage;
            player.GetComponent<playerHealth>().healthBar.SetHealth(player.GetComponent<playerHealth>().currentHealth, player.GetComponent<playerHealth>().maxHealth);
            StartCoroutine(AttackCooldown());
        }

    }


    private void OnCollisionEnter2D(Collision2D other)
    {
        // disappear if shot
        if (other.gameObject.GetComponent<LemonGrenadeController>())
        {
            Destroy(gameObject);
        }

        if (other.gameObject.CompareTag("Player"))
        {
            playerInRange = true;
            animator.SetBool("inRange", true);
        }

        if (other.gameObject.CompareTag("Stand"))
        {
            standInRange = true;
        }
    }

    private void OnCollisionExit2D(Collision2D other) {
        if (other.gameObject.CompareTag("Player"))
        {
            playerInRange = false;
            animator.SetBool("inRange", false);
        }

        if (other.gameObject.CompareTag("Stand"))
        {
            standInRange = false;
        }
    }

    IEnumerator AttackCooldown()
    {
        canAttack = false;
        yield return new WaitForSeconds(coolDown);
        canAttack= true;
    }
}
