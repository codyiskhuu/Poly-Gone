using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Load : MonoBehaviour
{
    public static Dictionary<string, string> slot2Load = new Dictionary<string, string>();
    public static Dictionary<string, Vector2> slot2Position = new Dictionary<string, Vector2>();
    public static void init()
    {
        slot2Load.Clear();
        slot2Position.Clear();
        slot2Load.Add("level1", "Level 1");
        slot2Position.Add("level1", new Vector2());
        slot2Load.Add("bossroom", "Amanda-Dev");
        slot2Position.Add("bossroom", new Vector2());
        slot2Load.Add("winroom", "Testing Scene");
        slot2Position.Add("winroom", new Vector2());
        Debug.Log("set the initial value of slot2load");
    }
    public void LoadScene()
    {
        SceneManager.LoadScene(1);
        //Application.LoadLevel(whichscene);
        FindObjectOfType<AudioManager>().Stop("Menu");
    }
    
}
