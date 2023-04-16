using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;


public class grapeBulltetController : MonoBehaviour
{
    // Outlets
    private Rigidbody2D _rb;

    public float speed = 8f;
    public float liveTime = 0.5f;
    public int damage = 5;

    GameObject stand;
    GameObject player;
   


    void Start() {
        _rb = GetComponent<Rigidbody2D>();
        _rb.velocity = transform.right * speed;
        Destroy(gameObject, liveTime);
        stand = GameObject.Find("Lemonade Stand");
        player = GameObject.Find("Player");
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.CompareTag("Player")) {
            if(player.GetComponent<playerHealth>().currentHealth < damage) {
                player.GetComponent<playerHealth>().currentHealth = 0;
            } else {
                player.GetComponent<playerHealth>().currentHealth -= damage;
            }
            SoundFxManager.instance.PlayLAMOuch();
        }
        if (other.gameObject.CompareTag("Stand")) {
            stand.GetComponent<StandController>().TakeDamage(damage);
        }
        Destroy(gameObject);
    }

    void Update()
    {
       
    }
}
