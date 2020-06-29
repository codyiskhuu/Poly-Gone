using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class loadslot2 : MonoBehaviour
{
    public void load2()
    {

        if (loadslot1.isNew)
        {
            PlayerPrefs.SetInt("isNew", 1);
            PlayerPrefs.SetString("Slot2", "level1");
            string slot2 = PlayerPrefs.GetString("Slot2");
            string load;
            Load.slot2Load.TryGetValue(slot2, out load);
            SceneManager.LoadScene(load);
        }
        else
        {
            if (PlayerPrefs.HasKey("Slot2"))
            {
                string slot2 = PlayerPrefs.GetString("Slot2");
                string load;
                Load.slot2Load.TryGetValue(slot2, out load);
                SceneManager.LoadScene(load);
            }
        }
        if (PlayerPrefs.HasKey("ThisSlot"))
        {
            PlayerPrefs.DeleteKey("ThisSlot");
            PlayerPrefs.SetString("ThisSlot", "Slot2");
            Debug.Log("set this slot as" + PlayerPrefs.GetString("ThisSlot"));
        }
        else
        {
            PlayerPrefs.SetString("ThisSlot", "Slot2");

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
