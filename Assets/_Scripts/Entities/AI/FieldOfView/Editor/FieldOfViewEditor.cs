// using UnityEditor;
// using UnityEngine;

// [CustomEditor(typeof(FieldOfView))]
// public class FieldOfViewEditor : Editor
// {
//     private void OnSceneGUI()
//     {
//         FieldOfView fov = (FieldOfView)target;
//         Handles.color = Color.white;
//         Handles.DrawWireArc(fov.transform.position, Vector3.up, Vector3.forward, 360, fov.Radius.Value);

//         Vector3 viewAngle01 = DirectionFromAngle(fov.transform.eulerAngles.y, -fov.Angle / 2);
//         Vector3 viewAngle02 = DirectionFromAngle(fov.transform.eulerAngles.y, fov.Angle / 2);

//         Handles.color = Color.yellow;
//         Handles.DrawLine(fov.transform.position, fov.transform.position + viewAngle01 * fov.Radius.Value);
//         Handles.DrawLine(fov.transform.position, fov.transform.position + viewAngle02 * fov.Radius.Value);

//         if (fov.CanSeeEnemy)
//         {
//             Handles.color = Color.green;
//             Handles.DrawLine(fov.transform.position, fov.EnemySeeing.transform.position);
//         }
//     }

//     private Vector3 DirectionFromAngle(float eulerY, float angleInDegrees)
//     {
//         angleInDegrees += eulerY;

//         return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
//     }
// }
