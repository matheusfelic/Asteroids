using UnityEngine;

public class Bullet : MonoBehaviour
{

    public float speed = 500f;
    public float maxLifeTime = 10f;
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void Project(Vector2 direction)
    {
        rb.AddForce(direction * speed);

        Destroy(gameObject, maxLifeTime);
    }

    //every time there's collision
    private void OnCollisionEnter2D(Collision2D collision) 
    {
        Destroy(gameObject);
    }
}
