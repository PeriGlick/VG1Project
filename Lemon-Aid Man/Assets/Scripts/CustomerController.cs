using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class CustomerController : MonoBehaviour
{
    public float moveSpeed;
    public float coolDown;
    public float lemonadePrice = .05f;

    private bool standInRange = false;
    private bool canBuy = true;
    private bool visitedStand = false;
    private int randExitDirection; 

    private Rigidbody2D _rb;
    GameObject gameManager;
    GameObject stand;

    private Vector3 finalDestRightPos;
    private Vector3 finalDestLeftPos;

    private Animator animator;

    // Start is called before the first frame update
    void Start() {
        _rb = GetComponent<Rigidbody2D>();
        stand = GameObject.Find("Lemonade Stand");
        gameManager = GameObject.Find("GameManager");
        finalDestRightPos = GameObject.Find("Customer Final Dest (Right)").transform.position;
        finalDestLeftPos = GameObject.Find("Customer Final Dest (Left)").transform.position;
        randExitDirection = UnityEngine.Random.Range(0, 2); // Pops out either 0 or 1
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // Set animator parameters
        // animator.SetBool("VisitedStand", visitedStand);
        // animator.SetBool("StandInRange", standInRange);
        // animator.SetBool("CanBuy", canBuy);
        
        // move towards stand
        if (!visitedStand) {
            var step = moveSpeed * Time.deltaTime;
            var standPosition = stand.transform.position + new Vector3(0, -1f, 0);
            transform.position = Vector3.MoveTowards(transform.position, standPosition, step);
        }

        // buy from stand if near
        if (standInRange && canBuy) {
            FindObjectOfType<gameManager>().increaseBank(lemonadePrice);
            // Switch animation to Idle and pause at stand for a few secs
            StartCoroutine(IdleWait()); 
            canBuy = false;
        }

        if (visitedStand) {
            CustomerLeave();
         }

        // trying to change animation based on velocity
        if(_rb.velocity.y > 0) {
            animator.SetBool("VisitedStand", true);
        } else {
            animator.SetBool("VisitedStand", false);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        // disappear if shot by lemon grenade OR hits game boundary after buying from stand
        if (other.gameObject.GetComponent<LemonGrenadeController>() || (other.collider.CompareTag("Game Boundary") && visitedStand))
        {
            Destroy(gameObject);
        }

        if (other.gameObject.CompareTag("Stand"))
        {
            standInRange = true;
        }
    }

    private void CustomerLeave() {
        // Randomly exits to the left or right of the screen
        if (randExitDirection == 0) {
            transform.position = Vector3.MoveTowards(transform.position, finalDestRightPos, moveSpeed * Time.deltaTime);
        }
        else {
            transform.position = Vector3.MoveTowards(transform.position, finalDestLeftPos, moveSpeed * Time.deltaTime);
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
