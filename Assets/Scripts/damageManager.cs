using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class damageManager : MonoBehaviour
{
    public int spikeValue = 1;

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            healthBar.instance.UpdateHealth(spikeValue, 'd');
            
        }
    }
}
