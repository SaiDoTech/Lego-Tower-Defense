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

    public GameObject CellObject;

    void Start()
    {
        InitCells();
        //DrawMap();
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
        for (int i=0; i < map.GetLength(0); i++)
        {
            for (int j=0; j < map.GetLength(1); j++)
            {
                var temp = Instantiate(CellObject, new Vector3(i*6.4f, 0, j*6.4f), Quaternion.identity);
                temp.transform.SetParent(gameObject.transform);
                temp.name = $"Cell: {i}x{j}";

                var cell = temp.GetComponent<Cell>();
                cell.RowNum = i;
                cell.ColumnNum = j;
                cell.WasVisited = false;
                cell.Value = null;
                cell.NeedYRotation = 0;
            }
        }
    }

    private void DestroyCells()
    {
        foreach (var cell in map)
        {
            Destroy(cell.gameObject);
        }
    }

    private void DrawMap()
    {
        for (int i = 0; i < map.GetLength(0); i++)
        {
            for (int j = 0; j < map.GetLength(1); j++)
            {
                if (map[i, j].Value == null)
                {

                }
            }
        }
    }
}
