using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

/// <summary>
/// Controls all extinguishers behaviour.
/// </summary>
/// <Author: Play2Make></Author>
public class ExtinguisherController : MonoBehaviour
{
    public InputDeviceCharacteristics controllerCharacteristics;
    private InputDevice controller;
    public bool canSpray;
    public bool canUse;
    public bool typeC;

    public bool isResetting;
    public float resetSpeed = 10000f;

    public Transform resetPosition;
    public Transform grabber;
    private Vector3 startPosition;

    [Header("Collider Control")]
    public GameObject colliderToManipulate;

    void Start()
    {
        startPosition = transform.position;
        GetController();
        canSpray = false;
        isResetting = true;
    }

    private void Update()
    {
        if (!controller.isValid)
        {
            GetController();
        }
        if (isResetting)
        {
            grabber.position = Vector3.Lerp(grabber.position, resetPosition.position, resetSpeed * Time.deltaTime);
        }

        if (isResetting && typeC)
        {
            grabber.rotation = Quaternion.Lerp(grabber.rotation, resetPosition.rotation, resetSpeed * Time.deltaTime);
        }
    }

    // Turn off the reset hose's starting position.
    public void DontResetGrabPosition()
    {
        isResetting = false;
    }

    // Reset the hose's starting position.
    public void ResetGrabberPosition()
    {
        isResetting = true;
    }

    // Bool to turn the extinguisher able to use
    public void CanUse()
    {
        canUse = true;
    }

    // Makes it possible to use the fire extinguisher.
    public void CanSprayOn()
    {
        canSpray = true;
    }

    // Makes it possible to not use the fire extinguisher.
    public void CanSprayOff()
    {
        canSpray = false;
    }

    // Turn on the extinguisher collider
    public void ActivateCollider()
    {
        if (colliderToManipulate != null)
        {
            Collider collider = colliderToManipulate.GetComponent<Collider>();
            if (collider != null)
            {
                collider.enabled = true;
            }
            else
            {
                Debug.LogWarning("Nenhum Collider encontrado no GameObject alvo!");
            }
        }
        else
        {
            Debug.LogWarning("GameObject alvo não atribuído!");
        }
    }

    // Turn off the extinguisher collider.
    public void DeactivateCollider()
    {
        if (colliderToManipulate != null)
        {
            Collider collider = colliderToManipulate.GetComponent<Collider>();
            if (collider != null)
            {
                collider.enabled = false;
            }
            else
            {
                Debug.LogWarning("Nenhum Collider encontrado no GameObject alvo!");
            }
        }
        else
        {
            Debug.LogWarning("GameObject alvo não atribuído!");
        }
    }

    // Get the VR Controls reference.
    private void GetController()
    {
        var inputDevices = new List<InputDevice>();
        InputDevices.GetDevicesWithCharacteristics(controllerCharacteristics, inputDevices);

        if (inputDevices.Count > 0)
        {
            controller = inputDevices[0];
        }
    }
}
