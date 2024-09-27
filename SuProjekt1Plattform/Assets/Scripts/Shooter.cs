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
    private bool canShoot = true;
    private bool Shooting;

    private SpriteRenderer rend;
    private Animator anim; 

    private void Start()
    {
        rend = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        //if (!canMove)     
        //    return;   
        
        //transform.Translate(new Vector2 (moveSpeed, 0) * Time.deltaTime);

        //if (moveSpeed < 0)
        //{
        //    rend.flipX = true;
        //    direction = false;
        //}

        //if (moveSpeed > 0)
        //{
        //    rend.flipX = false;
        //    direction = true;
        //}
    }

    //private void OnCollisionEnter2D(Collision2D other)
    //{
    //    print("collision");
    //    if (other.gameObject.CompareTag("EnemyBlock"))
    //    {
    //        print("enemy");
    //        moveSpeed = -moveSpeed;
    //    }

    //    if (other.gameObject.CompareTag("Enemy"))
    //    {
    //        moveSpeed = -moveSpeed;
    //    }
    //}


    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.CompareTag("Player"))
        {
            anim.SetBool("Shooting", true);
            canShoot = true;
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

    public void Test()
    {
        print("Hej");
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))  anim.SetBool("Shooting", false);
        canShoot = false;
    }

    private void ReadyShoot()
    {
        canShoot = true;
    }
}
