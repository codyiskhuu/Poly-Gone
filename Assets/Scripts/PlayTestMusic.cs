using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayTestMusic : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        FindObjectOfType<AudioManager>().Stop("Boss");
        FindObjectOfType<AudioManager>().Stop("Menu");
        FindObjectOfType<AudioManager>().Play("JoJo");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
