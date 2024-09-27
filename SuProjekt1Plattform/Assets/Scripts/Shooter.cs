using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    //[SerializeField] private float coolDown = 1.3f;
    [SerializeField] private GameObject projectileLeft;
    [SerializeField] private GameObject projectileRight;
    [SerializeField] private GameObject player;
    [SerializeField] private float projectileOffsetX = 0.8f;
    [SerializeField] private float projectileOffsetY = 0.8f;
    private bool Shooting;

    private SpriteRenderer rend;
    private Animator anim; 

    private void Start()
    {
        rend = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.CompareTag("Player"))
        {
            anim.SetBool("Shooting", true);
        }
    }

    public void SpawnProjectile()
    {
        if (player.transform.position.x > transform.position.x)
        {
            rend.flipX = true;
            Instantiate(projectileLeft, new Vector3(transform.position.x + -projectileOffsetX, transform.position.y - projectileOffsetY, transform.position.z), Quaternion.identity);
        }
        else
        {
            Instantiate(projectileRight, new Vector3(transform.position.x + -projectileOffsetX, transform.position.y - projectileOffsetY, transform.position.z), Quaternion.identity);
            rend.flipX = false;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))  anim.SetBool("Shooting", false);
    }
}
