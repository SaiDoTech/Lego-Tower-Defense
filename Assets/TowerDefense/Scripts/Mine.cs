using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mine : MonoBehaviour, ICanBePlaced
{
    public bool wasPlaced;
    public string RoadTag = "Road";

    public bool CheckCell(CellObject cell)
    {
        if (cell.Value.gameObject.tag == RoadTag)
        {
            return true;
        }
        else
            return false;
    }

    public void PlaceTool()
    {
        wasPlaced = true;
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
