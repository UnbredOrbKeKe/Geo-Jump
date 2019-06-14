using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    public int lvlunlocker;
    public string scene;
    // Start is called before the first frame update
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Debug.Log(lvlunlocker);
            PlayerPrefs.SetInt("LvlUnlocker", lvlunlocker);
            PlayerPrefs.Save();

            int shit = 0;
            Debug.Log("is it 1?: " + PlayerPrefs.GetInt("LvlUnlocker", shit));
            SceneManager.LoadScene(scene);
        }
    }
}
