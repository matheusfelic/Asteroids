using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject asteroidFactoryPrefab;
    private AsteroidFactory asteroidFactory;
    public float trajectoryVariance = 15.0f;
    public float spawnRate = 2.0f;
    public int spawnAmount = 1;
    public float spawnDistance = 15.0f;

    private void Start()
    {
        InitializeAsteroidFactory();
        InvokeRepeating(nameof(Spawn), spawnRate, spawnRate);
    }

    private void InitializeAsteroidFactory()
    {
        if (asteroidFactoryPrefab != null)
        {
            GameObject factoryObject = Instantiate(asteroidFactoryPrefab);
            asteroidFactory = factoryObject.GetComponent<AsteroidFactory>();
        }
    }

    private void Spawn()
    {
        if (asteroidFactory != null)
        {
            for (int i = 0; i < spawnAmount; i++)
            {
                Vector3 spawnDirection = Random.insideUnitCircle.normalized;
                Vector3 spawnPoint = transform.position + (spawnDirection * spawnDistance);

                float variance = Random.Range(-trajectoryVariance, trajectoryVariance);
                Quaternion rotation = Quaternion.AngleAxis(variance, Vector3.forward);

                Asteroid asteroid = asteroidFactory.CreateAsteroid(spawnPoint, rotation, Random.Range(Asteroid.minSize, Asteroid.maxSize), rotation * -spawnDirection);
            }
        }
    }
}
