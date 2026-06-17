using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(GrapplePoints))]
public class GrappleArc : Editor
{
    // private GrapplePoints grapplePoint;
    // private Vector3 predictedEndPosition;
    // private Vector3[] arcPoints;
    // private const int segments = 30;
    //
    // public override void OnInspectorGUI()
    // {
    //     grapplePoint = (GrapplePoints)target;
    //     
    //     DrawDefaultInspector();
    //     
    //     EditorGUILayout.Space();
    //     EditorGUILayout.LabelField("Arc Visualization", EditorStyles.boldLabel);
    //     
    //     if (GUILayout.Button("Calculate Arc"))
    //     {
    //         CalculateArc();
    //     }
    //     
    //     if (arcPoints != null && arcPoints.Length > 0)
    //     {
    //         EditorGUILayout.LabelField($"Arc segments: {arcPoints.Length}");
    //         
    //         if (GUILayout.Button("Draw in Scene"))
    //         {
    //             EditorUtility.SetDirty(grapplePoint);
    //         }
    //     }
    // }
    //
    // private void CalculateArc()
    // {
    //     if (grapplePoint.endPoint == null)
    //     {
    //         Debug.LogWarning("No end point assigned!");
    //         return;
    //     }
    //     
    //     try
    //     {
    //         Vector3 startPos = grapplePoint.transform.position;
    //         float gravity = Physics.gravity.y;
    //         Vector3 launchVector = grapplePoint.GrappleDirection(startPos, gravity);
    //         
    //         predictedEndPosition = grapplePoint.endPoint.position;
    //         arcPoints = new Vector3[segments + 1];
    //         
    //         float totalDistance = launchVector.magnitude;
    //         float totalTime = totalDistance / grapplePoint.speed;
    //         float step = totalTime / segments;
    //         
    //         for (int i = 0; i <= segments; i++)
    //         {
    //             float t = i * step;
    //             arcPoints[i] = startPos + launchVector * t + 0.5f * Physics.gravity * t * t;
    //         }
    //     }
    //     catch (System.Exception e)
    //     {
    //         Debug.LogError($"Arc calculation failed: {e.Message}");
    //         arcPoints = null;
    //     }
    // }
    //
    // private void OnSceneGUI()
    // {
    //     grapplePoint = (GrapplePoints)target;
    //     
    //     if (grapplePoint.endPoint == null)
    //         return;
    //     
    //     // Draw start point
    //     Handles.color = Color.green;
    //     Handles.DrawSolidDisc(grapplePoint.transform.position, Vector3.up, 0.2f);
    //     Handles.Label(grapplePoint.transform.position, "Start");
    //     
    //     // Draw end point
    //     Handles.color = Color.red;
    //     Handles.DrawSolidDisc(grapplePoint.endPoint.position, Vector3.up, 0.2f);
    //     Handles.Label(grapplePoint.endPoint.position, "End Point");
    //     
    //     // Draw connection line
    //     Handles.color = Color.yellow;
    //     Handles.DrawLine(grapplePoint.transform.position, grapplePoint.endPoint.position);
    //     
    //     // Draw arc if calculated
    //     if (arcPoints != null && arcPoints.Length > 1)
    //     {
    //         Handles.color = new Color(0, 1, 1, 0.8f);
    //         Handles.DrawAAPolyLine(3f, arcPoints);
    //         
    //         
    //         Vector3 gravity = Physics.gravity.y;
    //         try
    //         {
    //             Vector3 launchVec = grapplePoint.GrappleDirection(grapplePoint.transform.position, gravity);
    //             Handles.color = Color.cyan;
    //             Handles.ArrowHandleCap(0, grapplePoint.transform.position, Quaternion.LookRotation(launchVec), launchVec.magnitude * 0.1f, EventType.Repaint);
    //             Handles.Label(grapplePoint.transform.position + launchVec.normalized * 0.5f, "Launch Velocity");
    //         }
    //         catch
    //         {
    //             // Ignore calculation errors in scene view
    //         }
    //     }
    //     
    //     // Handle endPoint position handle
    //     EditorGUI.BeginChangeCheck();
    //     Vector3 newEndPos = Handles.PositionHandle(grapplePoint.endPoint.position, Quaternion.identity);
    //     if (EditorGUI.EndChangeCheck())
    //     {
    //         Undo.RecordObject(grapplePoint.endPoint, "Move End Point");
    //         grapplePoint.endPoint.position = newEndPos;
    //         EditorUtility.SetDirty(grapplePoint);
    //     }
    // }
}