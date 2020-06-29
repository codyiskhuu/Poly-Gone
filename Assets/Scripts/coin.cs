using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coin : MonoBehaviour
{
    public int coinValue = 1;

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            coinManager.instance.ChangeScore(coinValue);
            healthBar.instance.UpdateHealth(coinValue, 'h');
        }
    }
}
