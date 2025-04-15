using UnityEngine;

/// <summary>
/// Used for start and stop the NPCs extinguisher particles.
/// </summary>
/// <Author: Play2Make></Author>
public class Npc_Extinguisher : MonoBehaviour
{
    private ParticleSystem particleSystem;

    private void Start()
    {
        particleSystem = GetComponent<ParticleSystem>();
    }

    // Start the particles emission.
    public void StartParticles()
    {
        particleSystem.Play();
    }

    // Stop the particles emission.
    public void StopParticles()
    {
        particleSystem.Stop();
    }
    
}
