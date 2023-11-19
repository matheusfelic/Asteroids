using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public float speed = 500f;
    public float maxLifeTime = 10f;
    private Rigidbody2D _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    public void Project(Vector2 direction)
    {
        _rigidbody.AddForce(direction * speed);

        Destroy(gameObject, maxLifeTime);
    }

    //every time there's collision
    private void OnCollisionEnter2D(Collision2D collision) 
    {
        Destroy(gameObject);
    }
}
