using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Cryptography;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class Shoot : MonoBehaviour
{
    public float speed = 100;
    public Rigidbody2D rb;
    public int damage = 1;

    // Update is called once per frame
    void Update()
    {
        rb.velocity = transform.right * -speed;
        
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.tag == "Player")
        {
            GameObject.Find("healthManager").GetComponent<healthBar>().UpdateHealth(damage, 'd');
            Destroy(gameObject);
        }
        
    }

}
