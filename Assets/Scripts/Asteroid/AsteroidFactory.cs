using UnityEngine;
public class AsteroidFactory : MonoBehaviour
{
    [SerializeField]
    private GameObject asteroidPrefab;

    public Asteroid CreateAsteroid(Vector3 position, Quaternion rotation, float size, Vector2 trajectory)
    {
        GameObject asteroidObject = Instantiate(asteroidPrefab, position, rotation);
        Asteroid asteroid = asteroidObject.GetComponent<Asteroid>();
        asteroid.Initialize(size, trajectory);
        asteroid.size = size;
        asteroid.SetTrajectory(trajectory);

        return asteroid;
    }
}