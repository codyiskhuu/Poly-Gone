using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerKillZone : MonoBehaviour
{
    public GameObject spawnPoint;

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.transform.CompareTag("Player"))
        {
            GameObject.Find("healthManager").GetComponent<healthBar>().UpdateHealth(1, 'd');
            col.transform.position = new Vector3(spawnPoint.transform.position.x, spawnPoint.transform.position.y, spawnPoint.transform.position.z);
        }
    }

}
