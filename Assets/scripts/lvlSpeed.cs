﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lvlSpeed : MonoBehaviour
{
    public float speedUpgrade = 8f;
    void OnTriggerEnter2D(Collider2D collisotion)
    {
        Player.instance.speed = speedUpgrade;
    }
}
