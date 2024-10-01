using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackArea : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        //if (other.GetComponent<Shooter>() && other.GetComponent<Shooter>().isBoss)
        // {
        //     GetComponent<Shooter>().Hurt();
        // }

        if (other.CompareTag("Enemy"))
        {
            Destroy(other.gameObject);
        }

        else if (other.CompareTag("Boss"))
        {
            other.gameObject.GetComponent<Shooter>().Hurt();
        }

    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            print("nånting");
            Destroy(other.gameObject);
        }
        if (other.gameObject.CompareTag("Boss"))
        {
            other.gameObject.GetComponent<Shooter>().Hurt();
        }
    }
}
