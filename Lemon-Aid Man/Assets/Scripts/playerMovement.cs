using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace LemonAidMan
{
    public class playerMovement : MonoBehaviour
    {
        private Rigidbody2D _rb;
        public Transform aimPivot;
        public GameObject projectilePrefab;
        
        public KeyCode keyUp;
        public KeyCode keyDown;
        public KeyCode keyLeft;
        public KeyCode keyRight;
        public float moveSpeed;
        float health;

        void Start()
        {
            _rb = GetComponent<Rigidbody2D>();
        }

        void Update()
        {
            // reload();
            
            // Aim Towards Mouse
            Vector3 mousePos = Input.mousePosition;
            Vector3 mousePosInWorld = Camera.main.ScreenToWorldPoint(mousePos);
            Vector3 directionFromPlayerToMouse = mousePosInWorld - transform.position;

            float radiansToMouse = Mathf.Atan2(directionFromPlayerToMouse.y, directionFromPlayerToMouse.x);
            float angleToMouse = radiansToMouse * Mathf.Rad2Deg;
            
            aimPivot.rotation = Quaternion.Euler(0, 0, angleToMouse);
            
            // Fire Projectile
            if (Input.GetKeyDown(KeyCode.Space)) {
                GameObject newProjectile = Instantiate(projectilePrefab);
                newProjectile.transform.position = transform.position;
                newProjectile.transform.rotation = aimPivot.rotation;
            }
        }

        void FixedUpdate()
        {
           if (Input.GetKey(keyUp))
            {
                _rb.AddForce(Vector2.up * moveSpeed * Time.fixedDeltaTime, ForceMode2D.Impulse);
            }

            if (Input.GetKey(keyDown))
            {
                _rb.AddForce(Vector2.down * moveSpeed * Time.fixedDeltaTime, ForceMode2D.Impulse);
            }

            if (Input.GetKey(keyLeft))
            {
                _rb.AddForce(Vector2.left * moveSpeed * Time.fixedDeltaTime, ForceMode2D.Impulse);
            }

            if (Input.GetKey(keyRight))
            {
                _rb.AddForce(Vector2.right * moveSpeed * Time.fixedDeltaTime, ForceMode2D.Impulse);
            }
        }

    }
}
