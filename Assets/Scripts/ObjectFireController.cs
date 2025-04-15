using UnityEngine;

/// <summary>
/// Used to control the fires that the player needs to put out and then 
/// activates another area on fire randomly
/// </summary>
/// <Author: Play2Make></Author>
public class ObjectFireController : MonoBehaviour
{
    RespawnManager respawnManager;
    public GameObject fireController;

    public GameObject distanceController;

    public AudioSource fireAudioSource;

    public int fireCountered;
    public int fires;

    private void Awake()
    {
        respawnManager = FindFirstObjectByType<RespawnManager>();

    }

    // Check fires countered every frame.
    private void Update()
    {
        FireCounteredComplete();
    }

    // Includes one more fire countered on collision.
    public void AddFireCountered()
    {
        fireController.GetComponent<ObjectFireController>().fireCountered++;
    }

    // Set how many fires need be countered.
    public void AddFireToCounter()
    {
        fireController.GetComponent<ObjectFireController>().fires++;
    }

    // When the fire countered is equals fires, active other random fire object.
    public void FireCounteredComplete()
    {
        if (fires == fireCountered)
        {
            if (fireAudioSource != null)
            {
                fireAudioSource.volume = 0;
            }
            if (distanceController != null)
            {
                distanceController.SetActive(false);
            }
            fireCountered = 0;
            respawnManager.objectivesDone++;
            respawnManager.ActivateRandomFireObject();
        }
    }
}
