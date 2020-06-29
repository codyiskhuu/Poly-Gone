using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using Debug = UnityEngine.Debug;
using System.Security.Cryptography;
using System.Collections.Specialized;


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
    private int dazedHash = Animator.StringToHash("Base Layer.Triangle_Boss_Daze_Loop");

    //phase 1 items 
    private Rigidbody2D rb;
    public float dashSpeed;
    public float dashTime;
    public float startDashTime;
    public bool attack;
    public bool movingRight;

    private bool invincible;
    private int number;

    private float warntime = 0f;
    public GameObject right;
    public GameObject left;

    public Camera MainCamera;
    private Vector2 screenBounds;

    private int totalHealth;
    public int phase1H;
    public int phase2H;
    public int phase3H;

    public float stunTime;
    public float stunned;
    public float transitionTime;

    void flip() // get the transform of the boss and flip him
    {
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;

    }

    void warning()
    {
        if (warntime > 0)
        {
            warntime -= Time.deltaTime;
            if ((right.activeInHierarchy && left.activeInHierarchy))
            {
                right.SetActive(false);
                left.SetActive(false);
            }
            else
            {
                right.SetActive(true);
                left.SetActive(true);
            }
        }
        else
        {
            right.SetActive(false);
            left.SetActive(false);
        }
        //Debug.Log("Warning " + (warntime));
    }

    void reposition() // get the position of the boss and reposition to edge of map
    {
        //Debug.Log("reposition " + bossID);
        //Debug.Log("Old Position: " + transform.position);
        float boss_x = transform.position.x;
        float player_y = playerPos.position.y;
        float boss_z = transform.position.z;
        transform.position = new Vector3(boss_x, player_y, boss_z);
        Debug.Log("New Position: " + (boss_x, player_y, boss_z));


    }
    private void OnCollisionEnter2D(Collision2D collision)//if you are colliding with the ground: set it to true
    {
        if (collision.collider.tag == "Player")
        {
            GameObject.Find("healthManager").GetComponent<healthBar>().UpdateHealth(1, 'd');
        }

    }
    void phase1()
    {
        Vector3 direction = playerPos.position - transform.position;//distance between player and enemy
        if (GetComponent<Enemy>().health == (totalHealth - phase1H) )
        {
            if (transitionTime > 0)
            {
                rb.velocity = Vector2.down * dashSpeed;
                transitionTime -= Time.deltaTime;
            }
            else
            {
                GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
                anim.SetBool("isAttacking", true);
                anim.SetInteger("phase", 2);
                totalHealth -= phase1H;
                if (!movingRight)
                {
                    facingRight = -1;
                }
            }

        }
        else
        {
            if (bossID == "1")
            {
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

                if (atk_cnt >= 4)
                {
                    anim.SetTrigger("isTired");
                    atk_cnt = 0;
                    attack = false;
                    stunned = stunTime;    
                    if (GetComponent<Enemy>().invincible == true)
                    {
                        GetComponent<Enemy>().setStun();
                        GetComponent<Enemy>().setInvincibility();//set invinciblity off
                    }
                }
                else if(stunned > 0)
                {
                    stunned -= Time.deltaTime;
                    anim.SetBool("isJumping", false);
                }
                else if (dashTime <= 0)
                {
                    attack = false;
                    dashTime = startDashTime;
                    rb.velocity = Vector2.zero;
                    
                    if (atk_cnt == 1 && GetComponent<Enemy>().invincible == false)
                    {
                        GetComponent<Enemy>().setStun();
                        GetComponent<Enemy>().setInvincibility(); //set invincibility on
                    }
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
                    anim.SetBool("isJumping", true);
                    ++atk_cnt;
                }
                else if (!movingRight)
                {
                    attack = true;
                    rb.velocity = (Vector2.left * dashSpeed) + Vector2.up * dashSpeed;
                    anim.SetBool("isJumping", true);
                    ++atk_cnt;
                }
            }
            else if (bossID == "2")
            {
                if (atk_cnt >= 4)
                {
                    anim.SetTrigger("isTired");
                    atk_cnt = 0;
                    attack = false;
                    stunned = stunTime;
                    if (GetComponent<Enemy>().invincible == true)
                    {
                        GetComponent<Enemy>().setStun();
                        GetComponent<Enemy>().setInvincibility();//set invinciblity off
                    }
                }
                else if (stunned > 0)
                {
                    stunned -= Time.deltaTime;
                    anim.SetBool("isJumping", false);
                }
                else if (dashTime <= 0)
                {
                    number = (UnityEngine.Random.Range(0, 255)) % 3;

                    if (number == 0)//if the boss is to the right of the player, it will jump to the right
                    {
                        if (!movingRight)
                        {
                            flip();
                        }
                        movingRight = true;
                    }
                    else if (number == 1)//
                    {
                        if (movingRight)
                        {
                            flip();
                        }
                        movingRight = false;
                    }
                    attack = false;
                    dashTime = startDashTime;
                    rb.velocity = Vector2.zero;
                    if (atk_cnt == 1 && GetComponent<Enemy>().invincible == false)
                    {
                        GetComponent<Enemy>().setStun();
                        GetComponent<Enemy>().setInvincibility(); //set invincibility on
                    }
                }
                else if (attack)
                {
                    dashTime -= Time.deltaTime;
                    anim.SetBool("isJumping", false);
                }
                else if (number == 2)
                {
                    attack = true;
                    rb.velocity = Vector2.up * dashSpeed * 2;
                    anim.SetBool("isJumping", true);
                    ++atk_cnt;
                }
                else if (movingRight)
                {
                    attack = true;
                    rb.velocity = (Vector2.right * dashSpeed) + Vector2.up * dashSpeed;
                    anim.SetBool("isJumping", true);
                    ++atk_cnt;
                }
                else if (!movingRight)
                {
                    attack = true;
                    rb.velocity = (Vector2.left * dashSpeed) + Vector2.up * dashSpeed;
                    anim.SetBool("isJumping", true);
                    ++atk_cnt;
                }

            }
        }
    }

    void phase2()
    {
        Vector3 direction = playerPos.position - transform.position; //if positive, boss is facing player

        AnimatorStateInfo state = anim.GetCurrentAnimatorStateInfo(0); //current state

        if (GetComponent<Enemy>().health == (totalHealth - phase1H))
        {
            if (transitionTime > 0)
            {
                rb.velocity = Vector2.down * dashSpeed;
                transitionTime -= Time.deltaTime;
            }
            else
            {
                Debug.Log("go to next phase " + bossID);
                GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
                anim.SetBool("isAttacking", true);
                transitionTime = 2;
                anim.SetInteger("phase", 3);
                totalHealth -= phase1H;
            }
            return;
        }

        if (atk_cnt >= 3)
        {
            Debug.Log("go to tired state " + bossID);
            anim.SetTrigger("isTired");
            anim.SetBool("isAttacking", false);
            atk_cnt = 0;
            if (GetComponent<Enemy>().invincible == true)
            {
                GetComponent<Enemy>().setStun();
                GetComponent<Enemy>().setInvincibility();//set invinciblity off
            }

        } else if (state.fullPathHash.GetHashCode() == idleHash)
        {
            Debug.Log("go to attack state " + bossID);
            GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
            anim.SetBool("isAttacking", true);
            if (GetComponent<Enemy>().invincible == false)
            {
                GetComponent<Enemy>().setStun();
                GetComponent<Enemy>().setInvincibility(); //set invincibility on
            }
        }
        else if (direction.x * facingRight < 0 && anim.GetBool("atEdge")) //if past edge, reposition if needed
        {
            Debug.Log("reposition " + bossID);
            if (bossID == "1")
            {
                reposition();
            }  
            facingRight *= -1;
            flip();
            anim.SetBool("atEdge", false);
        }
        //spin attack until it reaches the end of the map
        else if (state.fullPathHash.GetHashCode() == spinLoopHash && !anim.GetBool("atEdge"))  // check state and animator parameters
        {
            Debug.Log("keep attacking " + bossID);
            if (GetComponent<Rigidbody2D>().bodyType == RigidbodyType2D.Dynamic)
            {
                GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
            }
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
            GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            GetComponent<Rigidbody2D>().mass = 10000;
            Debug.Log("just chillin " + bossID);
        }
    }

    void phase3()
    {
        Vector3 direction = playerPos.position - transform.position; //if positive, boss is facing player

        AnimatorStateInfo state = anim.GetCurrentAnimatorStateInfo(0); //current state
        if (atk_cnt == 2)
        {
            if (transform.position.x > -1.162571 && transform.position.x < -0.0197382)
            {
                anim.SetBool("isAttacking", false);
                anim.SetTrigger("isDazed");
                GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            }
        }
        if (state.fullPathHash.GetHashCode() == dazedHash)
        {
            if(GetComponent<Enemy>().invincible == true && GetComponent<Enemy>().damaged == false)
            {
                GetComponent<Enemy>().setInvincibility();
            }
            atk_cnt = 0;
        }
        else if (state.fullPathHash.GetHashCode() == idleHash)
        {
            GetComponent<Enemy>().damaged = false;
            if (GetComponent<Rigidbody2D>().bodyType == RigidbodyType2D.Dynamic)
            {
                GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
            }
            if ((transform.position.y  > -screenBounds.y) || (transform.position.y < screenBounds.y))
            {
                transform.Translate(Vector2.up * 5 * Time.deltaTime);
                warntime = 2f;
            }
            if ((transform.position.y < -screenBounds.y - 5 ) || (transform.position.y > screenBounds.y + 5 ))
            {

                if(bossID == "1")
                {
                    transform.position = new Vector3(-18.3f, -7.6f, 0f);
                }
                if (bossID == "2")
                {
                    transform.position = new Vector3(17.7f, -1.8f, 0f);
                    //reposition();
                    if (facingRight == 1)
                    {
                        facingRight *= -1;
                        flip();
                    }
                    
                }
                anim.SetBool("isAttacking", true);
                //transform.Translate(0,0,0);
            }
            warning();
        }
        else if (direction.x * facingRight < 0 && anim.GetBool("atEdge")) //if past edge, reposition if needed
        {
            //Debug.Log("reposition " + bossID);
            //reposition();
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
                if(atk_cnt == 2)
                {
                    if(bossID == "2")
                    {
                        transform.position = new Vector3(17.7f, -6.516125f, 0f);
                    }
                    if(bossID == "1")
                    {
                        transform.position = new Vector3(-17.7f, -6.516125f, 0f);
                    }
                }
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
        screenBounds = MainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, MainCamera.transform.position.z));

        //boss fight setup
        anim.SetInteger("phase", 3);
        speed = 15;
        startPos = transform.position;
        atk_cnt = 0;
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        rb = GetComponent<Rigidbody2D>();
        dashTime = startDashTime;
        attack = true;
        GetComponent<Enemy>().setInvincibility();
        totalHealth = GetComponent<Enemy>().health;
        transitionTime = 2;
    }

    // Update is called once per frame
    void Update()
    {
        /*if(anim.GetInteger("phase") == 1)
        {
            phase1();

        }
        if (anim.GetInteger("phase") == 2)
        {

            phase2();
        }*/
        //if (anim.GetInteger("phase") == 3)
        //{
           
            phase3();
        //}
    }
}
