using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySongTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        FindObjectOfType<AudioManager>().Stop("Menu");
        FindObjectOfType<AudioManager>().Stop("JoJo");
        FindObjectOfType<AudioManager>().Stop("Boss");
        FindObjectOfType<AudioManager>().Play("Level");
    }

}
