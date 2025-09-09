using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public List<GameObject> targets;
    public TextMeshProUGUI scoreText;
    private int score;

    public TextMeshProUGUI livesText;
    private int lives;

    private float spawnRate = 1;

    public TextMeshProUGUI gameOverText;

    public bool isGameActive;

    public Button restartButton;

    public GameObject titleScreen;

    public bool paused = false;
    public GameObject pauseScreen;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    public void startGame(float difficulty)
    {
        spawnRate /= difficulty;
        titleScreen.SetActive(false);
        StartCoroutine(SpawnTarget());
        score = 0;
        UpdateScore(0);
        gameOverText.gameObject.SetActive(false);
        restartButton.gameObject.SetActive(false);

        lives = 3;
        livesText.text = "Lives: " + lives;
        isGameActive = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ChangePaused();
        }
    }

    IEnumerator SpawnTarget()
    {
        while (isGameActive)
        {
            yield return new WaitForSeconds(spawnRate);
            int index = Random.Range(0, targets.Count);
            Instantiate(targets[index]);
        }
    }

    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = "Score: " + score;
    }

    public void UpdateLives()
    {
        lives -= 1;
        livesText.text = "Lives: " + lives;
        if(lives == 0)
        {
            GameOver();
        }
    }

    public void GameOver()
    {
        isGameActive = false;
        UpdateScore(score=0);
        gameOverText.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void ChangePaused()
    {
        if (!paused)
        {
            paused = true;
            pauseScreen.SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            paused = false;
            pauseScreen.SetActive(false);
            Time.timeScale = 1;
        }
    }
}
