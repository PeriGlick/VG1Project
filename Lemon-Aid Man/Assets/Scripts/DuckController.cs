using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class DuckController : MonoBehaviour
{
    public Transform aimPivot;
    public GameObject projectilePrefab;

    private bool playerInRange = false;
    public float coolDown = 1;
    public int damage = 1;
    private bool canAttack = true;

    private Rigidbody2D _rb;
    // private CircleCollider2D _c;
    
    public float moveSpeed;
    public float moveToPlayerDistance;
    private bool standInRange = false;
    GameObject stand;
    GameObject player;
    // GameObject LemonadeGrenade;
    Animator animator;
    public GameObject dkm;
    public DuckKillScript dks;
    public int duckHealth = 12;
    public SpriteRenderer sp;
    public CircleCollider2D _c;



    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _c = GetComponent<CircleCollider2D>();
        // LemonadeGrenade = GameObject.Find("LemonadeGrenade");
        stand = GameObject.Find("Lemonade Stand");
        player = GameObject.Find("Player");
        animator = GetComponent<Animator>();
        GameObject obj = GameObject.Find("DuckKillMonitor");
        dks = obj.GetComponent<DuckKillScript>();
        sp = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        animator.SetBool("inRange", playerInRange);
        var step =  moveSpeed * Time.deltaTime; 
        
        // move towards player
        if(Vector3.Distance(transform.position, player.transform.position) < moveToPlayerDistance)
        {
            Vector2 directionToTarget = player.transform.position - transform.position;
            _rb.AddForce(Vector3.Normalize(directionToTarget) * step, ForceMode2D.Impulse);

            // transform.position = Vector3.MoveTowards(transform.position, player.transform.position, step);
        }

        // move towards stand unless near player
        else if (Vector3.Distance(transform.position, stand.transform.position) > 1f)
        {
            Vector2 directionToTarget = stand.transform.position - transform.position;
            _rb.AddForce(Vector3.Normalize(directionToTarget) * step, ForceMode2D.Impulse);

            // transform.position = Vector3.MoveTowards(transform.position, stand.transform.position, step);
        }

        // change animation based on velocity
        float movementAngle = Mathf.Atan2(_rb.velocity.y, _rb.velocity.x) * Mathf.Rad2Deg;

        if(_rb.velocity.magnitude < .1) {
            animator.SetBool("Idle", true);
        } else {
            animator.SetBool("Idle", false);
        }

        // moving right
        if(movementAngle > -45 && movementAngle <= 45) {
            sp.flipX = false;
            animator.SetInteger("Direction", 0);
            aimPivot.transform.position = transform.position + new Vector3(.6f,0,0);
        // moving up 
        } else if(movementAngle > 45 && movementAngle <= 135) {
            animator.SetInteger("Direction", 1);
            aimPivot.transform.position = transform.position;
        // moving left
        } else if(movementAngle > 135 || movementAngle <= -135) {
            animator.SetInteger("Direction", 0);
            sp.flipX = true;
            aimPivot.transform.position = transform.position + new Vector3(-.6f,0,0);
        // moving down
        } else if(movementAngle > -135 && movementAngle <= -45) {
            animator.SetInteger("Direction", -1);
            aimPivot.transform.position = transform.position;
        }

        // attack player
        if(playerInRange && canAttack)
        {   
            animator.SetBool("Idle", false);
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
        //attack stand
        else if (standInRange && canAttack)
        {
            animator.SetBool("Idle", false);
            // Aim Towards Stand
            Vector3 standPosition = stand.transform.position;
            Vector3 directionFromDuckToStand = standPosition - transform.position;

            float radiansToPlayer = Mathf.Atan2(directionFromDuckToStand.y, directionFromDuckToStand.x);
            float angleToPlayer = radiansToPlayer * Mathf.Rad2Deg;

            aimPivot.rotation = Quaternion.Euler(0, 0, angleToPlayer);

            GameObject newProjectile = Instantiate(projectilePrefab);
            newProjectile.transform.position = aimPivot.transform.position;
            newProjectile.transform.rotation = aimPivot.rotation;

            // stand.GetComponent<StandController>().standHealth -= damage;
            stand.GetComponent<StandController>().TakeDamage(damage);
            StartCoroutine(AttackCooldown());
        }

        // IsTouching(_c, LemonadeGrenade.GetComponent<CircleCollider2D>());
    }

    // public static bool IsTouching(Collider2D collider1, Collider2D collider2) {
    //     return true;
    // }

    private void OnCollisionEnter2D(Collision2D other)
    {
        // disappear if shot
        if (other.gameObject.GetComponent<LemonGrenadeController>())
        {
            duckHealth = duckHealth - 6;
            if (duckHealth <= 0)
            {
                Destroy(gameObject);
                dks.addKill();
            }

        }

        if (other.gameObject.GetComponent<seedSpitter>())
        {
            duckHealth = duckHealth - 4;
            if (duckHealth <= 0)
            {
                Destroy(gameObject);
                dks.addKill();
            }
        }

        if (other.gameObject.GetComponent<LemonadeSplash>())
        {
            Destroy(gameObject);
            dks.addKill();
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
