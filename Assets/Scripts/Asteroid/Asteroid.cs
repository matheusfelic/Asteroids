using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [SerializeField]
    private Sprite[] sprites;

    public float size = 1f;
    public static float minSize = 0.35f;
    public static float maxSize = 1.65f;
    public float speed = 50f;
    public float maxLifeTime = 30f;
    private SpriteRenderer _spriteRenderer;
    private Rigidbody2D _rigidbody;
    private IAsteroidFactory asteroidFactory;

    public void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    public void Initialize(float size, Vector2 trajectory)
    {
        asteroidFactory = GameManager.Instance.GetAsteroidFactory();
        _spriteRenderer.sprite = sprites[Random.Range(0, sprites.Length)];

        //randomly rotating the sprites
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
        for(int i = 0; i < parts; i++)
        {
            //offsetting the position of new spawns
            Vector2 position = transform.position;
            Asteroid part = asteroidFactory.CreateAsteroid(position, transform.rotation, size / parts, Random.insideUnitCircle.normalized);
        }
    }

}
