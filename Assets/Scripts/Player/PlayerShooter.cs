using UnityEngine;
public class PlayerShooter : IPlayerShooter
{
    private readonly Bullet bulletPrefab;
    private readonly Transform playerTransform;
    private readonly IPlayerCollisionHandler collisionHandler;

    public PlayerShooter(Bullet bulletPrefab, Transform playerTransform, IPlayerCollisionHandler collisionHandler)
    {
        this.bulletPrefab = bulletPrefab;
        this.playerTransform = playerTransform;
        this.collisionHandler = collisionHandler;
    }

    public void Shoot()
    {
        if (collisionHandler.AreCollisionsEnabled())
        {
            Bullet bullet = Object.Instantiate(bulletPrefab, playerTransform.position, playerTransform.rotation);
            bullet.Project(playerTransform.up);
        }
    }
}