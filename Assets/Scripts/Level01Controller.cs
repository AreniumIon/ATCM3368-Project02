using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Level01Controller : MonoBehaviour
{
    [SerializeField] Text currentScoreTextView;
    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject gameOverMenu;

    int currentScore;

    private void Start()
    {
        Unpause();
        gameOverMenu.SetActive(false);
    }

    private void Update()
    {
        //pause
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (pauseMenu.activeSelf)
                Unpause();
            else
                Pause();
        }
    }

    public void IncreaseScore(int scoreIncrease)
    {
        //increase score
        currentScore += scoreIncrease;
        //update score display to see new score
        currentScoreTextView.text = "Score: " + currentScore;
    }

    public void Pause()
    {
        //free cursor
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        //pause menu
        pauseMenu.SetActive(true);
        //time
        Time.timeScale = 0f;
    }

    public void Unpause()
    {
        //lock cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        //pause menu
        pauseMenu.SetActive(false);
        //time
        Time.timeScale = 1f;
    }

    public void ExitLevel()
    {
        //compare score to high score
        int highScore = PlayerPrefs.GetInt("HighScore");
        if (currentScore > highScore)
        {
            //save current score as new high score
            PlayerPrefs.SetInt("HighScore", currentScore);
            Debug.Log("New high score: " + currentScore);
        }
        //load new level
        SceneManager.LoadScene("MainMenu");
    }

    public void GameOver()
    {
        //free cursor
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        //gameover menu
        gameOverMenu.SetActive(true);
        //time
        Time.timeScale = 0f;
    }

    public void Retry()
    {
        //compare score to high score
        int highScore = PlayerPrefs.GetInt("HighScore");
        if (currentScore > highScore)
        {
            //save current score as new high score
            PlayerPrefs.SetInt("HighScore", currentScore);
            Debug.Log("New high score: " + currentScore);
        }
        //retry level
        SceneManager.LoadScene("Level01");
    }
}
