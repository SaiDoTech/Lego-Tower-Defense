using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float MoveSpeed = 7.0f;
    public int HealthPoints = 3;

    private Transform target;
    private Vector3 dir;
    private Vector3 realTarget;
    private int pointIndx = 1;

    private void Start()
    {
        transform.position = MapCreator.PathPoints[0].position + new Vector3(0, 1, 0);

        target = MapCreator.PathPoints[pointIndx];
        realTarget = target.position + new Vector3(0, 1, 0);
        dir = realTarget - gameObject.transform.position;
    }

    private void Update()
    {
        if (!GameState.IsGameOnPause)
        {
            transform.Translate(dir.normalized * MoveSpeed * Time.deltaTime, Space.World);
            transform.LookAt(realTarget);

            if (Vector3.Distance(transform.position, realTarget) <= 0.5f)
            {
                GetNextPathPoint();
            }
        }
    }

    private void GetNextPathPoint()
    {
        pointIndx++;
        if (pointIndx < MapCreator.PathPoints.Count)
        {
            target = MapCreator.PathPoints[pointIndx];
            realTarget = target.position + new Vector3(0, 1, 0);
            dir = realTarget - gameObject.transform.position;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
