using UnityEngine;

public class PlayerInputHandler : IPlayerInputHandler
{
    public void HandleInput(Player player, IPlayerShooter playerShooter)
    {
        // Example: Handle player movement input
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Example: Set turn direction based on horizontal input
        player.turnDirection = (horizontalInput < 0) ? 1f : (horizontalInput > 0) ? -1f : 0f;

        // Example: Set thrusting based on vertical input
        player.thrusting = verticalInput > 0;

        // Example: Check for shooting input
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            playerShooter.Shoot();
        }
    }

    public void ApplyMovement(Rigidbody2D rigidbody, float thrustSpeed, Player player)
    {
        // Example: Apply thrusting force if the player is thrusting
        if (rigidbody != null && player.thrusting)
        {
            rigidbody.AddForce(player.transform.up * thrustSpeed);
        }
    }

    public void ApplyRotation(Rigidbody2D rigidbody, float rotationSpeed, Player player)
    {
        // Example: Apply rotation torque if there's a turn direction
        if (rigidbody != null && player.turnDirection != 0)
        {
            rigidbody.AddTorque(player.turnDirection * rotationSpeed);
        }
    }
}
