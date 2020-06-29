using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Save : MonoBehaviour
{
    public void save(string scene,string slot,Vector2 location,int coinnum)
    {
        PlayerPrefs.SetString(slot, scene);
        string loc = v2string(location);
        PlayerPrefs.SetString(slot + "location", loc);
        PlayerPrefs.SetInt(slot + "coin", coinnum);
    }
    public string v2string(Vector2 vec)
    {
        string r = "";
        r += vec.x.ToString();
        r += "/";
        r += vec.y.ToString();
        return r;
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
