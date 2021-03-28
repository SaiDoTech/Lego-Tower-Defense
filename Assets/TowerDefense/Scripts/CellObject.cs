using UnityEngine;

public class CellObject : MonoBehaviour
{
    // Coordinates within the row in grid
    public int RowNum { get; set; }
    // Coordinates within the column in grid
    public int ColumnNum { get; set; }
    // Value inside the grid
    public GameObject Value;
    // Was this cell useed?
    public bool WasVisited { get; set; }
    // Need rotate around Y
    public float NeedYRotation { get; set; }
}
