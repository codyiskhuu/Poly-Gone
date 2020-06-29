using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayBossMusic : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        FindObjectOfType<AudioManager>().Stop("Level");
        FindObjectOfType<AudioManager>().Stop("Menu");
        FindObjectOfType<AudioManager>().Stop("JoJo");
        FindObjectOfType<AudioManager>().Play("Boss");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
