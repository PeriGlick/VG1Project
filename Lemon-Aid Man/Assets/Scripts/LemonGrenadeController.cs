using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class LemonGrenadeController : MonoBehaviour
{
    // Outlets
    private Rigidbody2D _rb;
    public float speed = 8f;
    public float liveTime = 0.5f;
   


    void Start() {
        _rb = GetComponent<Rigidbody2D>();
        _rb.velocity = transform.right * speed;
        Destroy(gameObject, liveTime);
    }

    private void OnCollisionEnter2D(Collision2D other) {
        Destroy(gameObject);
    }

    void Update()
    {
       
    }

   


}
