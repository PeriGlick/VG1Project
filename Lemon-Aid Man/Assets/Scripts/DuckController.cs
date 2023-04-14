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

        Debug.Log( sp.sprite.name);

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
            animator.SetTrigger("gunning");
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
            animator.SetTrigger("gunning");
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

        // var rootStateMachine = animator.layers[0].stateMachine;
        // var stateWithBlendTree = rootStateMachine.states[0].state;
        // var blendTree = (BlendTree)stateWithBlendTree.motion;


        // int w = this.animator.GetCurrentAnimatorClipInfo(0).Length;
        // string[] clipName = new string[w];
        // string clipNames = "";
        // for (int i = 0; i < w; i += 1)
        // {
        //     clipName[i] = animator.GetCurrentAnimatorClipInfo(0)[i].clip.name;
        //     if(clipName[i] == "LeftShoot") {            
        //         aimPivot.transform.position = transform.position + new Vector3(-.6f,0.036f,0);
        //     } else if(clipName[i] == "RightShoot") {            
        //         aimPivot.transform.position = transform.position + new Vector3(.6f,0.036f,0);
        //     } else if(clipName[i] == "DownShoot") {            
        //         aimPivot.transform.position = transform.position + new Vector3(-0.042f,0.007f,0);
        //     } else if(clipName[i] == "IdleUp") {            
        //         aimPivot.transform.position = transform.position + new Vector3(-0.042f,0.007f,0);
        //     }
        //     clipNames += clipName[i];
        // }
        // Debug.Log(clipNames);
    }

    void FixedUpdate() {
        string spriteName = sp.sprite.name;

        // shoot right
        if(spriteName == "duck_sprite_sheet_24") {
            aimPivot.transform.position = transform.position + new Vector3(.6f,0.036f,0);
            sp.flipX = false;
        // shoot left
        } else if(spriteName == "duck_sprite_sheet_23") {
            aimPivot.transform.position = transform.position + new Vector3(-.6f,0.036f,0);
            sp.flipX = true;
        } else {
            sp.flipX = false;
            aimPivot.transform.position = transform.position + new Vector3(-0.042f,0.007f,0);
        }

        var step =  moveSpeed * Time.deltaTime; 
        var distBtwPlayerAndDuck = Vector3.Distance(transform.position, player.transform.position);

        if(distBtwPlayerAndDuck < shootRange) {
            playerInRange = true;
            // animator.SetBool("gunning", true);
        } else {
            playerInRange = false;
            // animator.SetBool("gunning", false);
        }
        
        // move towards player
        if(distBtwPlayerAndDuck < moveToPlayerDistance && distBtwPlayerAndDuck > shootRange * (1/2))
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

    private void OnCollisionEnter2D(Collision2D other)
    {
        // disappear if shot with grenade
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

        if (other.gameObject.CompareTag("Stand"))
        {
            standInRange = true;
            animator.SetBool("gunning", true);
        }
    }

    private void OnCollisionExit2D(Collision2D other) {
        // if (other.gameObject.CompareTag("Player"))
        // {
        //     playerInRange = false;
        //     animator.SetBool("gunning", false);
        // }

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
