using UnityEngine;

public interface IGameStateManager
{
    void NewGame();
    void OnAsteroidDestroyed(Asteroid asteroid);
    void OnPlayerDeath(Player player);
    IAsteroidFactory GetAsteroidFactory();
}