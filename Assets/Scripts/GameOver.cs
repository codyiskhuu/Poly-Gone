using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    public static bool isOver = false;
    public GameObject gameover;
    public void showgameover()
    {
        isOver = true;
        Debug.Log("isOver");
        Time.timeScale = 0;
        gameover.SetActive(true);
    }
    // Start is called before the first frame update
    void Start()
    {
        gameover.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
