using UnityEngine;
public class PlayerCollisionHandler : IPlayerCollisionHandler
{
    private readonly Player player;

    public PlayerCollisionHandler(Player player)
    {
        this.player = player;
    }

    public void TurnOffCollisions()
    {
        player.gameObject.layer = LayerMask.NameToLayer("IgnoreCollisions");
    }

    public void TurnOnCollisions()
    {
        player.gameObject.layer = LayerMask.NameToLayer("Player");
    }

    public void HandleCollision(Collision2D collision, Rigidbody2D rigidbody)
    {
        if (collision.gameObject.CompareTag("Asteroid"))
        {
            rigidbody.velocity = Vector3.zero;
            rigidbody.angularVelocity = 0f;

            GameManager.Instance.OnPlayerDeath(player);
        }
    }

    public bool AreCollisionsEnabled()
    {
        return player.gameObject.layer != LayerMask.NameToLayer("IgnoreCollisions");
    }
}