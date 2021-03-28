using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolBuilder : MonoBehaviour
{
    public string CellTag = "Cell";

    private Camera mainCamera;
    private RaycastHit[] hits;
    private Ray ray;

    private static GameObject choosedCell;
    private static GameObject choosedTool;
    private static GameObject flyingTool;

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
            }
        }

        if (flyingTool != null && choosedCell != null)
        {
            flyingTool.transform.position = choosedCell.transform.position + new Vector3(0, 0.2f, 0);
        }

        if ((flyingTool != null) && (Input.GetMouseButtonDown(0)))
        {
            // Check Can Be Placed?
            var intf = choosedTool.GetComponent<ICanBePlaced>();

            if (choosedCell != null)
            {
                var cell = choosedCell.GetComponent<CellObject>();

                if (intf.CheckCell(cell, choosedTool))
                {
                    intf.PlaceTool();
                    Instantiate(choosedTool, cell.transform.position + new Vector3(0, 0.2f, 0), new Quaternion());
                    Debug.Log("Placed");

                    Destroy(flyingTool);
                    choosedTool = null;
                }
            }
        }

        if ((flyingTool != null) && (Input.GetMouseButtonDown(1)))
        {
            Destroy(flyingTool);
            choosedTool = null;
        }
    }

    public void SetFlyingTool(GameObject tool)
    {
        choosedCell = null;
        choosedTool = null;
        Destroy(flyingTool);
        flyingTool = null;

        if (tool != null)
        {
            choosedTool = tool;
            flyingTool = Instantiate(tool);
        }
    }
}
