using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class enemyAttack : MonoBehaviour
{
    public float timeBtwAttack;
    public float startTimeBtwAttack;

    public Transform attackPos;
    public LayerMask whatIsEnemies;
    public float attackRange;
    public int damage;
    public GameObject theEnemy;

    public Animator animator;
    // Update is called once per frame


    void Update()
    {
        //GameObject theEnemy = GameObject.Find("Temp Enemy");
        enemyMovement enemyScript = theEnemy.GetComponent<enemyMovement>();
        
        if(timeBtwAttack <= 0)
        {
            animator.SetBool("finished_return", true);
            animator.SetBool("isAttacking", false);
 
            
            if (enemyScript.attack == true)//when the enemy is in range of the player && sword_time <= 0
            {
                //Debug.Log("attack!!");
                animator.SetBool("isAttacking", true);
                animator.SetBool("finished_return", true);

                Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange, whatIsEnemies); //grab the player tag
                timeBtwAttack = startTimeBtwAttack;

                for (int i = 0; i < enemiesToDamage.Length; i++)
                {
                    //GameObject.Find("healthManager").GetComponent<healthBar>().UpdateHealth(damage, 'd');
                    enemiesToDamage[i].GetComponent<playerAttack>().takeDamage(damage, 'd');
                }
                
            }

        }
        else
        {
            animator.SetBool("finished_return", false);
            timeBtwAttack -= Time.deltaTime;
        }

    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }
}
