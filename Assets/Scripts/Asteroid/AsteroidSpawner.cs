using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject asteroidFactoryPrefab; // Reference to the AsteroidFactory prefab
    private IAsteroidFactory asteroidFactory;
    public float trajectoryVariance = 15.0f; //15 degrees of cone variance
    public float spawnRate = 2.0f;
    public int spawnAmount = 1;
    public float spawnDistance = 15.0f; //threshold to spawn on the outside

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
            asteroidFactory = factoryObject.GetComponent<IAsteroidFactory>();
        }
    }

    private void Spawn()
    {
        if (asteroidFactory != null)
        {
            for (int i = 0; i < spawnAmount; i++)
            {
                Vector3 spawnDirection = Random.insideUnitCircle.normalized; // spawn with random direction
                Vector3 spawnPoint = transform.position + (spawnDirection * spawnDistance); // spawn a distance away from the destroyed one;

                float variance = Random.Range(-trajectoryVariance, trajectoryVariance);
                Quaternion rotation = Quaternion.AngleAxis(variance, Vector3.forward);

                Asteroid asteroid = asteroidFactory.CreateAsteroid(spawnPoint, rotation, Random.Range(Asteroid.minSize, Asteroid.maxSize), rotation * -spawnDirection);
            }
        }
    }
}
