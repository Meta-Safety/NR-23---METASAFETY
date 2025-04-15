using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Checks the control trigger to spawn particles.
/// </summary>
/// <Author: Play2Make></Author>
public class ParticleTrigger : MonoBehaviour
{
    public ParticleSystem _particleSystem;    
    public InputActionProperty triggerAction; 

    private void Update()
    {
        // If the input is pressed, start the particle spawn.
        if (triggerAction.action.ReadValue<float>() > 0.1f)
        {
            if (!_particleSystem.isPlaying)
            {
                _particleSystem.Play();  
            }
        }
        else
        {
            if (_particleSystem.isPlaying)
            {
                _particleSystem.Stop(); 
            }
        }
    }
}
