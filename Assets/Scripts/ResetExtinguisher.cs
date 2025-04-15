using UnityEngine;

/// <summary>
/// Script that controls the extinguishers position, 
/// checking if the contact with the ground is equals 3 seconds to respawn on original position
/// </summary>
/// <Author: Play2Make></Author>
/// 
public class ResetOnFloorCollision : MonoBehaviour
{
    private Vector3 initialRotation; // Keep the original rotation
    private Vector3 initialPosition; // Keep the original position
    private float collisionTime = 0f; // Keep the collision time
    private bool isColliding = false; // Check the collision
    public float resetTime = 3f; // Time to reset the position

    void Start()
    {
        // Keep the positions and rotations when the game starts
        initialRotation = transform.eulerAngles; 
        initialPosition = transform.position;
    }

    void Update()
    {
        //Check if the extinguisher are colliding with the respawn collisor.
        if (isColliding)
        {
            collisionTime += Time.deltaTime;

            // If the collision is true, start the respawn timer to set the extinguisher to the original position
            if (collisionTime >= resetTime)
            {
                ResetPosition();
            }
        }
    }

    //Check when the extinguisher enter from the respawn collisor.
    private void OnCollisionEnter(Collision collision)
    {
        // Inicia a contagem se o objeto colidir com "Floor"
        if (collision.gameObject.CompareTag("Respawn"))
        {
            isColliding = true;
        }
    }

    //Check when the extinguisher exit from the respawn collisor.
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Respawn"))
        {
            isColliding = false;
            collisionTime = 0f;
        }
    }

    // Called to return the extinguisher to original position
    private void ResetPosition()
    {
        transform.eulerAngles = initialRotation;
        transform.position = initialPosition;
        collisionTime = 0f;
        isColliding = false;
    }
}
