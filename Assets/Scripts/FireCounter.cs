using UnityEngine;

/// <summary>
/// This scripts controls the fires on scenes, 
/// storing and updating the fireworks that need to be put out and counting 
/// this to finish the missions
/// </summary>
/// <Author: Play2Make></Author>
public class FireCounter : MonoBehaviour
{
    TaskManager taskManager;
    public GameObject fireController;
    public GameObject nextMissionWay;

    public GameObject distanceController;

    public AudioSource fireAudioSource;

    public int fireCountered;
    public int fires;

    public bool alreadyCountered;
    private void Awake()
    {
        alreadyCountered = false;
        taskManager = FindFirstObjectByType<TaskManager>();

    }

    private void Update()
    {
        FireCounteredComplete();
    }

    // Adds an extra fire to the count.
    public void AddFireCountered()
    {
        if (!alreadyCountered)
        {
            fireController.GetComponent<FireCounter>().fireCountered++;
            alreadyCountered = true;
        }
        
    }

    // Check how many fires has been countered to finish the mission.
    public void FireCounteredComplete()
    {
        if (fires == fireCountered)
        {
            if (distanceController != null)
            {
                distanceController.SetActive(false);
            }
            fireCountered = 0;
            if(nextMissionWay != null)
            {
                nextMissionWay.SetActive(true);
            }
            if (fireAudioSource != null)
            {
                fireAudioSource.volume = 0;
            }
            taskManager.CompleteCurrentMission();
        }
    }
}
