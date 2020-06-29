using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuPage : MonoBehaviour
{
    public static bool enable = true;
    public GameObject StartMenu;
    public static bool switch2menu = false;
    public GameObject Loadpage;
    public void Switch2Menu()
    {
        Debug.Log("now switching");
        switch2menu = true;
        LoadPage.enable = false;
        if (LoadPage.switch2load) LoadPage.switch2load = false;

    }
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public float initialposition;
    // Update is called once per frame
    void Update()
    {
        if (switch2menu)
        {
            StartMenu.GetComponent<Transform>().localPosition.Set(0, initialposition, 0);
            StartMenu.SetActive(true);
            StartMenu.GetComponent<Transform>().localPosition.Set(0, initialposition, 0);
            if (StartMenu.GetComponent<Transform>().localPosition.y >1)
            {
                Loadpage.GetComponent<Transform>().Translate(0, -StartMenu.GetComponent<Transform>().localPosition.y / 100, 0);
                StartMenu.GetComponent<Transform>().Translate(0, -StartMenu.GetComponent<Transform>().localPosition.y / 100, 0);
            }
            else
            {
                switch2menu = false;
                Loadpage.SetActive(false);
                LoadPage.enable = true;
                Debug.Log("switching done");
            }
        }
    }
}
