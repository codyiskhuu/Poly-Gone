using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class teleport : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Do transporting!");
        string thisslot = PlayerPrefs.GetString("ThisSlot");
        Debug.Log("get thisslot:" + thisslot);
        PlayerPrefs.SetString(thisslot, "bossroom");
        PlayerPrefs.SetInt(thisslot + "coin", coinManager.score);
        string slot = PlayerPrefs.GetString(thisslot);
        Debug.Log("get slot:" + slot);
        string load;
        
        Load.slot2Load.TryGetValue(slot, out load);
        Debug.Log("get load:" + load);
        SceneManager.LoadScene(load);
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
