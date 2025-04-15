using UnityEngine;

/// <summary>
/// Used on practice room, to manage the animations on pine and seal explanation.
/// </summary>
/// <Author: Play2Make></Author>
public class SealAndPine : MonoBehaviour
{
    public ExtinguisherController extinguisherController;
    public GameObject seal;
    public GameObject pine;
    public GameObject infoPanel;
    public Animator animator;

    // Turn off the seal game object.
    private void DisableSeal()
    {
        seal.SetActive(false);
    }

    // Disable the pine game object and the enable the extinguisher to use.
    public void DisablePine()
    {
        animator.enabled = false;
        pine.SetActive(false);
        extinguisherController.canUse = true;

        if (infoPanel != null)
        {
            infoPanel.SetActive(false);
        }
    }

    // Start animation.
    public void PlayAnim()
    {
        if (animator != null)
        {
            animator.enabled = true;
            animator.SetBool("startAnimation", true);
        }
    }

    // Checks when the finger collision to start the animation.
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("finger"))
        {
            PlayAnim();
            Debug.Log("finger in");
        }
    }
}
