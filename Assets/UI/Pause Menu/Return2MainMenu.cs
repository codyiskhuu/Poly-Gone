using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Return2MainMenu : MonoBehaviour
{
    public void return2main()
    {
        GameOver.isOver = false;
        Debug.Log("Start Again");
        Time.timeScale = 1;
        SceneManager.LoadScene("Start Menu");
        MenuPage.switch2menu = true;
        LoadPage.enable = false;
        if (LoadPage.switch2load) LoadPage.switch2load = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
