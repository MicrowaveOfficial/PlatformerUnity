using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMovement : MonoBehaviour
{
    [SerializeField] private float projectileSpeed = 2f;
    [SerializeField] private int damage = 1;
    private SpriteRenderer rend;

    private void Start()
    {
        rend = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        transform.Translate(new Vector2(projectileSpeed, 0) * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.gameObject.GetComponent<PlayerMovement>().TakeDamage(damage);
            Destroy(gameObject);
        }
    }

    public void ProjectileBehavior(Collider2D other, bool direction)
    {
        if(direction == false)
        {
            projectileSpeed = -projectileSpeed;
            rend.flipX = false;
        }
} 
}
