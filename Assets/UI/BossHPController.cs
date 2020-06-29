using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BossHPController : MonoBehaviour
{
    public GameObject youwin;
    public GameObject bar;
    public GameObject Boss;
    public int MaximumHealth;
    // Start is called before the first frame update
    void Start()
    {
        t = 5;
        youwin.SetActive(false);
        Load.init();
    }
    private float t = 5;
    // Update is called once per frame
    void Update()
    {
        if (Boss == null)
        {
            youwin.SetActive(true);
            t -= Time.deltaTime;
            Debug.Log(t);
            if (t <= 0)
            {

                Debug.Log("Do transporting!");
                string thisslot = PlayerPrefs.GetString("ThisSlot");
                Debug.Log("get thisslot:" + thisslot);
                PlayerPrefs.SetString(thisslot, "winroom");
                PlayerPrefs.SetInt(thisslot + "coin", coinManager.score);
                string slot = PlayerPrefs.GetString(thisslot);
                Debug.Log("get slot:" + slot);
                string load;
                Load.slot2Load.TryGetValue(slot, out load);
                Debug.Log("get load:" + load);
                SceneManager.LoadScene(load);
            }
            return;
        }
        bar.GetComponent<Image>().fillAmount = (float)Boss.GetComponent<Enemy>().health / (float)MaximumHealth;
        if (Boss.GetComponent<Enemy>().health == 0)
        {
            youwin.SetActive(true);
            t -= Time.deltaTime;
            Debug.Log(t);
            if (t <= 0)
            {

                Debug.Log("Do transporting!");
                string thisslot = PlayerPrefs.GetString("ThisSlot");
                Debug.Log("get thisslot:" + thisslot);
                PlayerPrefs.SetString(thisslot, "winroom");
                PlayerPrefs.SetInt(thisslot + "coin", coinManager.score);
                string slot = PlayerPrefs.GetString(thisslot);
                Debug.Log("get slot:" + slot);
                string load;
                Load.slot2Load.TryGetValue(slot, out load);
                Debug.Log("get load:" + load);
                SceneManager.LoadScene(load);
            }

        }
    }
}
