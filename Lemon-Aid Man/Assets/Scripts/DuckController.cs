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

    private Vector3 vectorBtwPlayerOrStand;

    private Rigidbody2D _rb;
    // private CircleCollider2D _c;
    
    public float moveSpeed;
    public float moveToPlayerDistance;
    public float shootRange;
    private bool standInRange = false;
    GameObject stand;
    GameObject player;
    // GameObject LemonadeGrenade;
    Animator animator;
    public GameObject dkm;
    public DuckKillScript dks;
    public int duckHealth = 12;
    public SpriteRenderer sp;
    // public CircleCollider2D _c;

    AnimatorClipInfo[] animatorinfo;
    string current_animation;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        // _c = GetComponent<CircleCollider2D>();
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
        float movementSpeed = _rb.velocity.sqrMagnitude;
        animator.SetFloat("speed", movementSpeed);
        if(movementSpeed > .1f) {
            animator.SetFloat("movementX", _rb.velocity.x);
            animator.SetFloat("movementY", _rb.velocity.y);                
        }            

        // this is literally all just to move the bullet to come out of the gun
        // and it doesn't even workkkk
        string spriteName = sp.sprite.name;
        float movementAngle = Mathf.Atan2(vectorBtwPlayerOrStand.x, vectorBtwPlayerOrStand.y) * Mathf.Rad2Deg;
        if(spriteName == "duck_sprite_sheet_24") {
            aimPivot.transform.position = transform.position + new Vector3(.6f,0.036f,0);
        // shoot left
        } else if(spriteName == "flipped_duck_sprite_sheet_26") {
            aimPivot.transform.position = transform.position + new Vector3(-.6f,0.036f,0);
        } else {
            // moving right
            if(movementAngle > -50 && movementAngle <= -130) {
                aimPivot.transform.position = transform.position + new Vector3(-.6f,0.036f,0);
            // moving left
            } else if(movementAngle > 50 && movementAngle <= 130) {
                aimPivot.transform.position = transform.position + new Vector3(.6f,0.036f,0);
            // moving up 
            } else {
                aimPivot.transform.position = transform.position + new Vector3(-0.042f,0.007f,0);
            }
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

            // shoot a grape
            GameObject newProjectile = Instantiate(projectilePrefab);
            newProjectile.transform.position = aimPivot.transform.position;
            newProjectile.transform.rotation = aimPivot.rotation;

            player.GetComponent<playerHealth>().currentHealth -= damage;
            
            StartCoroutine(AttackCooldown());
        }
        //attack stand
        else if (standInRange && canAttack)
        {
            // Aim Towards Stand
            Vector3 standPosition = stand.transform.position;
            Vector3 directionFromDuckToStand = standPosition - transform.position;

            float radiansToPlayer = Mathf.Atan2(directionFromDuckToStand.y, directionFromDuckToStand.x);
            float angleToPlayer = radiansToPlayer * Mathf.Rad2Deg;

            aimPivot.rotation = Quaternion.Euler(0, 0, angleToPlayer);

            // shoot a grape
            GameObject newProjectile = Instantiate(projectilePrefab);
            newProjectile.transform.position = aimPivot.transform.position;
            newProjectile.transform.rotation = aimPivot.rotation;

            stand.GetComponent<StandController>().TakeDamage(damage);
            StartCoroutine(AttackCooldown());
        }
    }

    void FixedUpdate() {
        var step =  moveSpeed * Time.deltaTime; 
        var distBtwPlayerAndDuck = Vector3.Distance(transform.position, player.transform.position);
        var distBtwStandAndDuck = Vector3.Distance(transform.position, stand.transform.position);

        // check if player is within range to attack stand or player
        if(distBtwPlayerAndDuck < shootRange) {
            vectorBtwPlayerOrStand = player.transform.position - transform.position;
            playerInRange = true;
            standInRange = false;
            animator.SetBool("gunning", true);
            animator.SetFloat("ShootDirectionX", vectorBtwPlayerOrStand.x);
            animator.SetFloat("ShootDirectionY", vectorBtwPlayerOrStand.y);  
        } else if(distBtwStandAndDuck < shootRange) {
            vectorBtwPlayerOrStand = stand.transform.position - transform.position;
            playerInRange = false;
            standInRange = true;
            animator.SetBool("gunning", true);
            animator.SetFloat("ShootDirectionX", vectorBtwPlayerOrStand.x);
            animator.SetFloat("ShootDirectionY", vectorBtwPlayerOrStand.y);  
        } else {
            playerInRange = false;
            standInRange = false;
            animator.SetBool("gunning", false);
        }
        
        // move towards player
        if(distBtwPlayerAndDuck < moveToPlayerDistance && distBtwPlayerAndDuck > shootRange * (1/3) && canAttack)
        {
            Vector2 directionToTarget = player.transform.position - transform.position;
            _rb.AddForce(Vector3.Normalize(directionToTarget) * step, ForceMode2D.Impulse);
        }
        // move towards stand unless near player
        else if (distBtwStandAndDuck > shootRange * (1/3) && (distBtwPlayerAndDuck >= moveToPlayerDistance))
        {
            Vector2 directionToTarget = stand.transform.position - transform.position;
            _rb.AddForce(Vector3.Normalize(directionToTarget) * step, ForceMode2D.Impulse);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        // lose 6 health if shot with grenade
        if (other.gameObject.GetComponent<LemonGrenadeController>())
        {
            duckHealth = duckHealth - 6;
            if (duckHealth <= 0)
            {
                Destroy(gameObject);
                dks.addKill();
            }
        }

        // lose health if shot with seed spitter
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

        // if (other.gameObject.CompareTag("Player"))
        // {
        //     playerInRange = true;
        //     animator.SetBool("gunning", true);
        // }

        // if (other.gameObject.CompareTag("Stand"))
        // {
        //     standInRange = true;
        //     animator.SetBool("gunning", true);
        // }
    }

    private void OnCollisionExit2D(Collision2D other) {
        // if (other.gameObject.CompareTag("Player"))
        // {
        //     playerInRange = false;
        //     animator.SetBool("gunning", false);
        // }

        // if (other.gameObject.CompareTag("Stand"))
        // {
        //     standInRange = false;
        //     animator.SetBool("gunning", false);
        // }
    }

    IEnumerator AttackCooldown()
    {
        canAttack = false;
        yield return new WaitForSeconds(coolDown);
        canAttack= true;
    }
}
