using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Level01Controller : MonoBehaviour
{
    //AUDIO
    [SerializeField] AudioClip startingSong;
    
    //Start is called before the first frame update
    private void Start()
    {
        if (startingSong != null)
        {
            AudioManager.Instance.PlaySong(startingSong);
        }
    }

    //SCORE AND SCENE MANAGEMENT
    [SerializeField] Text currentScoreTextView;

    int currentScore;

    private void Update()
    {
        //increase score
        if (Input.GetKeyDown(KeyCode.Q))
        {
            IncreaseScore(5);
        }
        //exit level
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ExitLevel();
        }
    }

    public void IncreaseScore(int scoreIncrease)
    {
        //increase score
        currentScore += scoreIncrease;
        //update score display to see new score
        currentScoreTextView.text = "Score: " + currentScore;
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
}
