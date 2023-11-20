using UnityEngine;
public interface IPlayerCollisionHandler
{
    void TurnOffCollisions();
    void TurnOnCollisions();
    void HandleCollision(Collision2D collision, Rigidbody2D rigidbody);
    bool AreCollisionsEnabled();
}