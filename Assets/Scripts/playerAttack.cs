using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class playerAttack : MonoBehaviour
{
    public float timeBtwAttack;
    public float startTimeBtwAttack;

    public Transform attackPos;
    public LayerMask whatIsEnemies;
    public LayerMask boxes;
    public float attackRange;
    public int damage;
    public bool kepper; 

    public Animator animator;
    private int attackHashL = Animator.StringToHash("Base Layer.not_kirb_triangle_attack_L");
    private int attackHashR = Animator.StringToHash("Base Layer.not_kirb_triangle_attack_R");

    // Update is called once per frame
    void Update()
    {
        if(timeBtwAttack <= 0)//if you already attack, then you to wait a bit before attacking again
        {
            animator.SetBool("isAttacking", false);//not attacking
            if (Input.GetKey(KeyCode.Mouse0))//press the attack button
            {
                if (kepper) FindObjectOfType<AudioManager>().Play("Ora"); 
                else FindObjectOfType<AudioManager>().Play("Muda"); 

                animator.SetBool("isAttacking", true);//animatie the attack
                Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange, whatIsEnemies);//if enemy is in the range, grab it
                timeBtwAttack = startTimeBtwAttack;//set the time between attacks to the designated start time 

                for (int i = 0; i < enemiesToDamage.Length; i++)//attack the enemy with the damage
                {
                    enemiesToDamage[i].GetComponent<Enemy>().TakeDamage(damage);
                }

                enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange, boxes);
                for (int i = 0; i < enemiesToDamage.Length; i++)
                {
                    Destroy(enemiesToDamage[i].gameObject);
                    Debug.Log("Box Destroyed");
                }

            }

        }
        else
        {//reduce the time
            AnimatorStateInfo state = animator.GetCurrentAnimatorStateInfo(0);
            if(state.fullPathHash.GetHashCode() == attackHashL || state.fullPathHash.GetHashCode() == attackHashR)
            {
                Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange, whatIsEnemies);//if enemy is in the range, grab it

                for (int i = 0; i < enemiesToDamage.Length; i++)//attack the enemy with the damage
                {
                    enemiesToDamage[i].GetComponent<Enemy>().TakeDamage(damage);
                }

                enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange, boxes);
                for (int i = 0; i < enemiesToDamage.Length; i++)
                {
                    Destroy(enemiesToDamage[i].gameObject);
                    Debug.Log("Box Destroyed");
                }
            }
            timeBtwAttack -= Time.deltaTime;
        }

    }
    void OnDrawGizmosSelected()//gizmo to detect the attack positon
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }
    public void takeDamage(int damage, char d)
    {
        GameObject.Find("healthManager").GetComponent<healthBar>().UpdateHealth(damage, d);
    }
}
