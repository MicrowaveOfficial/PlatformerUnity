using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Shooter : MonoBehaviour
{
    //[SerializeField] private float coolDown = 1.3f;
    [SerializeField] private GameObject projectileLeft;
    [SerializeField] private GameObject projectileRight;
    [SerializeField] private GameObject player;
    [SerializeField] private float projectileOffsetX = 0.8f;
    [SerializeField] private float projectileOffsetY = 0.8f;
    [SerializeField] private int damageGiven = 1;
    [SerializeField] private int bossHealth = 10;
    private bool Shooting;
    public bool isBoss;
   

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
        if (!isBoss)
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
        else
        {
            float randomY = Random.Range(-2f, 3.5f);
            if (player.transform.position.x > transform.position.x)
            {
                rend.flipX = true;
                Instantiate(projectileLeft, new Vector3(transform.position.x + -projectileOffsetX, transform.position.y - randomY, transform.position.z), Quaternion.identity);
            }
            else
            {
                Instantiate(projectileRight, new Vector3(transform.position.x + -projectileOffsetX, transform.position.y - randomY, transform.position.z), Quaternion.identity);
                rend.flipX = false;
            }
        }

    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))  anim.SetBool("Shooting", false);
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<PlayerMovement>().TakeDamage(damageGiven);
        }

        //if (other.gameObject.CompareTag("AttackArea"))
        //{
        //    Hurt();
        //}
    }

    public void Hurt()
    {
        bossHealth--;
        print("Tjena");

            if(bossHealth <= 0)
            {
                Die();
            }      
    }

    private void Die()
    {
        GetComponent<Animator>().SetTrigger("Die");
        GetComponent<BoxCollider2D>().enabled = false;
        GetComponent<CircleCollider2D>().enabled = false;
        Invoke("LoadMainMenu", 5.0f);
    }
    private void LoadMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    private void Update()
    {
        if(bossHealth <= 0)
        {
            Die();
        }
    }
}
