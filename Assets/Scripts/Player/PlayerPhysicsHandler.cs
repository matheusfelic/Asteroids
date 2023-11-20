using UnityEngine;
public class PlayerPhysicsHandler
{
    public void ApplyMovement(Rigidbody2D rigidbody, float thrustSpeed, Player player)
    {
        if (rigidbody != null && player.thrusting)
        {
            rigidbody.AddForce(player.transform.up * thrustSpeed);
        }
    }

    public void ApplyRotation(Rigidbody2D rigidbody, float rotationSpeed, Player player)
    {
        if (rigidbody != null && player.turnDirection != 0)
        {
            rigidbody.AddTorque(player.turnDirection * rotationSpeed);
        }
    }
}