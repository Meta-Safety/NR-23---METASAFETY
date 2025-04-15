using UnityEngine;
using UnityEngine.XR;

/// <summary>
/// Check the hand collision and the controller pinch value to start the pine off animation.
/// </summary>
/// <Author: Play2Make></Author>
public class PinchInteractor : MonoBehaviour
{
    public SealAndPine sealAndPine;

    public XRNode handNode = XRNode.LeftHand;

    private bool isPinching = false;
    private bool isColliding = false;  

    private void Update()
    {
        // Check if the hand is colliding to 
        if (!isColliding) return;

        InputDevice handDevice = InputDevices.GetDeviceAtXRNode(handNode);

        if (handDevice.isValid)
        {
            // If the colision is true and the pinch value is > 0.5, is able to start the PinchAnimation.
            if (handDevice.TryGetFeatureValue(CommonUsages.trigger, out float triggerValue))
            {
                bool pinchDetected = triggerValue > 0.5f;

               
                if (pinchDetected && !isPinching)
                {
                    // If the collision is true and the pinch value is > 0.5, start the animation.
                    isPinching = true;
                    OnPinchStart();
                }
                else if (!pinchDetected && isPinching)
                {
                    isPinching = false;
                    OnPinchEnd();
                    Debug.Log("PINCHOFF");
                }
            }
        }
    }

    void OnPinchStart()
    {
        Debug.Log("Pinch iniciado");
        sealAndPine.PlayAnim();
    }

    void OnPinchEnd()
    {
        Debug.Log("Pinch finalizado");
    }

    // On hand enter in collider, change the bool to true.
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("RightHand"))
        {
            isColliding = true;
            Debug.Log("RightHand em contato");
        }
    }

    // On hand exit from collider, change the bool to false.
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("RightHand"))
        {
            isColliding = false;
        }
    }
}
