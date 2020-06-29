using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class EnemyRangeAttack : MonoBehaviour
{
    public float timeBtwAttack;
    public float startTimeBtwAttack;
    public Transform AttackPos;
    public int damage;
    public GameObject enemy;
    public GameObject Shuriken;
    public Animator attack;
    
    void Start()
    {
        attack.SetBool("Attack", false);
    }
    // Update is called once per frame
    void Update()
    {
        enemyMovement enemyScript = enemy.GetComponent<enemyMovement>(); ;
        if (timeBtwAttack <= 0)
        {
                attack.SetBool("Attack",true);
                Instantiate(Shuriken, AttackPos.position, AttackPos.rotation);
                timeBtwAttack = startTimeBtwAttack;
        }
        else
        {
                attack.SetBool("Attack",false);
                timeBtwAttack -= Time.deltaTime;
        }
    }
}
