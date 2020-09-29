using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuController : MonoBehaviour
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
}
