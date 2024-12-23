using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(LineOfSight))]
public class LosGUI : Editor
{
    private void OnSceneGUI()
    {
        LineOfSight los = (LineOfSight)target;
        Handles.color = Color.white;
        Handles.DrawWireArc(los.transform.position, Vector3.up, Vector3.forward, 360, los.radius);

        Vector3 A1 = AngleDirection(los.transform.eulerAngles.y, -los.viewAngle/2, false);
        Vector3 A2 = AngleDirection(los.transform.eulerAngles.y, los.viewAngle/2, false);

        Handles.color = Color.red;
        Handles.DrawLine(los.transform.position, los.transform.position + A1 * los.radius);
        Handles.DrawLine(los.transform.position, los.transform.position + A2 * los.radius);

        Handles.color = Color.blue;
        foreach (GameObject visibleTarget in los.visibleEnemy)
        {
            Handles.DrawLine(los.transform.position, visibleTarget.transform.position);
        }
    }

    private Vector3 AngleDirection(float eulerY, float angleDeg, bool angleInGlobal)
    {
        if (!angleInGlobal)
        {
            angleDeg += eulerY;
        }

        return new Vector3(Mathf.Sin(angleDeg * Mathf.Deg2Rad), 0, Mathf.Cos(angleDeg * Mathf.Deg2Rad));
    }
}