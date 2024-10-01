using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private GameObject attackArea = default;
    private GameObject attackArea2 = default;
    private Animator anim;

    private float timeToAttack = 0.25f;
    private float timer = 0f;
  
    void Start()
    {
        attackArea = transform.GetChild(0).gameObject;
        attackArea2 = transform.GetChild(1).gameObject;
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            Attack();
        }

        //if (attacking)
        //{
        //    timer += Time.deltaTime;

        //    if (timer >= timeToAttack)
        //    {
        //        timer = 0;
        //        attacking = false;
        //        attackArea.SetActive(attacking);
        //    }
        //}
      
    }

    private void Attack()
    {
        anim.SetBool("isAttacking", true);    
    }

    public void stopAttack()
    {
        attackArea.SetActive(true);
        attackArea2.SetActive(true);
        anim.SetBool("isAttacking", false);
        Invoke("AttackAreaOff", 0.2f);
    }

    private void AttackAreaOff()
    {
        attackArea.SetActive(false);
        attackArea2.SetActive(false);
    }
}
