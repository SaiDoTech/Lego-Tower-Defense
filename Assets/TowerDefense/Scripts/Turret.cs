using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour, ICanBePlaced
{
	public bool wasPlaced = false;

	public float Range = 14.0f;
	public float TurnSpeed = 10.0f;
	public float fireRate = 1.1f;
	private float fireCountdown = 0f;

	public GameObject PartToRotate;
	public string EnemyTag = "Enemy";

	private Transform target;

	public GameObject bullet;
	public Transform[] firePoint = new Transform[2];
	private int shooted = 0;

    private void Start()
	{
		InvokeRepeating("UpdateTarget", 0f, 0.5f);
	}

	private void Update()
	{
		if ((!GameState.IsGameOnPause) && (wasPlaced))
		{
			if (target != null)
			{
				LockOnTarget();

				if (fireCountdown <= 0)
				{
					Shoot();
					fireCountdown = 1f / fireRate;
				}

			}

			fireCountdown -= Time.deltaTime;
		}
	}

	private void Shoot()
	{
		GameObject newBullet = Instantiate(bullet, firePoint[shooted % firePoint.Length].position, firePoint[shooted % firePoint.Length].rotation);
		BulletScript bulletScript = newBullet.GetComponent<BulletScript>();

		if (bulletScript != null)
			bulletScript.SetTarget(target.transform);

		shooted++;
	}

	private void UpdateTarget()
	{
		GameObject[] enemies = GameObject.FindGameObjectsWithTag(EnemyTag);
		float shortestDistance = Mathf.Infinity;
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

	private void LockOnTarget()
	{
		Vector3 dir = target.position - transform.position;
		Quaternion lookRotation = Quaternion.LookRotation(dir);
		Vector3 rotation = Quaternion.Lerp(PartToRotate.transform.rotation, lookRotation, Time.deltaTime * TurnSpeed).eulerAngles;
		PartToRotate.transform.rotation = Quaternion.Euler(0f, rotation.y, 0f);
	}

    public bool CheckCell(CellObject cell, GameObject tool)
    {
		if (cell.Value == null)
		{
			cell.Value = tool;
			return true;
		}
		else
			return false;
    }

    public void PlaceTool()
    {
		wasPlaced = true;
    }
}