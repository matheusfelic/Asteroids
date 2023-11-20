using UnityEngine;

public interface IAsteroidFactory
{
    Asteroid CreateAsteroid(Vector3 position, Quaternion rotation, float size, Vector2 trajectory);
}