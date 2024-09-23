using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BeeMovement : MonoBehaviour
{
    [SerializeField] private Transform target1, target2;
    [SerializeField] private float moveSpeed = 2.0f;
    private Transform currentTarget;
    private bool canMove = true;
    private SpriteRenderer rend;

    [SerializeField] private int damageGiven = 1;
    [SerializeField] private float knockForce = 200f;
    [SerializeField] private float upwardForce = 100f;
    [SerializeField] private float bounce = 200f;

    void Start()
    {
        rend = GetComponent<SpriteRenderer>();
        currentTarget = target1;
    }

    void FixedUpdate()
    {
        if (!canMove)
            return;

        if(transform.position == target1.position)
        {
            currentTarget = target2;
            rend.flipX = true;
        }

        if(transform.position == target2.position)
        {
            currentTarget = target1;
            rend.flipX = false;
        }

        transform.position = Vector2.MoveTowards(transform.position, currentTarget.position, moveSpeed*Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<PlayerMovement>().TakeDamage(damageGiven);

            if(other.transform.position.x > transform.position.x)
            {
                other.gameObject.GetComponent<PlayerMovement>().TakeKnockback(knockForce, upwardForce);
            }
            else
            {
                other.gameObject.GetComponent<PlayerMovement>().TakeKnockback(-knockForce, upwardForce);
            }

        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<Rigidbody2D>().velocity = new Vector2(other.GetComponent<Rigidbody2D>().velocity.x, 0);
            other.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, bounce));
            GetComponent<Animator>().SetTrigger("Hit");
            GetComponent<BoxCollider2D>().enabled = false;
            GetComponent<CircleCollider2D>().enabled = false;
            canMove = false;
            Destroy(gameObject, 0.5f);
        }
    }

}
