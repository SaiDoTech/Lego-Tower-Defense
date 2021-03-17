using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCreator : MonoBehaviour
{
    public const int MapHeight = 10;
    public const int MapWidth = 10;

    private Cell[,] map = new Cell[MapHeight, MapWidth];


    [Header("Path Objects")]
    public GameObject TurnObject;
    public GameObject RoadObject;
    public GameObject StartObject;
    public GameObject FinishObject;
    public GameObject GroundObject;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CreateNewMap()
    {
        // If map was generated earlier - destroy it.
        if (gameObject.transform.childCount != 0)
        {
            DestroyCells();
        }

        InitCells();
    }

    private void InitCells()
    {
        for (int i=0; i < MapHeight; i++)
        {
            for (int j=0; j < MapWidth; j++)
            {
                map[i, j].X = j;
                map[i, j].Y = i;
                map[i, j].Value = null;
                map[i, j].WasVisited = false;
                map[i, j].NeedYRotation = 0;
            }
        }
    }

    private void DestroyCells()
    {
        foreach (var cell in map)
        {
            Destroy(cell);
        }
    }
}
