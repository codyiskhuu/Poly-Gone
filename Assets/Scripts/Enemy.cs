using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health;
    public bool invincible;
    private bool invis;
    public float invincibility_frames;
    public float invincible_timer = -1;
    private SpriteRenderer sprender;
    public bool damaged;
    public bool stun;
    // Start is called before the first frame update
    void Start()
    {
        invis = false;
        sprender = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(health <= 0 )
        {
            Destroy(gameObject);
        }
        if (invis == false && invincible_timer > 0)
        {
            invincible_timer -= Time.deltaTime;
            invis = true;
            sprender.enabled = false;
            
        }
        else
        {
            //invincible_timer -= Time.deltaTime;
            invis = false;
            sprender.enabled = true;
        }
        if(invincible_timer < 0 && invis == true)
        {
            stun = false;
        }
    }
    public void TakeDamage(int damage)
    {
        if (!invincible) {//prob can make the other eneimes not invincible just gotta manually apply it, but for the boss we can change that 
            health -= damage;
            Debug.Log("damage TAKEN");
            invincible = true;
            //stun = false;
            invincible_timer = invincibility_frames;
            damaged = true;
        }

    }
    public void setInvincibility()
    {
        if (invincible)
        {
            invincible = false;
            
        }
        else
        {
            invincible = true;
        }
    }
    public void setStun()
    {
        if (stun)
        {
            stun = false;
        }
        else
        {
            stun = true;
        }
    }
}
