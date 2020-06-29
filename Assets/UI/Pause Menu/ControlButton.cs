using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlButton : MonoBehaviour
{
    public GameObject hints;
    public GameObject controlbackb;
    public GameObject controlb;
    public GameObject resumeb;
    public GameObject mainb;
    public GameObject exitb;
    public void back()
    {
            controlbackb.SetActive(false);
            resumeb.SetActive(true);
            mainb.SetActive(true);
            exitb.SetActive(true);
            hints.SetActive(false);
        controlb.SetActive(true);
    }
    public void Show()
    { 
            controlbackb.SetActive(true);
            resumeb.SetActive(false);
            mainb.SetActive(false);
            exitb.SetActive(false);
            hints.SetActive(true);
        controlb.SetActive(false);
    }
    // Start is called before the first frame update
    void Start()
    {
        controlbackb.SetActive(false);
        resumeb.SetActive(true);
        mainb.SetActive(true);
        exitb.SetActive(true);
        hints.SetActive(false);
        controlb.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
