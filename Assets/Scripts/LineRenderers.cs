using UnityEngine;

/// <summary>
/// Script that renders the hose of the extinguishers.
/// </summary>
/// <Author: Play2Make></Author>
public class LineRenderers : MonoBehaviour

{
    public Transform extinguisherPoint; // Extinguisher attach point.
    public Transform firstPoint;        // First attach point.
    public Transform secondPoint;      // Second attach point.
    public Transform thirdPoint;    // Third attach point.
    private LineRenderer lineRenderer;

    // Set the hose positions on start.
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = 4;
    }

    // Updates the hose position every frame.
    void Update()
    {
        if (lineRenderer != null)
        {
            lineRenderer.SetPosition(0, extinguisherPoint.position);
            lineRenderer.SetPosition(1, firstPoint.position);
            lineRenderer.SetPosition(2, secondPoint.position);
            lineRenderer.SetPosition(3, thirdPoint.position);
        }
    }
}

