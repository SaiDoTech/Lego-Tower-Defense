using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentConstructor : MonoBehaviour
{
    public const float FillCoeff = 0.3f;
    private MapCreator mapCreator;

    private void Awake()
    {
        mapCreator = gameObject.GetComponent<MapCreator>();
    }

    public void CreateEnvironment(GameObject[] objects, CellObject[,] cells, int pathLength)
    {
        int freeSpace = (int)(FillCoeff * ((cells.GetLength(0) * cells.GetLength(1)) - pathLength));
        int objectPlaced = 0;
        System.Random rnd = new System.Random();

        while (objectPlaced <= freeSpace)
        {
            CellObject cell = cells[rnd.Next(cells.GetLength(0)), rnd.Next(cells.GetLength(1))];

            if (cell.Value == null)
            {
                cell.Value = objects[rnd.Next(objects.GetLength(0))];
                cell.NeedYRotation = rnd.Next(360);
                cell.WasVisited = true;

                objectPlaced++;
            }
        }
    }
}
