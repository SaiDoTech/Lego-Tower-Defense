using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathConstructor : MonoBehaviour
{
    private MapCreator mapCreator;
    public const int TryFindCount = 20;

    private void Awake()
    {
        mapCreator = gameObject.GetComponent<MapCreator>();
    }

    public int GetPath(CellObject[,] cells)
    {
        List<CellObject> pathList = new List<CellObject>();
        int pathLength = 0;

        MakeStartPoint(cells, mapCreator.StartObject, pathList);
        pathLength++;

        pathLength += CreatePath(cells, pathList, TryFindCount);
        MakeEndPoint(cells, mapCreator.FinishObject, pathList);
        CorrectPath(cells, pathList);
        CorrectPoints(cells, pathList);

        return pathLength;
    }

    private void MakeStartPoint(CellObject[,] cells, GameObject startObject, List<CellObject> pathList)
    {
        System.Random rnd = new System.Random();
        int rowNum = rnd.Next(0, 2) * (cells.GetLength(0) - 1);
        int columnNum = rnd.Next(0, 2) * (cells.GetLength(1) - 1);

        CellObject start = cells[rowNum, columnNum];
        start.Value = startObject;
        start.WasVisited = true;

        pathList.Add(start);
    }

    private int CreatePath(CellObject[,] cells, List<CellObject> pathList, int tryFindCount)
    {
        System.Random rnd = new System.Random();
        CellObject currentCell = pathList[0];
        List<CellObject> neighbors = new List<CellObject>();
        int length = 0;

        for (var i = 0; i < tryFindCount; i++)
        {
            GetNeighbors(neighbors, currentCell, cells);
            if (neighbors.Count != 0)
            {
                var lastCell = currentCell;
                currentCell = neighbors[rnd.Next(0, neighbors.Count)];
                currentCell.WasVisited = true;
                currentCell.Value = mapCreator.RoadObject;
                cells[currentCell.RowNum, currentCell.ColumnNum] = currentCell;

                CellObject midCell = null;

                if (currentCell.ColumnNum != lastCell.ColumnNum)
                {
                    midCell = cells[currentCell.RowNum, (Mathf.Abs(currentCell.ColumnNum + lastCell.ColumnNum) / 2)];
                }
                else if (currentCell.RowNum != lastCell.RowNum)
                {
                    midCell = cells[(Mathf.Abs(currentCell.RowNum + lastCell.RowNum) / 2), currentCell.ColumnNum];
                }

                midCell.WasVisited = true;
                midCell.Value = mapCreator.RoadObject;

                pathList.Add(midCell);
                pathList.Add(currentCell);
                length += 2;
            }
            neighbors.Clear();
        }
        return length;
    }
    private void GetNeighbors(List<CellObject> neighbors, CellObject currentCell, CellObject[,] cells)
    {
        var up = (currentCell.RowNum - 2, currentCell.ColumnNum);
        var right = (currentCell.RowNum, currentCell.ColumnNum + 2);
        var down = (currentCell.RowNum + 2, currentCell.ColumnNum);
        var left = (currentCell.RowNum, currentCell.ColumnNum - 2);
        var arr = new (int, int)[] { up, right, down, left };

        foreach (var neighbor in arr)
        {
            if ((neighbor.Item1 > -1) && (neighbor.Item1 < cells.GetLength(0))
                && (neighbor.Item2 > -1) && (neighbor.Item2 < cells.GetLength(1)))
            {
                if (!cells[neighbor.Item1, neighbor.Item2].WasVisited)
                {
                    neighbors.Add(cells[neighbor.Item1, neighbor.Item2]);
                }
            }
        }
    }

    private void MakeEndPoint(CellObject[,] cells, GameObject finishObject, List<CellObject> pathList)
    {
        var end = pathList[pathList.Count - 1];
        end.WasVisited = true;
        end.Value = finishObject;
    }


    private void CorrectPath(CellObject[,] cells, List<CellObject> pathList)
    {
        for (int i = 0; i < pathList.Count - 2; i++)
        {
            if ((pathList[i + 2].RowNum != pathList[i].RowNum) && (pathList[i + 2].ColumnNum != pathList[i].ColumnNum))
            {
                cells[pathList[i + 1].RowNum, pathList[i + 1].ColumnNum].Value = mapCreator.TurnObject;
            }
        }

        for (int i = pathList.Count - 2; i > 0; i--)
        {
            if (pathList[i].Value == mapCreator.RoadObject)
            {
                if ((pathList[i - 1].ColumnNum != pathList[i].ColumnNum))
                {
                    cells[pathList[i].RowNum, pathList[i].ColumnNum].NeedYRotation = 90;
                }
                else
                {
                    cells[pathList[i].RowNum, pathList[i].ColumnNum].NeedYRotation = 0;
                }
            }
            else
            {
                // Turn
                if ((pathList[i].RowNum < pathList[i - 1].RowNum) || (pathList[i].RowNum < pathList[i + 1].RowNum))
                {
                    if ((pathList[i].ColumnNum > pathList[i + 1].ColumnNum) || (pathList[i].ColumnNum > pathList[i - 1].ColumnNum))
                    {
                        cells[pathList[i].RowNum, pathList[i].ColumnNum].NeedYRotation = 270;
                    }
                    else
                    {
                        cells[pathList[i].RowNum, pathList[i].ColumnNum].NeedYRotation = 180;
                    }
                }
                else
                {
                    if ((pathList[i].ColumnNum > pathList[i + 1].ColumnNum) || (pathList[i].ColumnNum > pathList[i - 1].ColumnNum))
                    {
                        cells[pathList[i].RowNum, pathList[i].ColumnNum].NeedYRotation = 0;
                    }
                    else
                    {
                        cells[pathList[i].RowNum, pathList[i].ColumnNum].NeedYRotation = 90;
                    }
                }
            }
        }
    }

    private void CorrectPoints(CellObject[,] cells, List<CellObject> pathList)
    {
        CellObject startPoint = pathList[0];
        CellObject endPoint = pathList[pathList.Count - 1];

        startPoint.NeedYRotation = ChooseStartOrEndDir(pathList[1], startPoint);
        endPoint.NeedYRotation = ChooseStartOrEndDir(pathList[pathList.Count - 2], endPoint);
    }

    private int ChooseStartOrEndDir(CellObject a, CellObject b)
    {
        if (a.ColumnNum == b.ColumnNum)
        {
            if (a.RowNum < b.RowNum)
            {
                return 180;
            }
            else
            {
                return 0;
            }
        }
        else
        {
            if (a.ColumnNum < b.ColumnNum)
            {
                return 90;
            }
            else
            {
                return 270;
            }
        }
    }
}
