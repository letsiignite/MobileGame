using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineOfSight : MonoBehaviour
{
    public float radius = Mathf.Infinity;
    public float viewAngle = 110f;
    public LayerMask enemyLayer;
    public LayerMask obstructLayer;

    [SerializeField] private float offset = 1f;
    [SerializeField] private float meshResoultion;

    [HideInInspector] public List<GameObject> visibleEnemy = new List<GameObject>();

    private void Start()
    {
        StartCoroutine(findWithDelay());
    }

    private IEnumerator findWithDelay()
    {
        while (true)
        {
            findVisibleTargets();
            yield return new WaitForSeconds(0.2f);
        }
    }

    private void Update()
    {
        DrawFieldOfView();
    }

    void findVisibleTargets()
    {
        visibleEnemy.Clear();
        Collider[] rangeChecks = Physics.OverlapSphere(transform.position, radius, enemyLayer);
        if (rangeChecks.Length != 0)
        {
            for (int i = 0; i < rangeChecks.Length; i++)
            {
                GameObject target = rangeChecks[i].gameObject;
                Vector3 dir = (target.transform.position - transform.position).normalized;
                if (Vector3.Angle(transform.forward, dir) < viewAngle / 2)
                {
                    float distance = Vector3.Distance(transform.position, target.transform.position);

                    Vector3 origin = new Vector3(transform.position.x, transform.position.y + offset, transform.position.z);
                    if (!Physics.Raycast(origin, dir, distance, obstructLayer))
                    {
                        visibleEnemy.Add(target);
                    }
                }
            }
        }
    }

    private void DrawFieldOfView()
    {
        int stepCount = Mathf.RoundToInt(viewAngle * meshResoultion);
        float stepAngleSize = viewAngle / stepCount;

        for (int i = 0; i <= stepCount; i++)
        {
            float angle = transform.eulerAngles.y - viewAngle / 2 + stepAngleSize * i;
            Debug.DrawLine(transform.position, transform.position + DirFromAngle(angle, true) * radius, Color.red);
        }
    }

    public Vector3 DirFromAngle(float angle, bool global)
    {
        if (!global)
        {
            angle += transform.eulerAngles.y;
        }
        return new Vector3(Mathf.Sin(angle) * Mathf.Deg2Rad, 0, Mathf.Cos(angle) * Mathf.Deg2Rad);
    }
}