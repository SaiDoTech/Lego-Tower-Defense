using UnityEngine;

public class Cell : MonoBehaviour
{
    // Coordinates within the x grid
    public int X { get; set; }
    // Coordinates within the y grid
    public int Y { get; set; }
    // Value inside the grid
    public GameObject Value { get; set; }
    // Has the cage been visited?
    public bool WasVisited { get; set; }
    // Need rotate around Y
    public float NeedYRotation { get; set; }
}
