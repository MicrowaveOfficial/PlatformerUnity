using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackArea : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
       if (other.GetComponent<Shooter>() && other.GetComponent<Shooter>().isBoss)
        {
            GetComponent<Shooter>().Hurt();
        }
        else if (other.CompareTag("Enemy"))
        {
            Destroy(other.gameObject);
        }

    }
}
