using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class grounded : MonoBehaviour
{
    GameObject Player;
    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        Player = gameObject.transform.parent.gameObject;
        Debug.Log("Start Grounded");
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionStay2D(Collision2D collision)//if you are colliding with the ground: set it to true
    {
        if (collision.collider.tag == "Ground")
        {
            Player.GetComponent<charMove>().isGrounded = true;
            //animator.SetBool("isJumping", false);
        }


    }

    private void OnCollisionExit2D(Collision2D collision)//if you exit the collsion: then you are not on the ground
    {
        if (collision.collider.tag == "Ground")
        {
            Player.GetComponent<charMove>().isGrounded = false;
            Debug.Log("NotGrounded!");
        }
    }
}
