﻿using UnityEngine;
using System.Collections;

public class backgroundScrolling : MonoBehaviour
{
    public float scrollSpeed;
    public float tileSizeX;

    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        float newPosition = Mathf.PingPong(Time.time * scrollSpeed, tileSizeX);
        transform.position = startPosition + Vector3.left * newPosition;
    }
}