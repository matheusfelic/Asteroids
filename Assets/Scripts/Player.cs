using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D rigidbody;

    [SerializeField]
    private Bullet bulletPrefab;

    public float thrustSpeed = 1f;
    private bool thrusting;

    public float rotationSpeed = 1f;
    private float turnDirection;

    public float invulnerabilityTime = 3f;
    public float respawnDelay = 2f;

    private void Awake() {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    private void OnEnable() 
    {
        TurnOffCollisions();
        Invoke(nameof(TurnOnCollisions), invulnerabilityTime);
    }

    // Update is called once per frame
    private void Update()
    {
        // checks for player movement
        thrusting = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow);

        if(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) {   
            turnDirection = 1f;
        } else if(Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) {
            turnDirection = -1f;
        } else {
            turnDirection = 0f;
        }

        //checking for shooting action
        if(Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)) {
            Shoot();
        }
    }

    private void FixedUpdate() 
    {
        if(thrusting) {
            rigidbody.AddForce(transform.up * thrustSpeed);
        }

        if(turnDirection != 0) {
            rigidbody.AddTorque(turnDirection * rotationSpeed);
        }
    }

    private void Shoot() {
        //creating a new bullet referencing the postion and rotation of the player
        Bullet bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
        bullet.Project(transform.up);
    }

    private void TurnOffCollisions() 
    {
        gameObject.layer = LayerMask.NameToLayer("IgnoreCollisions");
    }

    private void TurnOnCollisions()
   {
        gameObject.layer = LayerMask.NameToLayer("Player");
   }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Asteroid")
        {
            rigidbody.velocity = Vector3.zero;
            rigidbody.angularVelocity = 0f;

            GameManager.Instance.OnPlayerDeath(this);
        }
    }
}
