using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Threading;
using UnityEngine;
using Debug = UnityEngine.Debug;
public class enemyMovement : MonoBehaviour
{
    public float speed;
    public float floor_distance;
    public float range;
    private float t_speed;
    public float stop_attack_range;
    public float wait_time_afterAttack;
    public float timer_attack;

    public bool movingRight;
    private bool player_Right;
    private bool player_inRange;
    private bool follow;
    public bool attack;


    public Transform groundDetection;//ray to check ground
    public Transform player;//the actual player distance 
    private Rigidbody2D rbody;

    void Awake()
    {
        rbody = GetComponent<Rigidbody2D>();
    }
    void flip() // get the transform of the enemy and flip him: might need to use this when we get animations working
    {
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;

    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy")) 
        {
            if (movingRight == true)//if the enemy is moving right, then change to the opposite direction
            {
                //flip();
                transform.eulerAngles = new Vector3(0, -180, 0);
                movingRight = false;
            }
            else //if the enemy is moving to the left, the change to the opposite direction
            {
                //flip();
                transform.eulerAngles = new Vector3(0, 0, 0);
                movingRight = true;
            }
        }

    }

    void Update()
    {
        Vector3 direction = player.position - transform.position;//distance between player and enemy
         //Debug.Log(direction.x);       
        if(direction.x < 0)//this means you are on the left side of the enemy
        {
            player_Right = false;
            if(movingRight == false && Mathf.Abs(direction.x) < range) //if the enemy is facing you and you are in range, it will follow you
            {
                //Debug.Log("left");
                player_inRange = true;
                follow = true;
            }
            else
            {
                player_inRange = false;
            }
        }
        else 
        {//this means you are on the right side of the enemy
            player_Right = true;
            if (movingRight == true && Mathf.Abs(direction.x) < range)//if the enemy is facing you and you are in range, it will follow you
            {
                //Debug.Log("right");
                player_inRange = true;
                follow = true;
            }
            else
            {
                player_inRange = false;
            }
        }



        if ((player_inRange == false) && (follow == false) && (timer_attack <= 0))//if you are out of stands range the enemy ignore you
        {
            //this enemy will walk to the edge and turn around so it does not fall off the stage
            transform.Translate(Vector2.right * speed * Time.deltaTime);//this is the speed of the enemy


            RaycastHit2D groundInfo = Physics2D.Raycast(groundDetection.position, Vector2.down, floor_distance);//this is the ray of the object, seeing if the enemy is near the edege or not
            attack = false;//not attacking

            if (groundInfo.collider == false)//if the enemy's ray indicator is of at the ledge then turn around
            {
                if (movingRight == true)//if the enemy is moving right, then change to the opposite direction
                {
                    //flip();
                    transform.eulerAngles = new Vector3(0, -180, 0);
                    movingRight = false;
                }
                else //if the enemy is moving to the left, the change to the opposite direction
                {
                    //flip();
                    transform.eulerAngles = new Vector3(0, 0, 0);
                    movingRight = true;
                }
            }
        }
        else //player in range
        {
            if (Mathf.Abs(direction.x) < stop_attack_range)//if you are within this range, the enemy will stop and attack you
            {
                t_speed = 0;
                transform.Translate(Vector2.right * t_speed * Time.deltaTime);
                //float x = Input.GetAxis("Horizontal");
                //float y = Input.GetAxis("vertical");
                //rbody.velocity = new Vector2(x * speed, y);
                Debug.Log("attack!!");
                //rbody.velocity = Vector3.zero;
                timer_attack = wait_time_afterAttack;
                attack = true;
            }
            else
            {
                
                if (timer_attack <= 0)
                {
                    attack = false;//not attacking
                    transform.Translate(Vector2.right * speed * Time.deltaTime);//keep moving
                }
                else
                {
                    timer_attack -= Time.deltaTime;
                }

            }
            if (Mathf.Abs(direction.x) > range)//if you are out of range, it will stop tracking you and following you
            {
                //Debug.Log("done following");
                follow = false;
            }
            if (player_Right == true && movingRight == false)//keep following
            {
                Debug.Log("keep following right");
                transform.eulerAngles = new Vector3(0, 0, 0);
                //flip();
                movingRight = true;
            }
            else if(player_Right == false && movingRight == true) //keep following
            {
                Debug.Log("keep following left");
                transform.eulerAngles = new Vector3(0, -180, 0);
                //flip();
                movingRight = false;
            }
            
            
        }


    }

}
