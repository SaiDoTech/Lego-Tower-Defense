﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolBuilder : MonoBehaviour
{
    public string CellTag = "Cell";

    private Camera mainCamera;
    private RaycastHit[] hits;
    private Ray ray;

    private static GameObject choosenCell;
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
                choosenCell = hits[i].transform.gameObject;
            }
        }

        if (flyingTool != null && choosenCell != null)
        {
            flyingTool.transform.position = choosenCell.transform.position + new Vector3(0, 0.2f, 0);
        }

        if ((flyingTool != null) && (Input.GetMouseButtonDown(0)))
        {
            // Check Can Be Placed?
            var intf = flyingTool.GetComponent<ICanBePlaced>();
            var cell = choosenCell.GetComponent<CellObject>();

            if (intf.CheckCell(cell, flyingTool))
            {
                intf.PlaceTool();

                Instantiate(flyingTool, cell.transform.position + new Vector3(0, 0.2f, 0), new Quaternion());
                Destroy(flyingTool);
            }

        }

        if ((flyingTool != null) && (Input.GetMouseButtonDown(1)))
        {
            Destroy(flyingTool);
        }
    }

    public void SetFlyingTool(GameObject tool)
    {
        if (tool != null)
        {
            flyingTool = Instantiate(tool);
        }
    }
}