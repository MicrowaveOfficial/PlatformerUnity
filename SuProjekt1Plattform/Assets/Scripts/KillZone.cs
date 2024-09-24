using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillZone : MonoBehaviour
{
    [SerializeField] private Transform spawnPosition;
    [SerializeField] private AudioClip fallSound;
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            //audioSource.PlayOneShot(fallSound, 1f);
            other.GetComponent<PlayerMovement>().KillPlayer();
        }
    }
}
