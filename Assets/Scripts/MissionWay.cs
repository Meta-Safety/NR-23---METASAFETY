using UnityEngine;

/// <summary>
/// Controls the missions arrows
/// </summary>
/// <Author: Play2Make></Author>

public class MissionWay : MonoBehaviour
{
    public GameObject arrowToTurnOff;
    public GameObject arrowToTurnOn;
    public GameObject[] objectsToTurnOff;
    public GameObject[] objectsToTurnOn;
    // Check the player collision to turn off the mission arrow.
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            arrowToTurnOff.SetActive(false);
            if (arrowToTurnOn != null)
            {
                arrowToTurnOn.SetActive(true);
            }

            if (objectsToTurnOff != null)
            {
                foreach (GameObject obj in objectsToTurnOff)
                {
                    obj.SetActive(false);
                }
            }
            if (objectsToTurnOn != null)
            {
                foreach (GameObject obj in objectsToTurnOn)
                {
                    obj.SetActive(true);
                }
            }

        }
    }
}
