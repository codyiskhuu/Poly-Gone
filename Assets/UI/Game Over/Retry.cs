using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Retry : MonoBehaviour
{
    public void retry()
    {
        if (PlayerPrefs.HasKey("ThisSlot"))
        {
            Debug.Log("now this slot is"+PlayerPrefs.GetString("ThisSlot"));
            string load;
            Load.init();
            Load.slot2Load.TryGetValue(PlayerPrefs.GetString(PlayerPrefs.GetString("ThisSlot")), out load);
            Debug.Log("now try to load " + load);
            SceneManager.LoadScene(load);
        }
        Time.timeScale = 1;
        GameOver.isOver = false;
        Debug.Log("Start Again");
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
