using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    public GameObject nodePrefab;
    public int sizeX;
    public int sizeY;
    public int offset = 1;

    public Node[,] grid;

    private static Grid instance = null;
    public static Grid GetInstance()
    {
        return instance;
    }
    void Awake()
    {
        //calls the create grid and createMousecollision function (void awake is used because i want to initialize other things on this instance aswell)
        instance = this;
        CreateGrid();
        CreateMouseCollision();
    }


    void CreateGrid()
    {
        //creating a node with size x & y
        grid = new Node[sizeX, sizeY];
        for (int x = 0; x < sizeX; x++)
        {
            for (int y = 0; y < sizeY; y++)
            {
                float posX = x * offset;
                float posY = y * offset;

                GameObject go = Instantiate(nodePrefab, new Vector3(posX, posY, 0), Quaternion.identity) as GameObject;
                go.transform.parent = transform.GetChild(0).transform;

                NodeObject nodeObj = go.GetComponent<NodeObject>();
                nodeObj.posX = x;
                nodeObj.posY = y;

                Node node = new Node();
                node.vis = go;
                node.tileRenderer = node.vis.GetComponent<MeshRenderer>();
                node.isWalkable = true;
                node.nodePosX = x;
                node.nodePosY = y;
                grid[x, y] = node;

            }
        }
    }

    void CreateMouseCollision()
    {
        GameObject go = new GameObject();
        go.AddComponent<BoxCollider>();
        go.GetComponent<BoxCollider>().size = new Vector3(sizeX * offset, 0.1f, sizeY * offset);
        go.transform.position = new Vector3((sizeX * offset) / 2 - 1, 0, (sizeY * offset) / 2 - 1);
    }
    public Node NodeFromWorldPosition(Vector3 worldPosition)
    {
        float worldX = worldPosition.x;
        float worldY = worldPosition.y;

        worldX /= offset;
        worldY /= offset;

        int x = Mathf.RoundToInt(worldX);
        int y = Mathf.RoundToInt(worldY);

        if (x > sizeX)
            x = sizeX;
        if (y > sizeY)
            y = sizeY;
        if (x < 0)
            x = 0;
        if (y < 0)
            y = 0;

        return grid[x, y];
    }
}