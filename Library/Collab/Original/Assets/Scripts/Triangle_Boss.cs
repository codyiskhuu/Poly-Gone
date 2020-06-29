using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class Triangle_Boss : MonoBehaviour
{
    private Animator anim;
    public GameObject player;
    public Transform playerPos; //the actual player distance 

    public Vector3 startPos;
    public int facingRight;
    public float speed;
    public int atk_cnt;

    public string bossID;
    private int idleHash = Animator.StringToHash("Base Layer.Triangle_Boss_Idle");
    private int spinHash;
    private int spinLoopHash;
    private int tiredHash;
    private int dazedHash = Animator.StringToHash("Base Layer.Triangle_Boss_Dazed");

    //phase 1 items 
    private Rigidbody2D rb;
    public float dashSpeed;
    public float dashTime;
    public float startDashTime;
    public bool attack;
    public bool movingRight;

    private bool invincible;


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
        float boss_x = transform.position.x;
        float player_y = playerPos.position.y;
        float boss_z = transform.position.z;
        transform.position.Set(boss_x, player_y, boss_z);


    }
    /*
    private void OnCollisionStay2D(Collision2D collision)//if you are colliding with the ground: set it to true
    {
        if (collision.collider.tag == "Ground")
        {
            GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
        }


    }

    private void OnCollisionExit2D(Collision2D collision)//if you exit the collsion: then you are not on the ground
    {

         GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;

    }*/

    void phase1()
    {
        Vector3 direction = playerPos.position - transform.position;//distance between player and enemy

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
            attack = false;
            if (GetComponent<Enemy>().invincible == true)
            {
                GetComponent<Enemy>().setInvincibility();//set invinciblity off
                //GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
            }
            

        }
        else if (dashTime <= 0)
        {
            attack = false;
            dashTime = startDashTime;
            rb.velocity = Vector2.zero;
            if(atk_cnt == 0 && GetComponent<Enemy>().invincible == false)
            {
                GetComponent<Enemy>().setInvincibility(); //set invincibility on
                //GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            }
            
        }
        else if (attack)
        {
            dashTime -= Time.deltaTime;

            anim.SetBool("isJumping", false);
        }
        else if (movingRight)
        {
            //GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            attack = true;
            rb.velocity = (Vector2.right * dashSpeed) + Vector2.up * dashSpeed;
            //Debug.Log("jumpinggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggg");
            anim.SetBool("isJumping", true);
            ++atk_cnt;
        }
        else if (!movingRight)
        {
            //GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
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

        AnimatorStateInfo state = anim.GetCurrentAnimatorStateInfo(0); //current state

        if (atk_cnt >= 3)
        {
            //Debug.Log("go to tired state " + bossID);
            anim.SetTrigger("isTired");
            anim.SetBool("isAttacking", false);
            atk_cnt = 0;

        } else if (state.fullPathHash.GetHashCode() == idleHash)
        {
            //Debug.Log("go to attack state " + bossID);
            anim.SetBool("isAttacking", true);
        }
        else if (direction.x * facingRight < 0 && anim.GetBool("atEdge")) //if past edge, reposition if needed
        {
            //Debug.Log("reposition " + bossID);
            reposition();       
            facingRight *= -1;
            flip();
            anim.SetBool("atEdge", false);
        }
        //spin attack until it reaches the end of the map
        else if (state.fullPathHash.GetHashCode() == spinLoopHash && !anim.GetBool("atEdge"))  // check state and animator parameters
        {
            //Debug.Log("keep attacking " + bossID);
            transform.Translate(Vector2.right * facingRight * speed * Time.deltaTime); //this moves the boss at a set speed
            //makes sure boss spins from one end to the other
            if ((transform.position.x < -screenBounds.x - 5 && facingRight < 0) || (transform.position.x > screenBounds.x + 5 && facingRight > 0)) 
            {
                anim.SetBool("atEdge", true);
                atk_cnt++;
            }
        }
        else
        {
            //Debug.Log("just chillin " + bossID);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        //setup animation states
        anim = GetComponent<Animator>();
        bossID = (GetComponent<SpriteRenderer>().name).Replace("TriangleBoss_", null);
        spinHash = Animator.StringToHash("Base Layer.Triangle_Boss_Spin_" + bossID);
        spinLoopHash = Animator.StringToHash("Base Layer.Triangle_Boss_Spin_Loop_" + bossID);
        tiredHash = Animator.StringToHash("Base Layer.Triangle_Boss_Tired_Loop_" + bossID);

        //find the player
        player = GameObject.Find("Player");
        playerPos = player.transform;
        facingRight = 1;
        
        //set boundaries
        //screenBounds = MainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, MainCamera.transform.position.z));
        
        //phase 2 setup
        anim.SetInteger("phase", 1);
        //startPos = transform.position;
        atk_cnt = 0;

        rb = GetComponent<Rigidbody2D>();
        dashTime = startDashTime;
        attack = true;
        GetComponent<Enemy>().setInvincibility();
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
            GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
            phase2();
        }
    }
}
