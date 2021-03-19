using UnityEngine;

public class Cell : MonoBehaviour
{
    // Coordinates within the row in grid
    public int RowNum { get; set; }
    // Coordinates within the column in grid
    public int ColumnNum { get; set; }
    // Value inside the grid
    public GameObject Value { get; set; }
    // Has the cage been visited?
    public bool WasVisited { get; set; }
    // Need rotate around Y
    public float NeedYRotation { get; set; }
}
