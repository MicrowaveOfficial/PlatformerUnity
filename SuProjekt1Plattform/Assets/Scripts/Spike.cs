using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : MonoBehaviour
{
    [SerializeField] private int damageGiven = 1;
    [SerializeField] private float knockForce = 200f;
    [SerializeField] private float upwardForce = 200f;

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<PlayerMovement>().TakeDamage(damageGiven);
        }
        else
            return;

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
