using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    [SerializeField] AudioClip startingSong;

    //Start is called before the first frame update
    private void Start()
    {
        if (startingSong != null)
        {
            AudioManager.Instance.PlaySong(startingSong);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            ExitLevel();
    }

    public void ExitLevel()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
