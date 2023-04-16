using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(BezierCreator))]
public class BezierEditor : Editor
{
    BezierCreator creator;
    BezierCurve Curve => creator.curve;

    const float SegmentSelectDistanceThreshold = 0.1f;
    int selectedSegmentIndex = -1;

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        EditorGUI.BeginChangeCheck();

        if (GUILayout.Button("Update curve"))
        {
            Undo.RecordObject(creator, "Update curve");
            UpdateCurve();
        }

        if (GUILayout.Button("Reset curve"))
        {
            Undo.RecordObject(creator, "Reset curve");

            creator.CreateCurve();
            ResetPointList();
        }

        if (EditorGUI.EndChangeCheck())
        {
            SceneView.RepaintAll();
        }
    }

    void OnSceneGUI()
    {
        //get current GUI event
        Event guiEvent = Event.current;

        GetMouseInput(guiEvent);
        DrawGizmos(guiEvent);
    }

    void GetMouseInput(Event guiEvent)
    {
        //convert mouse position to world position
        Vector2 mousePos = HandleUtility.GUIPointToWorldRay(guiEvent.mousePosition).origin;

        if (guiEvent.type == EventType.MouseDown)
        {
            //if Shift + LMB is pressed, add segment at mouse position
            if (guiEvent.button == 0 && guiEvent.shift)
            {
                //if mouse position is close enough to a point on the existing curve, split curve instead
                if (selectedSegmentIndex != -1)
                {
                    Undo.RecordObject(creator, "Split segment");
                    Curve.SplitSegment(mousePos, selectedSegmentIndex);

                    for (int i = 0; i < 3; i++)
                    {
                        creator.points.Insert(selectedSegmentIndex, mousePos);
                    }

                    UpdateAnchorPointList();
                }
                else
                {
                    Undo.RecordObject(creator, "Add segment");
                    Curve.AddSegment(mousePos);

                    for (int i = 0; i < 3; i++)
                    {
                        creator.points.Add(mousePos);
                    }

                    UpdateAnchorPointList();
                }
            }

            //if RMB is pressed, remove closest anchor point
            if (guiEvent.button == 1)
            {
                float closestAnchorDistance = creator.anchorGizmoRadius * 0.5f;
                int closestAnchorIndex = -1;

                for (int i = 0; i < Curve.NumPoints; i += 3)
                {
                    float dist = Vector2.Distance(mousePos, Curve[i]);
                    if (dist < closestAnchorDistance)
                    {
                        closestAnchorDistance = dist;
                        closestAnchorIndex = i;
                    }
                }

                if (closestAnchorIndex != -1)
                {
                    Undo.RecordObject(creator, "Delete segment");

                    if (Curve.NumSegments > 1)
                    {
                        creator.points.RemoveRange(closestAnchorIndex / 3 + 1, 3);
                    }

                    Curve.RemoveSegment(closestAnchorIndex);

                    UpdateAnchorPointList();
                }
            }
        }

        //if mouse has moved, update closest segment index
        if (guiEvent.type == EventType.MouseMove)
        {
            float closestSegmentDistance = SegmentSelectDistanceThreshold;
            int newSelectedSegmentIndex = -1;

            for (int i = 0; i < Curve.NumSegments; i++)
            {
                Vector2[] points = Curve.GetPointsInSegment(i);
                float dist = HandleUtility.DistancePointBezier(mousePos, points[0], points[3], points[1], points[2]);

                if (dist < closestSegmentDistance)
                {
                    closestSegmentDistance = dist;
                    newSelectedSegmentIndex = i;
                }
            }

            //update selected segment index
            if (newSelectedSegmentIndex != selectedSegmentIndex)
            {
                selectedSegmentIndex = newSelectedSegmentIndex;
                HandleUtility.Repaint();
            }
        }

        HandleUtility.AddDefaultControl(0);
    }

    void DrawGizmos(Event guiEvent)
    {
        Handles.color = Color.black;

        //draw lines
        for (int i = 0; i < Curve.NumSegments; i++)
        {
            Vector2[] points = Curve.GetPointsInSegment(i);

            //draw straight line from control points to respective anchor point
            if (creator.displayControlPoints)
            {
                Handles.DrawLine(points[1], points[0]);
                Handles.DrawLine(points[2], points[3]);
            }

            //draw bezier curve across segment
            Color segmentColour = (i == selectedSegmentIndex && Event.current.shift) ? creator.selectedSegementColour : creator.segementColour;
            Handles.DrawBezier(points[0], points[3], points[1], points[2], segmentColour, null, 2f);
        }

        //draw anchor and control point handles
        for (int i = 0; i < Curve.NumPoints; i++)
        {
            bool isAnchorPoint = i % 3 == 0;
            if (!isAnchorPoint && !creator.displayControlPoints) continue;

            Handles.color = isAnchorPoint ? creator.anchorColour : creator.controlColour;
            float handleGizmoRadius = isAnchorPoint ? creator.anchorGizmoRadius : creator.controlGizmoRadius;

            Vector2 newPos = Handles.FreeMoveHandle(Curve[i], Quaternion.identity, handleGizmoRadius, guiEvent.control ? creator.gridsnapInterval : Vector3.zero, Handles.SphereHandleCap);

            //if anchor has updated position
            if (Curve[i] != newPos)
            {
                Undo.RecordObject(creator, isAnchorPoint ? "Move anchor point" : "Move control point");

                Curve.MovePoint(i, newPos);
                UpdateAnchorPointList();
            }
        }
    }

    //add or remove elements to point list until its count equals (segment count + 1)
    void ResetPointList()
    {
        if (creator.points.Count > Curve.NumPoints)
        {
            int difference = creator.points.Count - Curve.NumPoints;
            creator.points.RemoveRange(creator.points.Count - difference, difference);
        }
        else if (creator.points.Count < Curve.NumPoints)
        {
            int difference = Curve.NumPoints - creator.points.Count;

            for (int i = 0; i < difference; i++)
            {
                creator.points.Add(Vector2.zero);
            }
        }

        UpdateAnchorPointList();
    }

    //set curve point positions based on BezierCreator.points list
    void UpdateCurve()
    {
        for (int i = 0; i < creator.points.Count; i++)
        {
            Curve[i] = creator.points[i];
        }
    }

    //set BezierCreator.points list based on curve point positions
    void UpdateAnchorPointList()
    {
        for (int i = 0; i < Curve.NumPoints; i++)
        {
            creator.points[i] = Curve[i];
        }
    }

    void OnEnable()
    {
        creator = target as BezierCreator;

        if (creator.curve == null)
        {
            creator.CreateCurve();
        }
    }
}