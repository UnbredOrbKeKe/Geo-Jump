using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mainMenuManager : MonoBehaviour
{

    public GameObject button1;
    public GameObject button2;
    public GameObject button3;
    public GameObject button4;
    public GameObject button5;
    // Start is called before the first frame update
    void Start()
    {
        int i = 0;
        i = PlayerPrefs.GetInt("LvlUnlocker", i);

        if (i >= 1)
        {
            button1.SetActive(true);
        }
        if (i >= 2)
        {
            button2.SetActive(true);
        }
        if (i >= 3)
        {
            button3.SetActive(true);
        }
        if (i >= 4)
        {
            button4.SetActive(true);
        }
        if (i >= 5)
        {
            button5.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void PlayerprefReset()
    {
        PlayerPrefs.DeleteAll();
    }
}
