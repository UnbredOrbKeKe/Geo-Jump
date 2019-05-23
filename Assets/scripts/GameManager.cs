using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioClip pressStart;

    AudioSource audioSource;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = pressStart;
        audioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
