using System.Collections.Generic;
using UnityEngine;

public class BezierCreator : MonoBehaviour
{
    [HideInInspector]
    public BezierCurve curve;

    public bool displayControlPoints = true;

    public Color anchorColour = Color.red;
    public Color controlColour = Color.white;
    public Color segementColour = Color.green;
    public Color selectedSegementColour = Color.magenta;

    [Space]

    [Range(0.01f, 0.1f)]
    public float anchorGizmoRadius = 0.1f;

    [Range(0.01f, 0.075f)]
    public float controlGizmoRadius = 0.05f;

    [Space]

    [Range(0.01f, 1f)]
    public float gridsnapInterval = 0.25f;

    [NonReorderable]
    public List<Vector2> points = new();

    public void CreateCurve()
    {
        curve = new BezierCurve(transform.position);
    }

    void Reset()
    {
        CreateCurve();
    }
}