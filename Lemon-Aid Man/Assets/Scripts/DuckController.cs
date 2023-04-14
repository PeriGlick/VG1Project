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
    public int duckHealth = 10;
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

        int w = this.animator.GetCurrentAnimatorClipInfo(0).Length;
        string[] clipName = new string[w];
        for (int i = 0; i < w; i += 1)
        {
            clipName[i] = animator.GetCurrentAnimatorClipInfo(0)[i].clip.name;
            if(clipName[i] == "LeftShoot") {            
                aimPivot.transform.position = transform.position + new Vector3(-.6f,0.036f,0);
            } else if(clipName[i] == "RightShoot") {            
                aimPivot.transform.position = transform.position + new Vector3(.6f,0.036f,0);
            } else if(clipName[i] == "DownShoot") {            
                aimPivot.transform.position = transform.position + new Vector3(-0.042f,0.007f,0);
            } else if(clipName[i] == "IdleUp") {            
                aimPivot.transform.position = transform.position + new Vector3(-0.042f,0.007f,0);
            }
        }

        // // change animation based on velocity
        // float movementAngle = Mathf.Atan2(_rb.velocity.y, _rb.velocity.x) * Mathf.Rad2Deg;

        // animatorinfo = animator.GetCurrentAnimatorClipInfo(0);
        // current_animation = animatorinfo[0].clip.name;
        // Debug.Log(current_animation);

        // if(current_animation == "DownShoot") {
        //     aimPivot.transform.position = transform.position + new Vector3(-0.042f,0.007f,0);
        // } else if(current_animation == "RightShoot") {
        //     aimPivot.transform.position = transform.position + new Vector3(.6f,0.036f,0);
        // } else if(current_animation == "LeftShoot") {
        //     aimPivot.transform.position = transform.position + new Vector3(-.6f,0.036f,0);
        // } else if(current_animation == "IdleUp") {
        //     aimPivot.transform.position = transform.position + new Vector3(-0.042f,0.007f,0);
        // }

        // // moving right
        // if(movementAngle > -45 && movementAngle <= 45) {
        //     if(!animator.GetBool("gunning")) {
        //         sp.flipX = false;
        //     }
        //     // animator.SetInteger("Direction", 0);
        //     aimPivot.transform.position = transform.position + new Vector3(.6f,0.036f,0);
        // // moving left
        // } else if(movementAngle > 135 || movementAngle <= -135) {
        //     // animator.SetInteger("Direction", 0);
        //     if(!animator.GetBool("gunning")) {
        //         sp.flipX = true;
        //     }
        //     aimPivot.transform.position = transform.position + new Vector3(-.6f,0.036f,0);
        // // moving up 
        // } else if(movementAngle > 45 && movementAngle <= 135) {
        //     // animator.SetInteger("Direction", 1);
        //     aimPivot.transform.position = transform.position + new Vector3(0.042f,0.007f,0);
        // // moving down
        // } else if(movementAngle > -135 && movementAngle <= -45) {
        //     // animator.SetInteger("Direction", -1);
        //     aimPivot.transform.position = transform.position + new Vector3(-0.042f,0.007f,0);
        // }

        // attack player
        if(playerInRange && canAttack)
        {   
            // animator.SetTrigger("attack");
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
            // animator.SetTrigger("attack");
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
        
        // move towards player
        if(Vector3.Distance(transform.position, player.transform.position) < moveToPlayerDistance)
        {
            Vector2 directionToTarget = player.transform.position - transform.position;
            _rb.AddForce(Vector3.Normalize(directionToTarget) * step, ForceMode2D.Impulse);
        }

        // move towards stand unless near player
        else if (Vector3.Distance(transform.position, stand.transform.position) > 1f)
        {
            Vector2 directionToTarget = stand.transform.position - transform.position;
            _rb.AddForce(Vector3.Normalize(directionToTarget) * step, ForceMode2D.Impulse);
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        // disappear if shot with grenade
        if (other.gameObject.GetComponent<LemonGrenadeController>())
        {
            Destroy(gameObject);
            dks.addKill();
            
        }

        // lose health if shot with seed spitter
        if (other.gameObject.GetComponent<seedSpitter>())
        {
            duckHealth = duckHealth - 2;
            if (duckHealth <= 0)
            {
                Destroy(gameObject);
                dks.addKill();
            }
        }

        if (other.gameObject.CompareTag("Player"))
        {
            playerInRange = true;
            animator.SetBool("gunning", true);
        }

        if (other.gameObject.CompareTag("Stand"))
        {
            standInRange = true;
            animator.SetBool("gunning", true);
        }
    }

    private void OnCollisionExit2D(Collision2D other) {
        if (other.gameObject.CompareTag("Player"))
        {
            playerInRange = false;
            animator.SetBool("gunning", false);
        }

        if (other.gameObject.CompareTag("Stand"))
        {
            standInRange = false;
            animator.SetBool("gunning", false);
        }
    }

    IEnumerator AttackCooldown()
    {
        canAttack = false;
        yield return new WaitForSeconds(coolDown);
        canAttack= true;
    }
}
