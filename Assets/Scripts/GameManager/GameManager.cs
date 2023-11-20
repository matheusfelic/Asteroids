using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
     public static GameManager Instance { get; private set; }
     private AsteroidFactory asteroidFactory;
     private UIManager uiManager;

     [SerializeField]
     private Player player;
     [SerializeField]
     public ParticleSystem explosion;
     [SerializeField]
     private GameObject gameOverUI;
     [SerializeField]
     private Text scoreText;
     [SerializeField]
     private Text livesText;
     [SerializeField]
     private GameObject asteroidFactoryPrefab;

     private int lives;
     private int score;

     public int Score => score;
     public int Lives => lives;

     private void Awake()
     {
          InitializeSingleton();
          uiManager = new UIManager(scoreText, livesText);

          GameObject factoryObject = Instantiate(asteroidFactoryPrefab);
          asteroidFactory = factoryObject.GetComponent<AsteroidFactory>();
     }

     public AsteroidFactory GetAsteroidFactory()
     {
          return asteroidFactory;
     }

     private void InitializeSingleton()
     {
          if (Instance != null)
          {
               DestroyImmediate(gameObject);
          }
          else
          {
               Instance = this;
               DontDestroyOnLoad(gameObject);
          }
     }

     private void Start()
     {
          NewGame();
     }

     private void Update()
     {
          CheckForNewGameInput();
     }

     private void CheckForNewGameInput()
     {
          if (lives <= 0 && Input.GetKeyDown(KeyCode.Return))
          {
               NewGame();
          }
     }

     public void NewGame()
     {
          RemoveAsteroids();
          HideGameOverUI();
          SetScore(0);
          SetLives(3);
          Respawn();
     }

     private void RemoveAsteroids()
     {
          Asteroid[] asteroids = FindObjectsOfType<Asteroid>();

          foreach (Asteroid asteroid in asteroids)
          {
               Destroy(asteroid.gameObject);
          }
     }

     private void HideGameOverUI()
     {
          gameOverUI.SetActive(false);
     }

     private void SetScore(int score)
     {
          this.score = score;
          UpdateScoreText();
     }

     private void UpdateScoreText()
     {
          uiManager.SetScore(score);
     }

     private void SetLives(int lives)
     {
          this.lives = lives;
          UpdateLivesText();
     }

     private void UpdateLivesText()
     {
          uiManager.SetLives(lives);
     }

     public void OnAsteroidDestroyed(Asteroid asteroid)
     {
          PlayExplosion(asteroid.transform.position);
          SetScore(this.score + (int)(100 * (1 / asteroid.size)));
     }

     private void PlayExplosion(Vector3 position)
     {
          explosion.transform.position = position;
          explosion.Play();
     }

     public void OnPlayerDeath(Player player)
     {
          player.gameObject.SetActive(false);

          PlayExplosion(player.transform.position);

          UpdateLivesAfterDeath();
     }

     private void UpdateLivesAfterDeath()
     {
          SetLives(lives - 1);

          if (lives <= 0)
          {
               ShowGameOverUI();
          }
          else
          {
               StartCoroutine(RespawnCoroutine(player.respawnDelay));
          }
     }

     private void ShowGameOverUI()
     {
          gameOverUI.SetActive(true);
     }

     private IEnumerator RespawnCoroutine(float delay)
     {
          yield return new WaitForSeconds(delay);
          Respawn();
     }

     private void Respawn()
     {
          player.transform.position = Vector3.zero;
          player.gameObject.SetActive(true);
     }
}
