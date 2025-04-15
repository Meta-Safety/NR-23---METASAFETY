using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Start_Particles : MonoBehaviour
{
    ParticleSystem particleSystem;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnEnable()
    {
        particleSystem.Play();
    }
}
