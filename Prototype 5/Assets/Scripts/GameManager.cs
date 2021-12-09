using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    public List<GameObject> targets;
    private float spawnRate = 1;
    public TextMeshProUGUI scoreText;
    private int score;
    public TextMeshProUGUI gameOverText;
    public bool gameOver;
    public Button restartButton;
    public GameObject titleScreen;
    public TextMeshProUGUI highscoreText;
    private int highscore = 0;
    private int lives = 3;//default
    public TextMeshProUGUI livesText;


    void Start()
    {
        highscore = PlayerPrefs.GetInt("Highscore");
    }

    IEnumerator SpawnTarget()
    {
        livesText.text = "Lives: " + lives;
        while (!gameOver)
        {
            yield return new WaitForSeconds(spawnRate);
            int index = Random.Range(0, targets.Count);
            Instantiate(targets[index]);

        }
    }
    public void UpdateScore(int scoreToAdd)
    {

        score += scoreToAdd;
        scoreText.text = "Score:" + score;
        
        if (score > highscore)
        {
            highscore = score;
        }
        highscoreText.text = "Highscore" + highscore;

    }
    public void GameOver()
    {
        PlayerPrefs.SetInt("Highscore", highscore);
        restartButton.gameObject.SetActive(true);
        gameOverText.gameObject.SetActive(true);
        gameOver = true;
    }
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void StartGame(int difficulty)
    {
        spawnRate = spawnRate / difficulty;
        StartCoroutine(SpawnTarget());
        score = 0;
        lives = 3;
        UpdateScore(0);
        gameOver = false;
        titleScreen.gameObject.SetActive(false);
    }

    public void LoseHealth()
    {
        if (lives > 0)
            lives--;
        if (lives <= 0)
        {
            GameOver();
        }
        livesText.text = "Lives: " + lives;
    }
}
