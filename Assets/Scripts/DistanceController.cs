using UnityEngine;

/// <summary>
/// Check if the player is inside the fire and issue an alert for them to move away.
/// </summary>
/// <Author: Play2Make></Author>
/// 
public class DistanceController : MonoBehaviour
{
    GameManager gameManager;
    public Transform respawnPoint;

    private void Start()
    {
        gameManager = FindFirstObjectByType<GameManager>();
    }

    // If the player enters the area, it issues the alert.
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        { 
            gameManager.respawnPoint = respawnPoint;
            gameManager.CloseToTheFire();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            gameManager.PlayerMovedAway();
        }
    }
}
