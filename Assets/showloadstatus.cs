using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class showloadstatus : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public GameObject text;
    // Update is called once per frame
    void Update()
    {
        if (loadslot1.isNew)
        {
            text.GetComponent<Text>().text = "Create new game:";
        }
        else
        {
            text.GetComponent<Text>().text = "Select one to load:";
        }
    }
}
