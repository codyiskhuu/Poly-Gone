using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    public GameObject pausemenu;
    public GameObject coins;
    public GameObject healthbar;
    public GameObject bosshealthbar;
    bool isPausing = false;
    // Start is called before the first frame update
    void Start()
    {
        pausemenu.active = false;
    }
   
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!GameOver.isOver)
            {
                if (!isPausing)
                {

                    Time.timeScale = 0;
                    pausemenu.active = true;
                    coins.active = false;
                    healthbar.active = false;
                    if (bosshealthbar != null)
                        bosshealthbar.active = false;
                    isPausing = !isPausing;

                }
                else
                {

                    Time.timeScale = 1;
                    pausemenu.active = false;
                    coins.active = true;
                    healthbar.active = true;
                    if (bosshealthbar != null)
                        bosshealthbar.active = true;
                    isPausing = !isPausing;
                }
            }
        }

    }
}
