using UnityEngine;
public interface IPlayerInputHandler
{
    void HandleInput(Player player, IPlayerShooter playerShooter);
    void ApplyMovement(Rigidbody2D rigidbody, float thrustSpeed, Player player);
    void ApplyRotation(Rigidbody2D rigidbody, float rotationSpeed, Player player);
}