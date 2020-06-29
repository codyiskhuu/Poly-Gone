using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadPage : MonoBehaviour
{
    public static bool enable = true;
    public GameObject StartMenu;
    public static bool switch2load = false;
    public GameObject Loadpage;
    public GameObject Slot1text;
    public GameObject Slot2text;
    public GameObject Slot3text;
    public void set4NewGame()
    {
        loadslot1.isNew = true;
    }
    public void set4Load()
    {
        loadslot1.isNew = false;
    }
    public void Switch2Load()
    {
        Debug.Log("now switching");
        switch2load = true;
        MenuPage.enable = false;
        if (MenuPage.switch2menu) MenuPage.switch2menu = false;

    }
    // Start is called before the first frame update
    void Start()
    {
        StartMenu.SetActive(true);
        Loadpage.SetActive(false);
        refresh();
    }

    public void refresh()
    {
        if (PlayerPrefs.HasKey("Slot1"))
        {
            string slot1 = PlayerPrefs.GetString("Slot1");
            Slot1text.GetComponent<Text>().text = "Slot1:" + slot1;
        }
        else
        {
            Slot1text.GetComponent<Text>().text = "Slot1:Empty";
        }
        if (PlayerPrefs.HasKey("Slot2"))
        {
            string slot2 = PlayerPrefs.GetString("Slot2");
            Slot2text.GetComponent<Text>().text = "Slot2:" + slot2;
        }
        else
        {
            Slot2text.GetComponent<Text>().text = "Slot2:Empty";
        }
        if (PlayerPrefs.HasKey("Slot3"))
        {
            string slot3 = PlayerPrefs.GetString("Slot3");
            Slot3text.GetComponent<Text>().text = "Slot3:" + slot3;
        }
        else
        {
            Slot3text.GetComponent<Text>().text = "Slot3:Empty";
        }
    }
    public float initialposition;
    // Update is called once per frame
    void Update()
    {
        refresh();
        if (switch2load)
        {
            Loadpage.GetComponent<Transform>().localPosition.Set(0, initialposition, 0);
            Loadpage.active = true;
            Loadpage.GetComponent<Transform>().localPosition.Set(0, initialposition, 0);
            if (Loadpage.GetComponent<Transform>().localPosition.y<-1)
            {
                Loadpage.GetComponent<Transform>().Translate(0,-Loadpage.GetComponent<Transform>().localPosition.y/100,0);
                StartMenu.GetComponent<Transform>().Translate(0, -Loadpage.GetComponent<Transform>().localPosition.y/100, 0);
            }
            else
            {
                switch2load = false;
                StartMenu.SetActive(false);
                MenuPage.enable = true;
                Debug.Log("switching done");
            }
        }
    }
}
