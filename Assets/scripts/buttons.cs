using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class buttons : MonoBehaviour
{
    // Start is called before the first frame update
    public string scene;
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void loadscene()
    {

            Debug.Log("work");
            SceneManager.LoadScene(scene);
        

    }
}
