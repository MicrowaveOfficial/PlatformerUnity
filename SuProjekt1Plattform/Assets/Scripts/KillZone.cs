using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillZone : MonoBehaviour
{
    [SerializeField] private Transform spawnPosition;
    private AudioSource audioSource;
    [SerializeField] private AudioClip fallSound;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            audioSource.PlayOneShot(fallSound, 1f);
            other.transform.position = spawnPosition.position;
            other.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }
    }
}
