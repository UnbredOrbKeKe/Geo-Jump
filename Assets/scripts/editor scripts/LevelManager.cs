using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LevelEditor;

public class LevelManager : MonoBehaviour
{
    Grid gridBase;
    public List<GameObject> inSceneGameObjects = new List<GameObject>();
    public List<GameObject> inSceneWalls = new List<GameObject>();
    public List<GameObject> inSceneStackObjects = new List<GameObject>();

    private static LevelManager instance = null;
    public static LevelManager GetInstance()
    {
        return instance;
    }

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        gridBase = Grid.GetInstance();  
    }

    void InitLevelObjects()
    {
        if(inSceneGameObjects.Count > 0)
        {

        }
    }
}
