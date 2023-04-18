using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class MeleeDuckController : MonoBehaviour
{
    public Transform aimPivot;
    public GameObject projectilePrefab;

    private bool playerInRange = false;
    public float coolDown = 1;
    public int damage = 1;
    private bool canAttack = true;

    private Vector3 vectorBtwPlayerOrStand;

    private Rigidbody2D _rb;
    
    public float moveSpeed;
    public float moveToPlayerDistance;
    public float shootRange;
    private bool standInRange = false;
    GameObject stand;
    GameObject player;
    Animator animator;
    public GameObject dkm;
    public DuckKillScript dks;
    public int duckHealth = 12;
    public SpriteRenderer sp;

    AnimatorClipInfo[] animatorinfo;
    string current_animation;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
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
        
        // attack player
        if(playerInRange && canAttack)
        {
            // prevent negative damage
            if(player.GetComponent<playerHealth>().currentHealth < damage) {
                player.GetComponent<playerHealth>().currentHealth = 0;
            } else {
                player.GetComponent<playerHealth>().currentHealth -= damage;
            }
            SoundFxManager.instance.PlayMeleeSwoosh();
            SoundFxManager.instance.PlayLAMOuch();
            StartCoroutine(AttackCooldown());
        }
        
        //attack stand
        else if (standInRange && canAttack)
        {
            SoundFxManager.instance.PlayMeleeSwoosh();
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
            animator.SetBool("knifing", true);
            animator.SetFloat("KnifeDirectionX", vectorBtwPlayerOrStand.x);
            animator.SetFloat("KnifeDirectionY", vectorBtwPlayerOrStand.y);  
        } else if(distBtwStandAndDuck < shootRange) {
            vectorBtwPlayerOrStand = stand.transform.position - transform.position;
            playerInRange = false;
            standInRange = true;
            animator.SetBool("knifing", true);
            animator.SetFloat("KnifeDirectionX", vectorBtwPlayerOrStand.x);
            animator.SetFloat("KnifeDirectionY", vectorBtwPlayerOrStand.y);  
        } else {
            playerInRange = false;
            standInRange = false;
            animator.SetBool("knifing", false);
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
                SoundFxManager.instance.PlayDuckScream();
                Destroy(gameObject);
                dks.addKill();
            }
            else {
                SoundFxManager.instance.PlayDuckQuackNormal();
            }
        }

        // lose health if shot with seed spitter
        if (other.gameObject.GetComponent<seedSpitter>())
        {
            duckHealth = duckHealth - 4;
            if (duckHealth <= 0)
            {
                SoundFxManager.instance.PlayDuckScream();
                Destroy(gameObject);
                dks.addKill();
            }
            else {
                SoundFxManager.instance.PlayDuckQuackNormal();
            }
        }

        if (other.gameObject.GetComponent<LemonadeSplash>())
        {
            SoundFxManager.instance.PlayDuckScream();
            Destroy(gameObject);
            dks.addKill();
        }
    }

    // private void OnCollisionExit2D(Collision2D other) {
    // }

    IEnumerator AttackCooldown()
    {
        canAttack = false;
        yield return new WaitForSeconds(coolDown);
        canAttack= true;
    }
}
