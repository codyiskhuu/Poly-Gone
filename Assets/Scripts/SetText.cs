using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetText : MonoBehaviour
{
    public string name;
    public Transform transform;
    public float during;
    public string s;
    public void ButtonOnClickEvent()
    {
        setText();
    }
    public void setText()
    {
        GameObject hints;
        hints = GameObject.Find("Hint");
        hints.GetComponent<Hints>().setText(name, transform, during);
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
