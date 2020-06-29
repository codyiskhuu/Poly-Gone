using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Debug = UnityEngine.Debug;

public class coinManager : MonoBehaviour
{
    public static coinManager instance;
    public TextMeshProUGUI text;
    public static int score;
    
    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        string thisslot = PlayerPrefs.GetString("ThisSlot");
        score=PlayerPrefs.GetInt(thisslot + "coin");
    }

    public void ChangeScore(int coinValue)
    {
        score += coinValue;
        Debug.Log(score);
        text.text = "x" + score.ToString();
    }
}
