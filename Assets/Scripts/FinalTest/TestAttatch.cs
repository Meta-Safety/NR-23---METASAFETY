using UnityEngine;

/// <summary>
/// Used to keep hose position at the right point.
/// </summary>
/// <Author: Play2Make></Author>
public class TestAttach : MonoBehaviour
{
    public Transform targetTransform;


    private void Update()
    {
        MoveToTarget();
    }

    // Keep the hose at the right point.
    public void MoveToTarget()
    {
        if (targetTransform != null)
        {
            transform.position = targetTransform.position;
            transform.rotation = targetTransform.rotation; 
        }
        else
        {
            Debug.LogWarning("Target Transform não está anexado.");
        }
    }
}