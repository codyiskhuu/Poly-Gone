using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class Coin_Collect : MonoBehaviour
{
    private float collecttime;
    // Update is called once per frame
    void Update()
    {
        if(collecttime > 0) { 
            collecttime -= Time.deltaTime;
            //Debug.Log(collecttime);
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.GetType() == typeof(CircleCollider2D))
        {
            if (collider.gameObject.tag == "Coins" && (collecttime <= 0))
            {
                GameObject.Find("coinManager").GetComponent<coinManager>().ChangeScore(1);
                Destroy(collider.gameObject);
                collecttime = 0.1f;
            }
        }
    }


}
