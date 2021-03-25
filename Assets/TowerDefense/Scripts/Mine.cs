using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mine : MonoBehaviour, ICanBePlaced
{
	public bool wasPlaced;
	public float Range = 3.2f;

	public string RoadTag = "Road";
	public string EnemyTag = "Enemy";

	private Transform target;

	void Start()
	{
		InvokeRepeating("UpdateTarget", 0f, 0.25f);
	}

	void Update()
	{
		if ((!GameState.IsGameOnPause) && (wasPlaced))
        {
			if (target != null)
            {
				Destroy(target.gameObject);
				Destroy(gameObject);
            }
        }
	}

	public bool CheckCell(CellObject cell, GameObject tool)
    {
        if (cell.Value.gameObject.CompareTag(RoadTag))
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

	private void UpdateTarget()
	{
		GameObject[] enemies = GameObject.FindGameObjectsWithTag(EnemyTag);
		float shortestDistance = Range;//Mathf.Infinity;
		GameObject nearestEnemy = null;
		foreach (GameObject enemy in enemies)
		{
			float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
			if (distanceToEnemy < shortestDistance)
			{
				shortestDistance = distanceToEnemy;
				nearestEnemy = enemy;
			}
		}

		if (nearestEnemy != null && shortestDistance <= Range)
		{
			target = nearestEnemy.transform;
		}
		else
		{
			target = null;
		}
	}
}
