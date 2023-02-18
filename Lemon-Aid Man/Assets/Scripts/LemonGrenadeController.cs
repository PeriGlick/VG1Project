using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LemonGrenadeController : MonoBehaviour
{
    // Outlets
    private Rigidbody2D _rb;
    public float speed = 8f;
    
    // Start is called before the first frame update
    void Start() {
        _rb = GetComponent<Rigidbody2D>();
        _rb.velocity = transform.right * speed;
    }

    private void OnCollisionEnter2D(Collision2D other) {
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
