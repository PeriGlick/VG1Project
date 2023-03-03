using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LemonGrenadeController : MonoBehaviour
{
    // Outlets
    private Rigidbody2D _rb;
    public float speed = 8f;
    
    void Start() {
        _rb = GetComponent<Rigidbody2D>();
        _rb.velocity = transform.right * speed;
    }

    private void OnCollisionEnter2D(Collision2D other) {
        Destroy(gameObject);
    }

    void Update()
    {
        
    }
}
