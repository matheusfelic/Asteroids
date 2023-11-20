using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb;
    private IPlayerInputHandler inputHandler;
    private IPlayerShooter shooter;
    private IPlayerCollisionHandler collisionHandler;

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

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
        inputHandler = new PlayerInputHandler();
        collisionHandler = new PlayerCollisionHandler(this);
        shooter = new PlayerShooter(bulletPrefab, transform, collisionHandler);
    }

     private void OnEnable()
    {
        collisionHandler.TurnOffCollisions();
        isInvulnerable = true;
        invulnerabilityCoroutine = StartCoroutine(TurnOnCollisionsAfterDelay(invulnerabilityTime));
    }

    private IEnumerator TurnOnCollisionsAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        collisionHandler.TurnOnCollisions();
        isInvulnerable = false;
    }

    private void OnDisable()
    {
        if (invulnerabilityCoroutine != null)
        {
            StopCoroutine(invulnerabilityCoroutine);
        }
    }

    // Update is called once per frame
    private void Update()
    {
       inputHandler.HandleInput(this, shooter);
    }

    private void FixedUpdate() 
    {
        inputHandler.ApplyMovement(rb, thrustSpeed, this);
        inputHandler.ApplyRotation(rb, rotationSpeed, this);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        collisionHandler.HandleCollision(collision, rb);
    }

    public void SetBulletPrefab(Bullet bullet) {
        bulletPrefab = bullet;
    }
}
