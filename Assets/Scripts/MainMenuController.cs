using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] AudioClip startingSong;
    [SerializeField] Text highScoreTextView;

    //Start is called before the first frame update
    private void Start()
    {
        //load high score display
        int highScore = PlayerPrefs.GetInt("HighScore");
        highScoreTextView.text = highScore.ToString();

        //play music
        if (startingSong != null)
        {
            AudioManager.Instance.PlaySong(startingSong);
        }
    }

    public void ResetData()
    {
        PlayerPrefs.SetInt("HighScore", 0);
        highScoreTextView.text = PlayerPrefs.GetInt("HighScore").ToString();
    }

    public void Exit()
    {
        Application.Quit();
    }
}
