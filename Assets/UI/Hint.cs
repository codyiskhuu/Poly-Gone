using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hint : MonoBehaviour
{
    public float duringTime;
    public GameObject hint;
    private bool isShowing=false;
    private float resetTime;
    public void showHint()
    {
        hint.SetActive(true);
        isShowing = true;

    }
    // Start is called before the first frame update
    void Start()
    {
        hint.SetActive(false);
        resetTime = duringTime;
        if (PlayerPrefs.HasKey("isNew"))
        {
            if (PlayerPrefs.GetInt("isNew") == 1)
            {
                showHint();
                PlayerPrefs.SetInt("isNew",0);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        duringTime -= Time.deltaTime;
        if(duringTime <= 0)
        {
            hint.SetActive(false);
            isShowing = false;
            duringTime = resetTime;
        }
    }
}
