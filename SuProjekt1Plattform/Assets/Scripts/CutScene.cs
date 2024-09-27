using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class CutScene : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float moveSpeed = 3f;
    private bool canMove = false;
    //[SerializeField] private bool cutSceneActive = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        GetComponent<Animator>().SetTrigger("Run");
        canMove = true;
    }

    private void FixedUpdate()
    {
        if (canMove == true)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
        }
        
        if (transform.position == target.position)
        {
            Destroy(gameObject);
        }
    }
}
