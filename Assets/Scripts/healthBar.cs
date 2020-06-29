using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System.Reflection;
using System.Diagnostics;
using Debug = UnityEngine.Debug;

public class healthBar : MonoBehaviour
{
    public static healthBar instance;
    public GameObject bar;
    public GameObject gameover;
    public GameObject coinui;
    public GameObject hpui;
    public GameObject player;
    private SpriteRenderer sprender;

    public int Maxhealth;
    private int health;
    public Image image;
    private float step;
    public float invincibility_frames;
    public float invincible_timer = -1;
    private Rigidbody2D rb;
    private BoxCollider2D objCollider;
    private CircleCollider2D circCollider;
    private bool invis;

    void Start()
    {
        sprender = player.GetComponent<SpriteRenderer>();
        if (instance == null)
        {
            instance = this;
        }

        invis = false;
        if (GameObject.Find("BossHP") != null)
        {
            GameObject.Find("BossHP").SetActive(true);
        }
        step = image.fillAmount / Maxhealth;
        health = Maxhealth;
        hpui.SetActive(true);
        coinui.SetActive(true);
        //rb = player.GetComponent<Rigidbody2D>();
        objCollider = player.GetComponent<BoxCollider2D>();
        circCollider = player.GetComponent<CircleCollider2D>();
    }

    void Update()
    {
        //Debug.Log(health);
        if (invis == false && invincible_timer > 0)
        {
            invincible_timer -= Time.deltaTime;
            invis = true;
            sprender.enabled = false;
            //rb.isKinematic = true;
            objCollider.isTrigger = true;
            circCollider.isTrigger = true;
        }
        else
        {
            //invincible_timer -= Time.deltaTime;
            invis = false;
            sprender.enabled = true;
        }
        if(invincible_timer < 0)
        {
            objCollider.isTrigger = false;
            circCollider.isTrigger = false;
        }

    }

    public void UpdateHealth(int x, char c)//value of health // heal or dmg
    {
        Debug.Log(health);
        if(health < Maxhealth && c == 'h')//'h' is the healing
        {
            health += x;
            image.fillAmount =  step;
            Debug.Log("Heal!");
        }
        if(health > 0 && c == 'd' && invincible_timer < 0)//'d' is taking damage
        {
            FindObjectOfType<AudioManager>().Play("Oof");
            Invincibility();
            health -= x;
            image.fillAmount -= step;
            Debug.Log("Damage!");
        }
        if(health == 0)//if you have no HP then gameover 
        {
            
            Debug.Log("Died!");
            gameOver();
        }
    }

    public int GetHealth()
    {
        return health;
    }
    public void Invincibility()
    {
        invincible_timer = invincibility_frames;//set the time so that you don't get hit again

    }


    public void gameOver()//do the gameover scene
    {
        FindObjectOfType<AudioManager>().Play("Ow");
        Debug.Log("Died!");
        Time.timeScale = 0;
        gameover.SetActive(true);
        if (GameObject.Find("BossHP") != null)
        {
            GameObject.Find("BossHP").SetActive(false);
        }
        hpui.SetActive(false);
        coinui.SetActive(false);
        GameOver.isOver = true;
        Time.timeScale = 0;
        FindObjectOfType<AudioManager>().Stop("Level");
    }
}
