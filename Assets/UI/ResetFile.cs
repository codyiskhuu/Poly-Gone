using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResetFile : MonoBehaviour
{
    public GameObject Slot1text;
    public GameObject Slot2text;
    public GameObject Slot3text;
    // Start is called before the first frame update
    void Start()
    {
        
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
    public void reset()
    {
        PlayerPrefs.DeleteAll();
        refresh();

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
