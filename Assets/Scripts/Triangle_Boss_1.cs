using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Triangle_Boss_1 : MonoBehaviour
{
    private Rigidbody2D rb;
    public float dashSpeed;
    public float dashTime;
    public float startDashTime;
    public bool attack;
    public Transform player;//the actual player distance 
    public bool movingRight;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        dashTime = startDashTime;
        attack = true;
        Vector3 direction = player.position - transform.position;//distance between player and enemy
        if (direction.x > 0)//if the boss is to the right of the player, it will jump to the right
        {
            movingRight = true;
        }
        else if (direction.x < 0)//
        {
            movingRight = false;
        }
    }
    void flip() // get the transform of the enemy and flip him: might need to use this when we get animations working
    {
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = player.position - transform.position;//distance between player and enemy
        //RaycastHit2D groundInfo = Physics2D.Raycast(groundDetection.position, Vector2.down, floor_distance);//this is the ray of the object, seeing if the enemy is near the edege or not
        if(direction.x > 0)//if the boss is to the right of the player, it will jump to the right
        {
            if (!movingRight)
            {
                flip();
            }
            movingRight = true;
        }
        else if(direction.x < 0)//
        {
            if (movingRight)
            {
                flip();
            }
            movingRight = false;
        }


        if (dashTime <= 0)
        {
            attack = false;
            dashTime = startDashTime;
            rb.velocity = Vector2.zero;
        }
        else if (attack)
        {
            dashTime -= Time.deltaTime;
        }
        else if(movingRight)
        {
            
            attack = true;
            rb.velocity = (Vector2.right * dashSpeed) + Vector2.up * dashSpeed;
        }
        else if (!movingRight)
        {
            
            attack = true;
            rb.velocity = (Vector2.left * dashSpeed) + Vector2.up * dashSpeed;
        }
        
    }
}
