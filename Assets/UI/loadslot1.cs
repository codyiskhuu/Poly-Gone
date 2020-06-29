using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class loadslot1 : MonoBehaviour
{
    public static bool isNew = false;
    public void load1()
    {
        if (isNew)
        {
            PlayerPrefs.SetInt("isNew", 1);
            PlayerPrefs.SetString("Slot1", "level1");
            string slot1 = PlayerPrefs.GetString("Slot1");
            string load;
            Load.slot2Load.TryGetValue(slot1, out load);
            SceneManager.LoadScene(load);
        }
        else
        {
            if (PlayerPrefs.HasKey("Slot1"))
            {
                string slot1 = PlayerPrefs.GetString("Slot1");
                string load;
                Load.slot2Load.TryGetValue(slot1,out load);
                SceneManager.LoadScene(load);
            }
        }
        if (PlayerPrefs.HasKey("ThisSlot"))
        {
            PlayerPrefs.DeleteKey("ThisSlot");
            PlayerPrefs.SetString("ThisSlot", "Slot1");
            Debug.Log("set this slot as" + PlayerPrefs.GetString("ThisSlot"));
        }
        else
        {
            PlayerPrefs.SetString("ThisSlot", "Slot1");

        }

    }
    // Start is called before the first frame update
    void Start()
    {
        Load.init();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
