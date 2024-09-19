using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trampoline : MonoBehaviour
{
    [SerializeField] private float jumpForce = 300f;
    private AudioSource audioSource;
    [SerializeField] private AudioClip boing;
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            audioSource.PlayOneShot(boing, 1f);
            Rigidbody2D playerRigidbody = other.GetComponent<Rigidbody2D>();
            playerRigidbody.velocity = new Vector2(playerRigidbody.velocity.x, 0);
            playerRigidbody.AddForce(new Vector2(0, jumpForce));
            other.gameObject.GetComponent<PlayerMovement>().EnableDash();
            GetComponent<Animator>().SetTrigger("Jump");
        }
    }
}
