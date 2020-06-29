using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class playerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    public float speed;
    public float jumpForce;
    private float moveInput;

    public bool isGrounded;
    public Transform feetPos;
    public float checkRadius;
    public LayerMask whatIsGround;
    public LayerMask whatIsBox;

    private float jumpTimeCounter;
    public float jumpTime;
    private bool isJumping;

    private bool facingRight;
    public bool bossStuned;

    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); //get component of the players rigid body
        facingRight = true;//start the game facing right
        animator.SetBool("isRight", true);
        
    }

    void FixedUpdate()
    {
        moveInput = Input.GetAxisRaw("Horizontal"); //move left and right
        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);//speed calculations 
        //animator.SetFloat("moveSpeed", Mathf.Abs(moveInput));
    }
    
    void flip()
    {
        Vector3 theScale = transform.localScale; //get the players transfomration
        Debug.Log(theScale);
        theScale.x *= -1; //flip the x axis
        transform.localScale = theScale;//set it back to the player 
        Debug.Log(transform.localScale);


    }
    //coin interaction
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Coins"))//if you colide with this Tag: Delete it 
        {
            Destroy(other.gameObject);
        }
        
        if (other.gameObject.CompareTag("Enemy"))
        {
           bossStuned = other.gameObject.GetComponent<Enemy>().stun;
           if (bossStuned == false)
           {
                GameObject.Find("healthManager").GetComponent<healthBar>().UpdateHealth(1, 'd');
           }
            
        }

    }

    // Update is called once per frame
    void Update()
    {
        
        isGrounded = Physics2D.OverlapCircle(feetPos.position, checkRadius, whatIsGround);//ground check with the feet position
        if (isGrounded == false)
        {
            isGrounded = Physics2D.OverlapCircle(feetPos.position, checkRadius, whatIsBox);//ground check with the feet position
        }
        //animator.SetBool("isAttacking", true);
        
        if(moveInput > 0)//moving right
        {
            if (!facingRight)//not facing right then flip it 
            {
                facingRight = true;
                flip();
                animator.SetBool("isRight", true);
            }
            animator.SetBool("isIdle", false);//animating that you are moving
        }
        else if(moveInput < 0)//moving left
        {
            if (facingRight)//if you are facing right then flip it
            {
                facingRight = false;;
                flip();
                animator.SetBool("isRight", false);
            }
            animator.SetBool("isIdle", false);//animating that you are moving 
        }
        else
        {
            animator.SetBool("isIdle", true);//not moving at  all
        }



        if (isGrounded == true && Input.GetKeyDown(KeyCode.Space)) //if you are grounded and press the jump button
        {
            isJumping = true;//you are in the air
            
            jumpTimeCounter = jumpTime; //how long you can jump
            rb.velocity = Vector2.up * jumpForce; //calculation on how high you jump
            animator.SetBool("isJumping", true);//start the jumping animation 
        }
        if (Input.GetKey(KeyCode.Space) && isJumping == true)//this is for holding down the space bar and jump slightly higher 
        {
            if(jumpTimeCounter > 0)//if the amount of time of you jump is still good then keep it like it 
            {
                rb.velocity = Vector2.up * jumpForce;//keep going
                jumpTimeCounter -= Time.deltaTime;//decrease the timer
            }
            else
            {
                isJumping = false;
                animator.SetBool("isJumping", false);//done then stop thje animation 
            }
            
        }
        else if (isGrounded == true)//if you hit the ground then stop the animation 
        {
            animator.SetBool("isJumping", false);
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            isJumping = false;
        }


    }
}
