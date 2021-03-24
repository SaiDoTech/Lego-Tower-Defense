using Unity.LEGO.Behaviours.Actions;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    private Transform target;

    public float speed = 55.0f;

    public void SetTarget(Transform target)
    {
        this.target = target;
    }

    private void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 dir = target.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;

        if (dir.magnitude <= distanceThisFrame)
        {
            HitTarget();
            return;
        }

        transform.Translate(dir.normalized * distanceThisFrame, Space.World);
    }

    private void HitTarget()
    {
        var ExplodeScript = target.gameObject.GetComponentInChildren<ExplodeAction>();
        ExplodeScript.enabled = true;

        Destroy(target.gameObject);
        Destroy(gameObject);
    }
}