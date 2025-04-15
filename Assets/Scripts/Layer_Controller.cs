using UnityEngine;

/// <summary>
/// Manages the npcs animator layers. 
/// </summary>
/// <Author: Play2Make></Author>
public class Layer_Controller : MonoBehaviour
{
    public Animator animator; 

    private int faceLayerIndex;

    private void Start()
    {        
        faceLayerIndex = animator.GetLayerIndex("Face Layer");
    }

    // Set the layer to One.
    public void SetFaceLayerWeightToOne()
    {
        if (faceLayerIndex != -1)
        {
            animator.SetLayerWeight(faceLayerIndex, 1f);
        }
    }

    // Set the layer to Zero.
    public void SetFaceLayerWeightToZero()
    {
        if (faceLayerIndex != -1)
        {
            animator.SetLayerWeight(faceLayerIndex, 0f);
        }
    }
}
