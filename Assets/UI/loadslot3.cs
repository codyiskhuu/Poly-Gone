using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class loadslot3 : MonoBehaviour
{
    public void load3()
    {
        if (loadslot1.isNew)
        {
            PlayerPrefs.SetInt("isNew", 1);
            PlayerPrefs.SetString("Slot3", "level1");
            string slot3 = PlayerPrefs.GetString("Slot3");
            string load;
            Load.slot2Load.TryGetValue(slot3, out load);
            SceneManager.LoadScene(load);
        }
        else
        {
            if (PlayerPrefs.HasKey("Slot3"))
            {
                string slot3 = PlayerPrefs.GetString("Slot3");
                string load;
                Load.slot2Load.TryGetValue(slot3, out load);
                SceneManager.LoadScene(load);
            }
        }
        if (PlayerPrefs.HasKey("ThisSlot"))
        {
            PlayerPrefs.DeleteKey("ThisSlot");
            PlayerPrefs.SetString("ThisSlot", "Slot3");
            Debug.Log("set this slot as" + PlayerPrefs.GetString("ThisSlot"));
        }
        else
        {
            PlayerPrefs.SetString("ThisSlot", "Slot3");

        }

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
