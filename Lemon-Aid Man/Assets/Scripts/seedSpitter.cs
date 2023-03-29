using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class seedSpitter : MonoBehaviour
{

    private Rigidbody2D _rb;
    public float speed = 12f;
    public float liveTime = 0.5f;
    public float weaponCoolDown = 0.2f;
    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _rb.velocity = transform.right * speed;
        Destroy(gameObject, liveTime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        Destroy(gameObject);
    }
}
