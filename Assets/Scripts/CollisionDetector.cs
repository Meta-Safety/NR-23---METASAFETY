using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class CollisionDetector : MonoBehaviour
{
    public XRGrabInteractable interactable;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("DropExtinguisher"))
        {
            interactable.enabled = false;
            StartCoroutine(ReenableInteractableAfterDelay(2f)); 
        }
    }

    private IEnumerator ReenableInteractableAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay); 
        interactable.enabled = true;           
    }
}
