using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 1.4f;
    [SerializeField] private float bounce = 200f;
    [SerializeField] private int damageGiven = 1;
    [SerializeField] private float coolDown = 2f;
    [SerializeField] private GameObject projectile;
    private bool direction;
    [SerializeField] private float projectileOffset = 0.8f;
    private SpriteRenderer rend;
    private bool canMove = true;

    [SerializeField] private float knockForce = 200f;
    [SerializeField] private float upwardForce = 100f;

    private void Start()
    {
        rend = GetComponent<SpriteRenderer>();
    }

    void FixedUpdate()
    {
        if (!canMove)     
            return;   
        
        transform.Translate(new Vector2 (moveSpeed, 0) * Time.deltaTime);

        if (moveSpeed < 0)
        {
            rend.flipX = true;
            direction = false;
        }

        if (moveSpeed > 0)
        {
            rend.flipX = false;
            direction = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("EnemyBlock"))
        {
            moveSpeed = -moveSpeed;
        }

        if (other.gameObject.CompareTag("Enemy"))
        {
            moveSpeed = -moveSpeed;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(direction == false)  projectileOffset = -projectileOffset;

        Instantiate(projectile, transform.position + (transform.forward * projectileOffset), transform.rotation);
        if (other.CompareTag("Player"))
        {
            other.gameObject.GetComponent<ProjectileMovement>().ProjectileBehavior(other, direction);
        }
    }
}
