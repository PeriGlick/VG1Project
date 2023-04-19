using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class CustomerController : MonoBehaviour
{
    public float coolDown;
    public float lemonadePrice;

    private bool standInRange = false;
    private bool canBuy = true;
    private bool visitedStand = false;
    private int randExitDirection; 

    private Rigidbody2D _rb;
    GameObject gM;
    GameObject stand;
    
    public SpriteRenderer sp;

    private Vector3 finalDestRightPos;
    private Vector3 finalDestLeftPos;

    private Animator animator;

    void Start() {
        _rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sp = GetComponent<SpriteRenderer>();
        
        stand = GameObject.Find("Lemonade Stand");
        gM = GameObject.Find("GameManager");
        
        finalDestRightPos = GameObject.Find("Customer Final Dest (Right)").transform.position;
        finalDestLeftPos = GameObject.Find("Customer Final Dest (Left)").transform.position;
        randExitDirection = UnityEngine.Random.Range(0, 2); // Pops out either 0 or 1
        lemonadePrice = gameManager.instance.lemonadeCost;
    }

    // Update is called once per frame
    void Update()
    {
        lemonadePrice = gameManager.instance.lemonadeCost;

        // buy from stand if near
        if (standInRange && canBuy) {
            FindObjectOfType<gameManager>().increaseBank(lemonadePrice);
            // Switch animation to Idle and pause at stand for a few secs
            StartCoroutine(IdleWait()); 
            canBuy = false;
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
        // moving up 
        } else if(movementAngle > 45 && movementAngle <= 135 ) {
            animator.SetInteger("Direction", 1);
        // moving left
        } else if(movementAngle > 135 || movementAngle <= -135 ) {
            animator.SetInteger("Direction", 0);
            sp.flipX = true;
        // moving down
        } else if(movementAngle > -135 && movementAngle <= -45 ) {
            animator.SetInteger("Direction", -1);
        }
    }

    void FixedUpdate() {
        var step =  gameManager.instance.customerMoveSpeed * Time.deltaTime; 
        
        // leave game
        if (visitedStand) {
            CustomerLeave();
        // move towards stand
        } else {
            Vector2 directionToTarget = stand.transform.position - transform.position;
            _rb.AddForce(Vector3.Normalize(directionToTarget) * step, ForceMode2D.Impulse);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        // var hitByLemonBeforeVisitingStand = other.gameObject.GetComponent<LemonGrenadeController>() && !visitedStand;
        // var hitBySeedBeforeVisitingStand = other.gameObject.GetComponent<seedSpitter>() && !visitedStand;
        // var hitBySplashBeforeVisitingStand = other.gameObject.GetComponent<LemonadeSplash>() && !visitedStand;
        var hitByLemon = other.gameObject.GetComponent<LemonGrenadeController>();
        var hitBySeed = other.gameObject.GetComponent<seedSpitter>();
        var hitBySplash = other.gameObject.GetComponent<LemonadeSplash>();
        var visitedStandAndReachesBoundary = other.collider.CompareTag("Game Boundary") && visitedStand;

        if (hitByLemon || visitedStandAndReachesBoundary || hitBySeed || hitBySplash) {        
        // if (hitByLemonBeforeVisitingStand || visitedStandAndReachesBoundary || hitBySeedBeforeVisitingStand || hitBySplashBeforeVisitingStand) {
            Destroy(gameObject);
        }

        if (other.gameObject.CompareTag("Stand"))
        {
            standInRange = true;
        }
    }

    private void CustomerLeave() {
        var step = gameManager.instance.customerMoveSpeed * Time.fixedDeltaTime;
        // Randomly exits to the left or right of the screen
        if (randExitDirection == 0) {
            var standPosition = stand.transform.position + new Vector3(0, -1f, 0);
            Vector2 directionToTarget = finalDestRightPos - transform.position;
            _rb.AddForce(Vector3.Normalize(directionToTarget) * step, ForceMode2D.Impulse);
        }
        else {
            var standPosition = stand.transform.position + new Vector3(0, -1f, 0);
            Vector2 directionToTarget = finalDestLeftPos - transform.position;
            _rb.AddForce(Vector3.Normalize(directionToTarget) * step, ForceMode2D.Impulse);
        }
    }

    private void OnCollisionExit2D(Collision2D other) {
        if (other.gameObject.CompareTag("Stand"))
        {
            standInRange = false;
        }
    }
    
    IEnumerator IdleWait() {
        yield return new WaitForSeconds(3);
        visitedStand = true;
    }
}
