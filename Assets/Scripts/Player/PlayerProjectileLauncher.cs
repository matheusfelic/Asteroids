using UnityEngine;
public class PlayerProjectileLauncher
{
    private readonly Bullet bulletPrefab;
    private readonly Transform playerTransform;
    private readonly PlayerCollisionHandler collisionHandler;

    public PlayerProjectileLauncher(Bullet bulletPrefab, Transform playerTransform, PlayerCollisionHandler collisionHandler)
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
            bullet.Launch(playerTransform.up);
        }
    }
}