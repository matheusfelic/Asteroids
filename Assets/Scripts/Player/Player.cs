using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb;
    private PlayerInputHandler inputHandler;
    private PlayerProjectileLauncher shooter;
    private PlayerCollisionHandler collisionHandler;
    private PlayerPhysicsHandler physicsHandler;

    [SerializeField]
    private Bullet bulletPrefab;

    public float thrustSpeed = 1f;
    public bool thrusting;

    public float rotationSpeed = 1f;
    public float turnDirection;

    public float invulnerabilityTime = 3f;
    public float respawnDelay = 2f;
    public bool isInvulnerable;
    private Coroutine invulnerabilityCoroutine;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        inputHandler = new PlayerInputHandler();
        collisionHandler = new PlayerCollisionHandler(this);
        shooter = new PlayerProjectileLauncher(bulletPrefab, transform, collisionHandler);
        physicsHandler = new PlayerPhysicsHandler();
    }

    private void OnEnable()
    {
        collisionHandler.DisableCollisions();
        isInvulnerable = true;
        invulnerabilityCoroutine = StartCoroutine(TurnOnCollisionsAfterDelay(invulnerabilityTime));
    }

    private IEnumerator TurnOnCollisionsAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        collisionHandler.EnableCollisions();
        isInvulnerable = false;
    }

    private void OnDisable()
    {
        if (invulnerabilityCoroutine != null)
        {
            StopCoroutine(invulnerabilityCoroutine);
        }
    }

    private void Update()
    {
        InputData input = inputHandler.GetInput();
        turnDirection = (input.horizontalInput < 0) ? 1f : (input.horizontalInput > 0) ? -1f : 0f;
        thrusting = input.verticalInput > 0;

        if (input.isShootPressed)
        {
            shooter.Shoot();
        }
    }

    private void FixedUpdate()
    {
        physicsHandler.ApplyMovement(rb, thrustSpeed, this);
        physicsHandler.ApplyRotation(rb, rotationSpeed, this);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        collisionHandler.HandleCollision(collision, rb);
    }

    public void SetBulletPrefab(Bullet bullet)
    {
        bulletPrefab = bullet;
    }
}
