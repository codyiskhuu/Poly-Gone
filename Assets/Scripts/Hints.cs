using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class Hints : MonoBehaviour
{
    public List<Text> hints;
    public string compiletime;
    public Dictionary<Text, float[]> showing=new Dictionary<Text, float[]>();
    public Text getText(string name)
    {
        foreach (Text text in hints)
        {
            if (text.name == name) return text;
        }
        throw new ExitGUIException();
        return null;
    }
    public void setText(string name, Transform transform,  float during)
    {
        Text text = null;
        foreach(Text t in hints){
            if (t.name == name)
                text = t;
        }
        if (text == null) return;
        float[] time = { Time.realtimeSinceStartup, during };
        if (showing.ContainsKey(text))
        {
            showing.Remove(text);
        }
        showing.Add(text, time);
        text.canvas.transform.position = transform.position;
        text.enabled = true;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        foreach(KeyValuePair<Text,float[]> pair in showing)
        {
            if (Time.realtimeSinceStartup - pair.Value[0] >= pair.Value[1]) pair.Key.enabled = false;
        }
    }
}
