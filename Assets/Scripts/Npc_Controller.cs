using UnityEngine;

/// <summary>
/// Script to controls the NPC animations on practice room, before the game begins.
/// </summary>
/// <Author: Play2Make></Author>
public class Npc_Controller : MonoBehaviour
{
    public ParticleSystem particleSystem;

    public Animator fireFighterExtinguisher;
    public Animator presenter;

    // When the start button is pressed, start the pine animation
    // and wait 15 seconds to start the exemple to use the extinguisher.
    private void OnEnable()
    {
        PullPine();
        Invoke("FireFighterOn", 15f);
    }

    // Start the pine explanation animation.
    public void PullPine()
    {
        presenter.SetBool("pineExplanation", true);
    }

    // Start the fire fighter animation and the particle spawn.
    public void FireFighterOn()
    {
        fireFighterExtinguisher.SetBool("fireFighterOn", true);
        particleSystem.Play();
    }

}
