using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [SerializeField]
    private Sprite[] sprites;

    public float size;
    public static float minSize = 0.35f;
    public static float maxSize = 1.7f;
    public float speed = 50f;
    public float maxLifeTime = 30f;
    private SpriteRenderer _spriteRenderer;
    private Rigidbody2D _rigidbody;
    private AsteroidFactory asteroidFactory;

    public void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    public void Initialize(float size, Vector2 trajectory)
    {
        asteroidFactory = GameManager.Instance.GetAsteroidFactory();
        _spriteRenderer.sprite = sprites[Random.Range(0, sprites.Length)];

        transform.eulerAngles = new Vector3(0f, 0f, Random.value * 360f);

        transform.localScale = Vector3.one * size;
        _rigidbody.mass = size;

        Destroy(gameObject, maxLifeTime);
        SetTrajectory(trajectory);
    }

    public void SetTrajectory(Vector2 direction)
    {
        _rigidbody.AddForce(direction * speed);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            if ((size / 2) >= minSize)
            {
                SplitAsteriod(2);
            }
            GameManager.Instance.OnAsteroidDestroyed(this);
            Destroy(gameObject);
        }
    }

    private void SplitAsteriod(int parts)
    {
        for (int i = 0; i < parts; i++)
        {
            Vector2 position = transform.position;
            asteroidFactory.CreateAsteroid(position, transform.rotation, size / parts, Random.insideUnitCircle.normalized);
        }
    }

}
