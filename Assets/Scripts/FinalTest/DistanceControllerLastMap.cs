using DG.Tweening.Core.Easing;
using UnityEngine;

/// <summary>
/// Used to check if the player gets too close to the fire.
/// </summary>
/// <Author: Play2Make></Author>
public class DistanceControllerLastMap : MonoBehaviour
{
    RespawnManager respawnManager;
    public Transform respawnPoint;
    private void Start()
    {
        respawnManager = FindFirstObjectByType<RespawnManager>();
    }

    // While the player is near the fire, issues an alert for them to move away.
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            respawnManager.respawnPoint = respawnPoint;
            respawnManager.CloseToTheFire();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            respawnManager.PlayerMovedAway();
        }
    }
}
