using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
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
        public bool pause;
        public float coolDownTime = 1f;
        bool isCoolDown = false;
        public SpriteRenderer sp;
      

        void Start()
        {
            _rb = GetComponent<Rigidbody2D>();
            pause = false;
            sp = GetComponent<SpriteRenderer>();
        }

        void Update()
        {
            if (pause)
            {

            }
            if (!pause)
            {
                // Aim Towards Mouse
                Vector3 mousePos = Input.mousePosition;
                Vector3 mousePosInWorld = Camera.main.ScreenToWorldPoint(mousePos);
                Vector3 directionFromPlayerToMouse = mousePosInWorld - transform.position;

                float radiansToMouse = Mathf.Atan2(directionFromPlayerToMouse.y, directionFromPlayerToMouse.x);
                float angleToMouse = radiansToMouse * Mathf.Rad2Deg;

                aimPivot.rotation = Quaternion.Euler(0, 0, angleToMouse);

                // Fire Projectile
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    if (!isCoolDown)
                    {
                        GameObject newProjectile = Instantiate(projectilePrefab);
                        newProjectile.transform.position = transform.position;
                        newProjectile.transform.rotation = aimPivot.rotation;
                        StartCoroutine(CoolDown());
                    }
                }
            }
            
        }

        void FixedUpdate()
        {
            // wasd player movement
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
                sp.flipX = true;
            }

            if (Input.GetKey(keyRight))
            {
                _rb.AddForce(Vector2.right * moveSpeed * Time.fixedDeltaTime, ForceMode2D.Impulse);
                sp.flipX = false;
            }
            float angle = Mathf.Atan2(_rb.velocity.y, _rb.velocity.x) * Mathf.Rad2Deg;
            Debug.Log(angle);
        }

        IEnumerator CoolDown()
        {
            isCoolDown = true;
            yield return new WaitForSeconds(coolDownTime);
            isCoolDown = false;
        }


    }
}
