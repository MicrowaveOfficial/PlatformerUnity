using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackArea : MonoBehaviour
{
    private int damage = 3;

    private void OnTriggerEnter2D(Collider2D other)
    {
       if (other.CompareTag("Enemy"))
        {
            Destroy(other.gameObject);
        }

       if (other.GetComponent<Shooter>().isBoss)
        {
            getComponent<Shooter>().Hurt;
        }
    }
}
