using UnityEngine;

/// <summary>
/// Controls npcs animations when talk.
/// </summary>
/// <Author: Play2Make></Author>
public class Npc_TalkController : MonoBehaviour
{
    public Animator animator;

    private int faceLayerIndex;

    private void OnEnable()
    {
        faceLayerIndex = animator.GetLayerIndex("Face Layer");
        StartTalk();
    }

    private void OnDisable()
    {
        StopTalk();
    }

    // Start the npc lips animation.
    public void StartTalk()
    {
        animator.SetLayerWeight(faceLayerIndex, 1f);
    }

    // Stops the npc lips animation.
    public void StopTalk()
    {
        animator.SetLayerWeight(faceLayerIndex, 0f);
    }
}
