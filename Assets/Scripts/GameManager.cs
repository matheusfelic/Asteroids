using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
   public static GameManager Instance { get; private set; }
  
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

   private int lives;
   private int score;

   public int Score => score;
   public int Lives => lives;

   private void Awake() 
   {
        if(Instance != null) {
            DestroyImmediate(gameObject);
        } else {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
   }

   private void Start() 
   {
        NewGame();
   }

   private void Update() {
        if(lives <= 0 && Input.GetKeyDown(KeyCode.Return)) {
            NewGame();
        }
   }

   private void NewGame() 
   {
        Asteroid[] asteroids = FindObjectsOfType<Asteroid>();

        for(int i = 0; i < asteroids.Length; i++) {
            Destroy(asteroids[i].gameObject);
        }

        gameOverUI.SetActive(false);
        SetScore(0);
        SetLives(3);
        Respawn();
   }

   private void SetScore(int score) 
   {
        this.score = score;
        scoreText.text = score.ToString();
   }

   private void SetLives(int lives)
   {
        this.lives = lives;
        livesText.text = lives.ToString();
   }

   public void OnAsteroidDestroyed(Asteroid asteroid) 
   {
        explosion.transform.position = asteroid.transform.position;
        explosion.Play();

        SetScore(this.score + (int) (100 * (1/asteroid.size)));
   }

   public void OnPlayerDeath(Player player) 
   {
        player.gameObject.SetActive(false);

        //playing explosion animation
        explosion.transform.position = player.transform.position;
        explosion.Play();

        SetLives(this.lives - 1);
        if(this.lives <= 0) 
        {
            gameOverUI.SetActive(true);
        } 
        else 
        {
            Invoke(nameof(Respawn), player.respawnDelay);
        }
   }

   private void Respawn() {
        player.transform.position = Vector3.zero;
        player.gameObject.SetActive(true);
   }
}
