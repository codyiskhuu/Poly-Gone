using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerRespawn : MonoBehaviour
{
    public GameObject player;
    //public GameObject spawn;
    // Start is called before the first frame update
    void Start()
    {
        transform.position = player.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        playerMovement isGrounded = player.GetComponent<playerMovement>();


        if ((isGrounded.isGrounded == true))
        {
            transform.position = player.transform.position;
        }
    }
}
