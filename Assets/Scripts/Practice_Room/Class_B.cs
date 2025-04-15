using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

/// <summary>
/// Script that controls particle spawn when the trigger is pressed on the controller
/// </summary>
/// <Author: Play2Make></Author>

public class Class_B : MonoBehaviour
{
    private const string WRONG_EXTINGUISHER_COUNT_KEY = "WrongExtinguisherCount";
    private bool hasRegisteredWrongUse = false;

    public AudioSource audioSource;
    public ExtinguisherController extinguisherController;
    GameManager gameManager;

    private ParticleSystem _particleSystem;
    public InputDeviceCharacteristics controllerCharacteristics;
    private InputDevice controller;
    private bool canAlert;
    private void Awake()
    {
        gameManager = FindFirstObjectByType<GameManager>();
        _particleSystem = GetComponent<ParticleSystem>();
    }

    private void Start()
    {
        GameObject foundObject = GameObject.FindWithTag("ExtinguisherSound");
        audioSource = foundObject.GetComponent<AudioSource>();
        canAlert = true;
        _particleSystem.Stop();
        GetController();
    }

    // Check on update if the trigger on control is pressed to spawn or stop spawning the particles.
    private void Update()
    {
         if (!controller.isValid)
        {
            GetController();
        }

        
        if (controller.TryGetFeatureValue(CommonUsages.triggerButton, out bool isTriggerPressed) && isTriggerPressed && extinguisherController.canSpray && extinguisherController.canUse)
        {
            SpawnWaterParticles();
        }
        else
        {
            StopWaterParticles();
        }
    }

    // Find the VR Controller reference.
    private void GetController()
    {
        
        var inputDevices = new List<InputDevice>();
        InputDevices.GetDevicesWithCharacteristics(controllerCharacteristics, inputDevices);

        if (inputDevices.Count > 0)
        {
            controller = inputDevices[0];  
        }
    }

    // Function to start the particle spawn of extinguishers
    public void SpawnWaterParticles()
    {
        if (_particleSystem != null && !_particleSystem.isPlaying)
        {
            audioSource.Play();
            _particleSystem.Play();
        }
    }

    // Function to stop the particle spawn of extinguishers
    public void StopWaterParticles()
    {
        if (_particleSystem != null && _particleSystem.isPlaying)
        {
            audioSource.Stop();
            _particleSystem.Stop();
            Debug.Log("Parou agua");
        }
    }

    // Function to detect the particle collisions to extinguish the fires particles. 
    private void OnParticleCollision(GameObject other)
    {
        
        if (other.CompareTag("classB") || other.CompareTag("classC"))
        {
            other.GetComponent<FireCounter>().AddFireCountered();
            other.GetComponent<ParticleSystem>().Stop();
            other.SetActive(false);
            other.transform.GetChild(2).gameObject.SetActive(true);
        }
        if (other.CompareTag("classA") || other.CompareTag("classK") || other.CompareTag("classD"))
        {
            if (other.transform.childCount > 0)
            {
                Transform firstChild = other.transform.GetChild(0);
                firstChild.gameObject.SetActive(true);
            }
            if (canAlert)
            {
                canAlert = false;
                gameManager.WrongExtinguisherNotification();
                RegisterWrongExtinguisherUse();
                Invoke("ReturnAlerts", 4f);
            }
        }
    }

    // Controls the canAlert bool, to avoid information spawm.
    private void ReturnAlerts()
    {
        canAlert = true;
    }

    public void RegisterWrongExtinguisherUse()
    {
        if (!hasRegisteredWrongUse)
        {
            int currentCount = PlayerPrefs.GetInt(WRONG_EXTINGUISHER_COUNT_KEY, 0);
            PlayerPrefs.SetInt(WRONG_EXTINGUISHER_COUNT_KEY, currentCount + 1);
            PlayerPrefs.Save();

            hasRegisteredWrongUse = true; // Evita somar mais de uma vez

            Debug.Log("Extintor errado usado! Total: " + (currentCount + 1));
        }
    }
}
