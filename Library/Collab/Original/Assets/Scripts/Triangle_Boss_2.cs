using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class Triangle_Boss_2 : MonoBehaviour
{
    private Animator anim;
    public GameObject player;
    public Transform playerPos; //the actual player distance 

    public int facingRight;
    public float speed;
    public int atk_cnt;

    string bossID;
    private int idleHash = Animator.StringToHash("Base Layer.Triangle_Boss_Idle");
    private int spinHash;
    private int spinLoopHash;
    public int tiredHash;
    //phase 1 items 
    private Rigidbody2D rb;
    public float dashSpeed;
    public float dashTime;
    public float startDashTime;
    public bool attack;
    public Transform players;//the actual player distance 
    public bool movingRight;


    public Camera MainCamera;
    private Vector2 screenBounds;

    void flip() // get the transform of the boss and flip him
    {
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;

    }

    void reposition() // get the position of the boss and reposition to edge of map
    {
        //Vector3 thePos = transform.position;
        ////thePos.y *= -1;
        //Debug.Log("pos: " + thePos.x);
        //Debug.Log("bounds: " + screenBounds.x);
        //if (facingRight < 0)
        //{
        //    thePos.x = -screenBounds.x + (float)0.5;
        //} else
        //{
        //    thePos.x = screenBounds.x - (float)0.5;
        //}
        //transform.position = thePos;
        float boss_x = transform.position.x;
        float player_y = playerPos.position.y;
        float boss_z = transform.position.z;
        transform.position.Set(boss_x, player_y, boss_z);


    }
    void phase1()
    {
        Vector3 direction = players.position - transform.position;//distance between player and enemy
        if (direction.x > 0)//if the boss is to the right of the player, it will jump to the right
        {
            if (!movingRight)
            {
                flip();
            }
            movingRight = true;
        }
        else if (direction.x < 0)//
        {
            if (movingRight)
            {
                flip();
            }
            movingRight = false;
        }

        if(atk_cnt >= 3)
        {
            anim.SetTrigger("isTired");
            atk_cnt = 0;
        }
        else if (dashTime <= 0)
        {
            attack = false;
            dashTime = startDashTime;
            rb.velocity = Vector2.zero;
        }
        else if (attack)
        {
            dashTime -= Time.deltaTime;

            anim.SetBool("isJumping", false);
        }
        else if (movingRight)
        {

            attack = true;
            rb.velocity = (Vector2.right * dashSpeed) + Vector2.up * dashSpeed;
            //Debug.Log("jumpinggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggg");
            anim.SetBool("isJumping", true);
            ++atk_cnt;
        }
        else if (!movingRight)
        {
            //Debug.Log("jumpinggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggg");
            attack = true;
            rb.velocity = (Vector2.left * dashSpeed) + Vector2.up * dashSpeed;
            anim.SetBool("isJumping", true);
            ++atk_cnt;
        }

    }

    void phase2()
    {
        Vector3 direction = playerPos.position - transform.position; //if positive, boss is facing player

        AnimatorStateInfo state = anim.GetCurrentAnimatorStateInfo(0);
        if (atk_cnt >= 3)
        {
            Debug.Log("go to tired state " + bossID);
            anim.SetTrigger("isTired");
            anim.SetBool("isAttacking", false);
            atk_cnt = 0;

        } else if (state.fullPathHash.GetHashCode() == idleHash)
        {
            Debug.Log("go to attack state " + bossID);
            anim.SetBool("isAttacking", true);
        }
        else if (direction.x * facingRight < 0 && anim.GetBool("atEdge")/*state.fullPathHash.GetHashCode() == idleHash*/) //if past edge, reposition if needed
        {
            Debug.Log("reposition " + bossID);
            reposition();       
            facingRight *= -1;
            flip();
            anim.SetBool("atEdge", false);
        }
        else if (state.fullPathHash.GetHashCode() == spinLoopHash && !(anim.GetBool("atEdge"))) //spin attack until it reaches the end of the map
        {
            Debug.Log("keep attacking " + bossID);
            transform.Translate(Vector2.right * facingRight * speed * Time.deltaTime); //this moves the boss at a set speed
            if ((transform.position.x < -screenBounds.x - 5 && facingRight < 0) || (transform.position.x > screenBounds.x + 5 && facingRight > 0))
            {
                anim.SetBool("atEdge", true);
                atk_cnt++;
            }
        }
        else
        {
            Debug.Log("just chillin " + bossID);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
        anim = GetComponent<Animator>();
        bossID = (GetComponent<SpriteRenderer>().name).Replace("TriangleBoss_", null);
        spinHash = Animator.StringToHash("Base Layer.Triangle_Boss_Spin_" + bossID);
        spinLoopHash = Animator.StringToHash("Base Layer.Triangle_Boss_Spin_Loop_" + bossID);
        tiredHash = Animator.StringToHash("Base Layer.Triangle_Boss_Tired_" + bossID);

        player = GameObject.Find("Player");
        playerPos = player.transform;
        facingRight = 1;
        
        //screenBounds = MainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, MainCamera.transform.position.z));
        
        //phase 2 setup
        anim.SetInteger("phase", 1);
        //transform.position = new Vector3(Convert.ToInt32(bossID) * 10, 0, 0);
        atk_cnt = 0;

        rb = GetComponent<Rigidbody2D>();
        dashTime = startDashTime;
        attack = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(anim.GetInteger("phase") == 1)
        {
            phase1();

        }
        if (anim.GetInteger("phase") == 2)
        {
            phase2();
        }
    }
}
