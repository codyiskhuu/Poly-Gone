using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuPlayMusic : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        FindObjectOfType<AudioManager>().Stop("Level");
        FindObjectOfType<AudioManager>().Stop("Boss");
        FindObjectOfType<AudioManager>().Stop("JoJo");
        FindObjectOfType<AudioManager>().Play("Menu");
    }

}
