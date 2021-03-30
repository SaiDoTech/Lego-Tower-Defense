using UnityEngine;

public class ToolBuilder : MonoBehaviour
{
    public string CellTag = "Cell";

    private Camera mainCamera;
    private RaycastHit[] hits;
    private Ray ray;

    private static GameObject choosedCell;
    private static GameObject flyingTool;
    private static ICanBePlaced canBePlaced;

    void Start()
    {
        ray = new Ray();
        mainCamera = Camera.main;
    }

    private void Update()
    {
        // Transform mouse position to ray
        ray = mainCamera.ScreenPointToRay(Input.mousePosition);

        hits = Physics.RaycastAll(ray);
        for (int i = 0; i < hits.Length; i++)
        {
            if (hits[i].transform.gameObject.CompareTag(CellTag))
            {
                choosedCell = hits[i].transform.gameObject;

                if (flyingTool != null)
                {
                    flyingTool.transform.position = choosedCell.transform.position + new Vector3(0, 0.2f, 0);
                }
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (choosedCell != null)
            {
                var cell = choosedCell.GetComponent<CellObject>();

                if ((canBePlaced != null) && (flyingTool != null))
                {
                    if (canBePlaced.CheckCell(cell, flyingTool))
                    {
                        canBePlaced.PlaceTool();
                        Debug.Log("Placed");

                        flyingTool = null;
                        canBePlaced = null;

                        GameState.IsBuildModActive = false;
                    }
                }
            }
        }

        if ((flyingTool != null) && (Input.GetMouseButtonDown(1)))
        {
            Destroy(flyingTool);
            flyingTool = null;
            canBePlaced = null;

            GameState.IsBuildModActive = false;
        }
    }

    public void SetFlyingTool(GameObject tool)
    {
        Destroy(flyingTool);
        flyingTool = null;
        canBePlaced = null;

        if (tool != null)
        {
            flyingTool = Instantiate(tool);
            canBePlaced = flyingTool.GetComponent<ICanBePlaced>();
        }
    }
}
