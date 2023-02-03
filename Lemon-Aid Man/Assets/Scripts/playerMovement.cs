using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LemonAidMan
{
    public class playerMovement : MonoBehaviour
    {



        private Rigidbody2D _rb;
        private SpriteRenderer sprite;
        public float playerSpeed;
        private Vector2 playerDirection;
       

        // Start is called before the first frame update
        void Start()
        {

            _rb = GetComponent<Rigidbody2D>();
            sprite = GetComponent<SpriteRenderer>();   

        }

        // Update is called once per frame
        void Update()
        {
            float dirX = Input.GetAxisRaw("Horizontal"); //1 is right
            float dirY = Input.GetAxisRaw("Vertical"); // 1 is up

            playerDirection = new Vector2(dirX, dirY).normalized;
        }

        void FixedUpdate()
        {
            //Debug.Log(""); 
            _rb.velocity = new Vector2(playerDirection.x * playerSpeed, playerDirection.y * playerSpeed);
        }

    }
}
