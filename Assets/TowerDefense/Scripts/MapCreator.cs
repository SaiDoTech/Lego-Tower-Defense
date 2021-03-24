using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCreator : MonoBehaviour
{
    public const int MapHeight = 8;
    public const int MapWidth = 8;
    public const int MinPathLength = 16;

    // Map array
    private CellObject[,] map = new CellObject[MapHeight, MapWidth];
    // Path controll point
    public static List<Transform> PathPoints = new List<Transform>();

    [Header("Path Objects")]
    public PathConstructor pathConstructor;
    public GameObject TurnObject;
    public GameObject RoadObject;
    public GameObject StartObject;
    public GameObject FinishObject;
    public GameObject GroundObject;

    [Header("Environment Objects")]
    public EnvironmentConstructor environmentConstructor;
    public GameObject[] EnvironmentObjects;

    public GameObject CellObject;

    void Start()
    {
        CreateNewMap();
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

        // Create path 
        int currentLength = 0;
        while (currentLength < MinPathLength)
        {
            InitCells();
            currentLength = pathConstructor.GetPath(map);
        }

        // Create environment
        environmentConstructor.CreateEnvironment(EnvironmentObjects, map, currentLength);

        // Get path control points
        PathPoints = pathConstructor.GetPathPoints();

        DrawMap();
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

                var cell = temp.GetComponent<CellObject>();
                cell.RowNum = i;
                cell.ColumnNum = j;
                cell.WasVisited = false;
                cell.Value = null;
                cell.NeedYRotation = 0;

                map[i, j] = cell;
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
                    var temp = Instantiate(GroundObject, new Vector3(i*6.4f, 0, j*6.4f), Quaternion.Euler(0, map[i, j].NeedYRotation, 0));
                    temp.transform.SetParent(map[i, j].transform);
                }
                else
                {
                    var temp = Instantiate(map[i, j].Value, new Vector3(i * 6.4f, 0, j * 6.4f), Quaternion.Euler(0, map[i, j].NeedYRotation, 0));
                    temp.transform.SetParent(map[i, j].transform);
                }
            }
        }
    }
}
