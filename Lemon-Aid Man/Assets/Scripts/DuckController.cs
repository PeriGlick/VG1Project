using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class DuckController : MonoBehaviour
{
    public Transform aimPivot;
    public GameObject projectilePrefab;

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
    public gameManager gameManager;
    
    
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        stand = GameObject.Find("Lemonade Stand");
        player = GameObject.Find("Player");
        animator = GetComponent<Animator>();
       
    }

    void Update()
    {
        animator.SetBool("inRange", playerInRange);
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
            // Aim Towards Player
            Vector3 playerPosition = player.transform.position;
            Vector3 directionFromDuckToPlayer = playerPosition - transform.position;

            float radiansToPlayer = Mathf.Atan2(directionFromDuckToPlayer.y, directionFromDuckToPlayer.x);
            float angleToPlayer = radiansToPlayer * Mathf.Rad2Deg;

            aimPivot.rotation = Quaternion.Euler(0, 0, angleToPlayer);

            GameObject newProjectile = Instantiate(projectilePrefab);
            newProjectile.transform.position = aimPivot.transform.position;
            newProjectile.transform.rotation = aimPivot.rotation;

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
            gameManager.duckKills++;
        }

        if (other.gameObject.CompareTag("Player"))
        {
            playerInRange = true;
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
