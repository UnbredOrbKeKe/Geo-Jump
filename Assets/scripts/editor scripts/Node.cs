﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node {

    public int nodePosX;
    public int nodePosY;
    public GameObject vis;
    public MeshRenderer tileRenderer;
    public bool isWalkable;
    public LevelEditor.Level_Object placedObj;
    public List<LevelEditor.Level_Object> stackedObjs = new List<LevelEditor.Level_Object>();
    public LevelEditor.Level_WallObj wallObj;
    
}
