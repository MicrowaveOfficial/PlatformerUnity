using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    private Animator anim;
    private bool hasPlayedAnimation = false;
    [SerializeField] private GameObject box;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !hasPlayedAnimation)
        {
            box.SetActive(false);
            hasPlayedAnimation = true;
            anim.SetTrigger("Move");
        }
    }
}
